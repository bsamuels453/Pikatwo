#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

#endregion

namespace DataProcessing{
    internal class Program{
        const long _context = 5;
        const int _repliesPerFile = 10000;

        static void Main(string[] args){
            StripExcessData();
            LocateCandidateLines();
            GenerateCandidateMetadata();
        }

        static int[] GetBlacklistedHashes(){
            var sr = new StreamReader("generation/hashblacklist.json");
            var hashWords = JsonConvert.DeserializeObject<string[]>(sr.ReadToEnd());
            sr.Close();
            var hashes = new List<int>();
            foreach (var word in hashWords){
                hashes.Add(FnvHash(word));
            }
            return hashes.ToArray();
        }

        static void GenerateCandidateMetadata(){
            string[] lines;
            var blacklist = GetBlacklistedHashes();
            var candidates = new List<Candidate>(150000);
            using (var inStrm = new StreamReader("generation/dateRemoved1.txt")){
                var s = inStrm.ReadToEnd();
                lines = s.Split(new[]{"" + '\r' + '\n'}, StringSplitOptions.None);
            }
            for (int i = 0; i < lines.Length; i++){
                lines[i] = lines[i].ToLower();
            }

            var candidateStream = new StreamReader("generation/candidates2.txt");

            var candidStr = "";
            while ((candidStr = candidateStream.ReadLine()) != null){
                int commaIdx = candidStr.IndexOf(',');
                var targetLineIdx = long.Parse(candidStr.Substring(0, commaIdx));
                if (targetLineIdx <= _context || lines.Length - targetLineIdx <= _context)
                    continue;
                var targetNick = candidStr.Substring(commaIdx + 1).ToLower();

                var targetLine = lines[targetLineIdx];
                int msgBegin = targetLine.IndexOf('>');
                targetLine = targetLine.Substring(msgBegin + 2);
                var insertOffset = targetLine.IndexOf(targetNick, StringComparison.CurrentCulture);

                var candidate = new Candidate();
                candidate.InsertOffset = insertOffset;
                candidate.Message = targetLine.Remove(insertOffset, targetNick.Length);
                if (candidate.Message.Length < 3)
                    continue;
                if (candidate.Message.Contains('>'))
                    continue;
                if (candidate.Message.Contains('['))
                    continue;

                var hashes = GenerateContextHashes(targetLineIdx, lines, blacklist);
                candidate.Hashes = hashes;
                candidates.Add(candidate);
            }

            CleanOutputFolder();
            for (int i = 0; i < candidates.Count; i += _repliesPerFile){
                var sw = new StreamWriter("responseData/responses" + i/_repliesPerFile + ".json");
                var fileCandidates = candidates.Skip(i).Take(_repliesPerFile).ToArray();
                var serialized = JsonConvert.SerializeObject(fileCandidates, Formatting.Indented);
                sw.Write(serialized);
                sw.Close();
            }
        }

        static void CleanOutputFolder(){
            var trashFiles = Directory.GetFiles("responseData");
            foreach (var file in trashFiles){
                File.Delete(file);
            }
        }

        static Dictionary<int, int> GenerateContextHashes(long lineIdx, string[] lines, int[] blacklist){
            var hashes = new Dictionary<int, int>(200);
            for (long i = lineIdx - _context; i < lineIdx + _context; i++){
                var line = lines[i];
                if (line[0] == '*')
                    continue;
                var lineHash = CalcHashes(line);
                foreach (var pair in lineHash){
                    if (blacklist.Contains(pair.Key))
                        continue;
                    if (hashes.ContainsKey(pair.Key)){
                        hashes[pair.Key]++;
                    }
                    else{
                        hashes.Add(pair.Key, 1);
                    }
                }
            }
            return hashes;
        }

        static Dictionary<int, int> CalcHashes(string str){
            var hashes = new Dictionary<int, int>(200);
            var split = str.Split();
            foreach (var word in split){
                var hash = FnvHash(word);
                if (hashes.ContainsKey(hash)){
                    hashes[hash]++;
                }
                else{
                    hashes.Add(hash, 1);
                }
            }
            return hashes;
        }

        static int FnvHash(string data){
            unchecked{
                var bytes = new byte[data.Length*sizeof (char)];
                Buffer.BlockCopy(data.ToCharArray(), 0, bytes, 0, bytes.Length);
                const int p = 16777619;
                int hash = (int) 2166136261;

                for (int i = 0; i < data.Length; i++)
                    hash = (hash ^ data[i])*p;

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }

        static void LocateCandidateLines(){
            var inStrm = new StreamReader("generation/dateRemoved1.txt");
            var usersInChannel = new List<string>(500);
            var candidates = new StreamWriter("generation/candidates2.txt");
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
            var rawDataStrm = new StreamReader("generation/rawLogs0.txt");
            var dateRemovedStrm = new StreamWriter("generation/dateRemoved1.txt");
            string[] blacklist;
            using (var blacklistReader = new StreamReader("generation/blacklist.json")){
                blacklist = JsonConvert.DeserializeObject<string[]>(blacklistReader.ReadToEnd());
            }
            dateRemovedStrm.AutoFlush = false;

            string s = "";
            while ((s = rawDataStrm.ReadLine()) != null){
                if (s.Equals(""))
                    continue;
                s = s.Substring(16);
                if (blacklist.Any(str => s.Contains(str))){
                    continue;
                }
                dateRemovedStrm.WriteLine(s);
            }

            rawDataStrm.Close();
            dateRemovedStrm.Close();
        }

        #region Nested type: Candidate

        class Candidate{
            public Dictionary<int, int> Hashes;
            public int InsertOffset;
            public string Message;
        }

        #endregion
    }
}