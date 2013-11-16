#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Meebey.SmartIrc4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace Pikatwo{
    internal class GithubTracker : IrcComponent{
        const string _configPath = "githubConfig.json";
        const string _announceHistoryPath = "githubAnnounceHistory.json";
        const long _updateIntervalSeconds = 60*60*6;
        readonly List<QueuedAnnouncement> _queuedAnnouncements;
        readonly List<RepoAnnounceHistory> _repoAnnounceHistory;
        readonly string _rssUrl;
        readonly List<string> _subscribedProjects;
        long _lastUpdate;

        public GithubTracker(){
            _queuedAnnouncements = new List<QueuedAnnouncement>();
            _lastUpdate = -99999999999;
            JObject config;
            using (var sr = new StreamReader(_configPath)){
                var configStr = sr.ReadToEnd();
                config = JObject.Parse(configStr);
            }

            _subscribedProjects = config["SubscribedProjects"].ToObject<List<string>>();
            _rssUrl = config["RssUrl"].ToObject<string>();

            using (var sr = new StreamReader(_announceHistoryPath)){
                var announceStr = sr.ReadToEnd();
                _repoAnnounceHistory = JsonConvert.DeserializeObject<List<RepoAnnounceHistory>>(announceStr);
            }
        }

        #region IrcComponent Members

        public ClientInterface IrcInterface { get; set; }

        public void Update(long secsSinceStart){
            if (secsSinceStart - _lastUpdate > _updateIntervalSeconds){
                try{
                    RefreshRepos();
                    DispatchAnnouncements();
                }
                catch (Exception e){
                    IrcInterface.DebugLog("EXCEPTION: GithubTracker Update()");
                    IrcInterface.DebugLog(e.Message);
                    IrcInterface.DebugLog(e.StackTrace);
                    IrcInterface.DebugLog("END EXCEPTION");
                }
                _lastUpdate = secsSinceStart;
            }
        }

        #endregion

        void DispatchAnnouncements(){
            var grouped = _queuedAnnouncements.GroupBy(announce => announce.RepoName);
            foreach (var repoAnnouncements in grouped){
                var announceLi = repoAnnouncements.ToList();
                announceLi.Sort((a1, a2) => a1.TimeStamp.CompareTo(a2.TimeStamp));
                //cull any announcements that were made in the past
                RepoAnnounceHistory announceHistory;
                try{
                    announceHistory = _repoAnnounceHistory.Single(d => d.RepoName.Equals(announceLi[0].RepoName));
                }
                catch{
                    announceHistory = new RepoAnnounceHistory();
                    announceHistory.AnnouncedPushes = new List<string>();
                    announceHistory.RepoName = announceLi[0].RepoName;
                    _repoAnnounceHistory.Add(announceHistory);
                }
                for (int i = 0; i < announceLi.Count; i++){
                    var identifier = announceLi[i].CommitHashStart + announceLi[i].CommitHashEnd;
                    if (announceHistory.AnnouncedPushes.Contains(identifier)){
                        announceLi.RemoveAt(i);
                        i--;
                    }
                }
                if (announceLi.Count == 0){
                    continue;
                }
                int numCommits = announceLi.Sum(announce => announce.CommitCount);
                string beginCommit = announceLi[0].CommitHashStart;
                string endCommit = announceLi.Last().CommitHashEnd;
                string finalLink = announceLi[0].Link + beginCommit + "..." + endCommit;
                var channels = IrcInterface.Client.GetChannels();
                string plural = "";
                if (numCommits > 1){
                    plural = "s";
                }
                foreach (var channel in channels){
                    var msg = numCommits + " commit" + plural + " pushed to " + announceLi[0].RepoName + " by " + announceLi[0].Author + " " + finalLink;
                    IrcInterface.Client.SendMessage(SendType.Message, channel, msg);
                }

                //add the newly made announcements to the announcement history
                foreach (var announcement in announceLi){
                    var identifier = announcement.CommitHashStart + announcement.CommitHashEnd;
                    announceHistory.AnnouncedPushes.Add(identifier);
                }
                SaveAnnouncementHistory();
            }
        }

        void SaveAnnouncementHistory(){
            var serialized = JsonConvert.SerializeObject(_repoAnnounceHistory, Formatting.Indented);
            var sw = new StreamWriter(_announceHistoryPath);
            sw.Write(serialized);
            sw.Close();
        }

        void RefreshRepos(){
            var client = new WebClient();
            var rssStr = client.DownloadString(_rssUrl);
            client.Dispose();

            var rssDocument = new HtmlDocument();
            rssDocument.LoadHtml(rssStr);

            var entries = rssDocument.DocumentNode.SelectNodes("//entry");

            foreach (var entry in entries){
                var childNodes = entry.ChildNodes;
                if (childNodes["id"].InnerText.Contains("PushEvent")){
                    QueueNewAnnouncement(childNodes);
                }
            }
        }

        void QueueNewAnnouncement(HtmlNodeCollection repoNodes){
            string title = repoNodes["title"].InnerText;
            var separator = title.IndexOf('/');
            var repoName = title.Substring(separator + 1);
            var author = repoNodes["author"].ChildNodes["name"].InnerText;
            var publishTime = repoNodes["published"].InnerText;
            var rawLink = repoNodes["link"].Attributes["href"].Value;
            var compareSeparator = rawLink.LastIndexOf('/');
            var link = rawLink.Substring(0, compareSeparator + 1);
            var beginCommit = rawLink.Substring(compareSeparator + 1, 10);
            var endCommit = rawLink.Substring(compareSeparator + 1 + 10 + 3, 10);
            var timestamp = new DateTime
                (
                int.Parse(publishTime.Substring(0, 4)),
                int.Parse(publishTime.Substring(5, 2)),
                int.Parse(publishTime.Substring(8, 2)),
                int.Parse(publishTime.Substring(11, 2)),
                int.Parse(publishTime.Substring(14, 2)),
                int.Parse(publishTime.Substring(17, 2))
                );

            var client = new WebClient();
            var commitPage = client.DownloadString(rawLink);
            var doc = new HtmlDocument();
            doc.LoadHtml(commitPage);
            client.Dispose();

            var entry = doc.DocumentNode.SelectNodes("//span[@class=\"num\"]");
            var rawNumCommits = entry[0].InnerText;
            rawNumCommits = new string(rawNumCommits.Where(char.IsDigit).ToArray());
            int numCommits = int.Parse(rawNumCommits);


            if (_subscribedProjects.Contains(repoName)){
                _queuedAnnouncements.Add
                    (new QueuedAnnouncement
                        (
                        author,
                        link,
                        repoName,
                        timestamp,
                        beginCommit,
                        endCommit,
                        numCommits
                        )
                    );
            }
        }

        #region Nested type: QueuedAnnouncement

        class QueuedAnnouncement{
            public readonly string Author;
            public readonly int CommitCount;
            public readonly string CommitHashEnd;
            public readonly string CommitHashStart;
            public readonly string Link;
            public readonly string RepoName;
            public DateTime TimeStamp;

            public QueuedAnnouncement(string author, string link, string repoName, DateTime timeStamp, string commitHashStart, string commitHashEnd,
                int commitCount){
                Author = author;
                Link = link;
                RepoName = repoName;
                TimeStamp = timeStamp;
                CommitHashStart = commitHashStart;
                CommitHashEnd = commitHashEnd;
                CommitCount = commitCount;
            }
        }

        #endregion

        #region Nested type: RepoAnnounceHistory

        class RepoAnnounceHistory{
            public List<string> AnnouncedPushes;
            public string RepoName;
        }

        #endregion
    }
}