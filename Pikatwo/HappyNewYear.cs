#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Meebey.SmartIrc4net;

#endregion

namespace Pikatwo{
    internal class HappyNewYear : IrcComponent{
        readonly List<User> _activeCtcp;
        readonly List<User> _pendingUsers;
        readonly Stopwatch _timeSinceLastCtcp;
        bool _channelsInitialized;

        ClientInterface _client;
        List<User> _usersToGreet;

        public HappyNewYear(){
            _usersToGreet = new List<User>();
            _pendingUsers = new List<User>();
            _timeSinceLastCtcp = new Stopwatch();
            _activeCtcp = new List<User>();
            _timeSinceLastCtcp.Start();
        }

        #region IrcComponent Members

        public ClientInterface IrcInterface{
            get { return _client; }
            set{
                _client = value;
                _client.Client.OnCtcpReply += ClientOnOnCtcpReply;
            }
        }

        public void Update(long secsSinceStart){
            if (secsSinceStart < 15)
                return;
            if (!_channelsInitialized){
                ScanChannels();
                _channelsInitialized = true;
            }
            DispatchCtcps();
            UpdateGreetings();
        }

        public string[] GetCmdDocs(){
            return new string[0];
        }

        #endregion

        void DispatchCtcps(){
            if (_pendingUsers.Count > 0){
                if (_timeSinceLastCtcp.Elapsed.TotalSeconds >= 1){
                    var usr = _pendingUsers[0];
                    _activeCtcp.Add(usr);
                    _pendingUsers.RemoveAt(0);
                    _client.Client.SendMessage(SendType.CtcpRequest, usr.Nick, "TIME");
                    _timeSinceLastCtcp.Restart();
                }
            }
        }

        void UpdateGreetings(){
            var byTimeZone = _usersToGreet.GroupBy(u => u.DeltaHour).ToList();
            var curHour = DateTime.Now.Hour;
            foreach (var timeZone in byTimeZone){
                if (timeZone.Key + curHour >= 24){
                    var byChannel = timeZone.GroupBy(u => u.Channel).ToList();

                    foreach (var chUsrGrp in byChannel){
                        DispatchGreetings(chUsrGrp);
                    }

                    _usersToGreet = _usersToGreet.Where(usr => usr.DeltaHour != timeZone.Key).ToList();

                    break;
                }
            }
        }

        void DispatchGreetings(IGrouping<string, User> channelUsers){
            string prefix = "Happy New Year to ";
            string userList = "";
            foreach (var user in channelUsers){
                userList += user.Nick + ", ";
            }
            userList = userList.Substring(0, userList.Length - 2);

            var greetStr = prefix + userList + "!";
            _client.Client.SendMessage(SendType.Message, channelUsers.Key, greetStr);
        }

        void ScanChannels(){
            var channelStrs = _client.Client.GetChannels();
            var channels = channelStrs.Select(s => _client.Client.GetChannel(s));

            var users = new List<User>();
            foreach (var channel in channels){
                foreach (ChannelUser usr in channel.Users.Values){
                    users.Add(new User(usr.Nick, usr.Channel));
                }
            }
            _pendingUsers.AddRange(users);
        }

        void ClientOnOnCtcpReply(object sender, CtcpEventArgs ctcpEventArgs){
            try{
                var strTime = ctcpEventArgs.CtcpParameter;
                var semicolonIdx = strTime.IndexOf(':');
                var timeStr = strTime.Substring(semicolonIdx - 2, 5);

                var hourStr = timeStr.Substring(0, 2);
                var hour = int.Parse(hourStr);
                var diff = hour - DateTime.Now.Hour;
                var userNick = ctcpEventArgs.Data.Nick;
                var userChannel = _activeCtcp.Where(usr => usr.Nick.Equals(userNick)).ToList()[0].Channel;
                _usersToGreet.Add(new User(userNick, userChannel, diff));
            }
            catch (Exception e){
                int g = 5;
            }
        }

        #region Nested type: User

        class User{
            public readonly string Channel;
            public readonly int DeltaHour;
            public readonly string Nick;

            public User(string nick, string channel, int deltaHour){
                Nick = nick;
                Channel = channel;
                DeltaHour = deltaHour;
            }

            public User(string nick, string channel){
                Nick = nick;
                Channel = channel;
                DeltaHour = 0;
            }
        }

        #endregion
    }
}