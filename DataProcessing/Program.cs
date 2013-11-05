#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace DataProcessing{
    internal class Program{
        static void Main(string[] args){
            //StripExcessData();
            //LocateCandidateLines();
        }

        static void LocateCandidateLines(){
            var inStrm = new StreamReader("data/dateRemoved1.txt");
            var usersInChannel = new List<string>(500);
            var candidates = new StreamWriter("data/candidates2.txt");
            candidates.AutoFlush = false;

            string s = "";
            int lineIdx = 0;
            while ((s = inStrm.ReadLine()) != null){
                //a line is ignored for one of two reasons - it's an edge case line that can't be parsed, or it's an ENTER/EXIT channel msg
                bool ignoreLine = false;
                if (s[0] == '*'){
                    //user entered/exited channel
                    var name = s.Substring(2);
                    var terminator = name.IndexOf(' ');
                    name = name.Substring(0, terminator);
                    var trailing = s.Substring(terminator + 2);
                    if (trailing.Contains("has joined")){
                        usersInChannel.Remove(name);
                        usersInChannel.Add(name);
                    }
                    else{
                        usersInChannel.Remove(name);
                    }
                    ignoreLine = true;
                }
                else{
                    //standard user message
                    try{
                        var name = s.Substring(1);
                        var terminator = name.IndexOf('>');
                        name = name.Substring(0, terminator);

                        if (!usersInChannel.Contains(name)){
                            usersInChannel.Add(name);
                        }
                    }
                    catch{
                        //handles crazy edge cases - virtually no useful data causes this exception
                        ignoreLine = true;
                    }
                }

                //note: the branch predictor is going to pretty much optimize this conditional out w/ ~5% fail
                if (!ignoreLine){
                    //check for  nick mention matches
                    var msgBeginIdx = s.IndexOf('>');
                    var msg = s.Substring(msgBeginIdx + 2);
                    bool containsNick = false;
                    string mentionedNick = "";

                    foreach (var nick in usersInChannel){
                        if (msg.Contains(nick)){
                            mentionedNick = nick;
                            containsNick = true;
                            break;
                        }
                    }

                    if (containsNick){
                        candidates.WriteLine(lineIdx + "," + mentionedNick);
                    }
                }
                lineIdx++;
            }

            inStrm.Close();
            candidates.Close();
        }

        static void StripExcessData(){
            var rawDataStrm = new StreamReader("data/rawLogs0.txt");
            var dateRemovedStrm = new StreamWriter("data/dateRemoved1.txt");
            string[] blacklist;
            using (var blacklistReader = new StreamReader("data/blacklist.json")){
                blacklist = JsonConvert.DeserializeObject<string[]>(blacklistReader.ReadToEnd());
            }
            dateRemovedStrm.AutoFlush = false;

            string s = "";
            while ((s = rawDataStrm.ReadLine()) != null){
                if (s.Equals(""))
                    continue;
                if (blacklist.Any(str => s.Contains(str))){
                    continue;
                }
                dateRemovedStrm.WriteLine(s.Substring(16));
            }

            rawDataStrm.Close();
            dateRemovedStrm.Close();
        }
    }
}