using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Pikatwo {
    static class Parser {
        public static void ParseRaws(string inFileName, bool generateNicks=true){
            //var sr = new StreamReader(inFileName);
            GenNicks(inFileName);
            var sentences = new List<string>();
        }

        static void GenNicks(string inFileName){
            Regex regex;
            using (var blacklistStrm = new StreamReader("blacklist.json")){
                string regexCumulative = "";
                var blackListStr = JsonConvert.DeserializeObject<string[]>(blacklistStrm.ReadToEnd());

                foreach (var pattern in blackListStr){
                    regexCumulative += pattern + "|";
                }
                regexCumulative = regexCumulative.Substring(0, regexCumulative.Count() - 1);
                regex = new Regex(regexCumulative, RegexOptions.IgnoreCase);
            }

            var nicks = new List<string>();
            using (var sr = new StreamReader(inFileName)){
                string line;
                while ((line = sr.ReadLine()) != null){
                    int sepIdx = line.IndexOf(":");
                    if (sepIdx <= 0)
                        continue;
                    string nick = line.Substring(0, sepIdx);
                    if(!regex.IsMatch(nick) && !nicks.Contains(nick)){
                        nicks.Add(nick);
                    }
                }
            }

            var orderedNicks = from nick in nicks
                               where nick.Length >= 5
                               orderby nick.Length descending
                               select nick;

            var sw = new StreamWriter("nicks.txt");
            foreach (var nick in orderedNicks){
                sw.WriteLine(nick);
            }
            sw.Flush();
            sw.Close();
            int f = 4;
        }

    }
}
