﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Pikatwo {
    internal static class Parser{
        public static void ParseRaws(){
            //todo: convert everything to lowercase

            //if you run all of these at the same time, it will take ages to complete
            //this class is mostly experimental regardless

            //GenNicks("nicks.txt", "raw.txt", "recordingBlacklist.json");
            //FilterNoMentionComments("nicks.txt", "raw.txt", "mentionFiltered.txt");
            //FilterTalkerBlacklist("mentionBlacklist.json", "mentionFiltered.txt", "blacklistFiltered.txt");
            //ConvertInlineNicks("blacklistFiltered.txt", "inlinedQuotes.txt", "nicks.txt")
            ScoreQuotes("inlinedQuotes.txt","wordRanking.json", "parsedLogs.txt");
            
        }
        static void GenNicks(string nickFileName, string inFileName, string blacklistName) {
            Regex regex;
            using (var blacklistStrm = new StreamReader(blacklistName)) {
                string regexCumulative = "";
                var blackListStr = JsonConvert.DeserializeObject<string[]>(blacklistStrm.ReadToEnd());

                foreach (var pattern in blackListStr) {
                    regexCumulative += pattern + "|";
                }
                regexCumulative = regexCumulative.Substring(0, regexCumulative.Count() - 1);
                regex = new Regex(regexCumulative, RegexOptions.IgnoreCase);
            }
            #region unimplemented popularity filtering stuff
            //now we need to count the number of times each person has said something
            //it's ridiculiously faster to remap each name to an index value, output the
            //list of index values, then use the index values on a list to count how many
            //things each index value(name) has said
            //the alternative is to use a dictionary, which was so slow it couldnt get this
            //crap done in 40 minutes. screw dictionary lookups.
            /*
            StreamReader sr;
            string line;
            var nickIndexes = new List<string>();
            using (sr = new StreamReader(inFileName)){
                var remappedNames = new StreamWriter("temp");
                while ((line = sr.ReadLine()) != null){
                    int sepIdx = line.IndexOf(":");
                    if (sepIdx <= 0)
                        continue;
                    string nick = line.Substring(0, sepIdx);

                    int idx = -1;
                    if (!regex.IsMatch(nick)){
                        if (!nickIndexes.Contains(nick)) {
                            nickIndexes.Add(nick);
                            idx = nickIndexes.Count - 1;
                        }
                        else{
                            idx = nickIndexes.IndexOf(nick);

                        }
                    }
                    if (idx != -1){
                        remappedNames.WriteLine(idx);
                    }
                }
                remappedNames.Close();
            }

            var nickCommentCount = new int[nickIndexes.Count];

            using (sr = new StreamReader("temp")){
                while ((line = sr.ReadLine()) != null){
                    nickCommentCount[int.Parse(line)]++;
                }
            }

            //now we get the median number of comments per nick, and cut out the bottom half
            var sorted = (
                         from count in nickCommentCount
                         orderby count ascending
                         select count
                         ).ToArray();


            */
            #endregion

            string line;
            var nicks = new List<string>();

            using (var sr = new StreamReader(inFileName)) {
                while ((line = sr.ReadLine()) != null) {
                    int sepIdx = line.IndexOf(":");
                    if (sepIdx <= 0)
                        continue;
                    string nick = line.Substring(0, sepIdx).ToLower();
                    if (!regex.IsMatch(nick) && !nicks.Contains(nick)) {
                        nicks.Add(nick);
                    }
                }
            }

            var orderedNicks = from nick in nicks
                               where nick.Length >= 5
                               //where nick.Value > 1000
                               orderby nick.Length descending
                               select nick;

            var sw = new StreamWriter(nickFileName);
            foreach (var nick in orderedNicks) {
                sw.WriteLine(nick);
            }
            sw.Flush();
            sw.Close();

        }

        /// <summary>
        /// this filters out comments where they don't mention anyones names in them
        /// </summary>
        /// <param name="nickFileName"></param>
        /// <param name="inFileName"></param>
        /// <param name="outFileName"></param>
        static void FilterNoMentionComments(string nickFileName, string inFileName, string outFileName){
            var sourceComments = new StreamReader(inFileName);
            var sourceNicks = new StreamReader(nickFileName);
            var nicks = new List<string>();
            string str;
            while ((str = sourceNicks.ReadLine()) != null){
                nicks.Add(str);
            }
            sourceNicks.Close();

            var sourceCommentList = new LinkedList<string>();
            while ((str = sourceComments.ReadLine()) != null){
                if (str.Count() > 10){ //think up something better than this
                    sourceCommentList.AddLast(str.ToLower());
                }
            }

            Func<string, bool>
                containsNick = s => {
                    int separatorIdx = s.IndexOf(":");
                    if (separatorIdx <= 1)
                        return false;
                    string commenterName = s.Substring(0, separatorIdx);
                    string noCommenterName = s.Substring(separatorIdx + 1);
                    bool hasNick = false;
                    foreach (var nick in nicks) {
                        if (noCommenterName.Contains(nick)) {
                            hasNick = true;
                            break;
                        }
                    }
                    bool nickValid = false;
                    foreach (var nick in nicks) {
                        if (commenterName.Contains(nick)) {
                            nickValid = true;
                            break;
                        }
                    }
                    return hasNick && nickValid;
                };


            var filteredComments = from comment in sourceCommentList
                                   where containsNick(comment)
                                   select comment;

            var sw = new StreamWriter(outFileName);
            foreach (var comment in filteredComments){
                int separatorIdx = comment.IndexOf(":");
                sw.WriteLine(comment.Substring(separatorIdx + 1));
            }
            sw.Close();
        }

        static void FilterTalkerBlacklist(string blacklistName, string inFileName, string outFileName){
            Regex regex;
            using (var blacklistStrm = new StreamReader(blacklistName)) {
                string regexCumulative = "";
                var blackListStr = JsonConvert.DeserializeObject<string[]>(blacklistStrm.ReadToEnd());

                foreach (var pattern in blackListStr) {
                    regexCumulative += pattern + "|";
                }
                regexCumulative = regexCumulative.Substring(0, regexCumulative.Count() - 1);
                regex = new Regex(regexCumulative, RegexOptions.IgnoreCase);
            }

            var sw = new StreamWriter(outFileName);
            var sr = new StreamReader(inFileName);
            string line;
            while ((line = sr.ReadLine()) != null){
                if (!regex.IsMatch(line)){
                    sw.WriteLine(line);
                }
            }
            sr.Close();
            sw.Close();
        }

        static void ConvertInlineNicks(string inFileName, string outFileName, string nickFile){
            var sr = new StreamReader(inFileName);
            var sw = new StreamWriter(outFileName);
            
            string line;
            var nickStrm = new StreamReader(nickFile);
            var nicks = new List<string>();

            while ((line = nickStrm.ReadLine()) != null){
                nicks.Add(line);
            }
            nickStrm.Close();

            var quotes = new List<Quote>();
            while ((line = sr.ReadLine()) != null){
                int nicksFound = 0;
                foreach (var nick in nicks){
                    int idx = line.IndexOf(nick);
                    if (idx == -1)
                        continue;

                    line = line.Replace(nick, "$("+nicksFound+")");
                    nicksFound++;
                }
                if (nicksFound > 3){//this prevents spamnotices from leaking through
                    continue;
                }
                quotes.Add(new Quote{Text = line, InlinedNicks = nicksFound});
            }
            sw.Write(JsonConvert.SerializeObject(quotes, Formatting.Indented));
            sw.Close();
            sr.Close();
        }

        static void ScoreQuotes(string inFileName, string rankFileName, string outFileName){
            var sr = new StreamReader(inFileName);
            var quotes = JsonConvert.DeserializeObject<List<Quote>>(sr.ReadToEnd());
            sr.Close();
            sr = new StreamReader(rankFileName);
            var wordRanks = JsonConvert.DeserializeObject<Dictionary<string, int>>(sr.ReadToEnd());
            sr.Close();

            for (int i = 0; i < quotes.Count; i++){
                int score = 10;
                var str = quotes[i].Text;
                if (
                    str.Contains("www.") ||
                    str.Contains("http") ||
                    str.Contains(".com") ||
                    str.Contains(".net") ||
                    str.Contains(".org") ||
                    str.Contains(".edu") ||
                    str.Contains(".gov")
                    ){
                        quotes.RemoveAt(i);
                        i--;
                        continue;
                }

                foreach (var word in wordRanks){
                    if (str.Contains(word.Key)){
                        score += word.Value;
                    }
                }

                var splitStr = str.Split(' ');
                if (splitStr.Count() > 10){
                    score -= (splitStr.Count() - 10)*2;
                }

                if (score <= 0){
                    quotes.RemoveAt(i);
                    i--;
                    continue;
                }
                int quoteLen = str.Length - 4 * quotes[i].InlinedNicks;
                if (quoteLen < 3){
                    quotes.RemoveAt(i);
                    i--;
                    continue;
                }

                quotes[i].Score = score;
            }
            var sw = new StreamWriter(outFileName);
            sw.Write(JsonConvert.SerializeObject(quotes, Formatting.Indented));
            sw.Close();
        }
    }

    internal class Quote{
        public string Text;
        public int InlinedNicks;
        public int Score;
    }
}
