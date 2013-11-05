#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Meebey.SmartIrc4net;
using Newtonsoft.Json;

#endregion

namespace Pikatwo{
    internal class Responder : IrcComponent{
        const int _responsesUntilRefresh = 10;
        readonly string _botNick;
        readonly Random _rand;
        readonly string[] _sourceFiles;
        ClientInterface _ircInterface;
        List<Response> _responses;
        int _responsesConsumed;

        public Responder(string botNick){
            _botNick = botNick;
            _rand = new Random();
            _sourceFiles = Directory.GetFiles("responseData");
            RefreshResponses();
        }

        #region IrcComponent Members

        public ClientInterface IrcInterface{
            get { return _ircInterface; }
            set{
                _ircInterface = value;
                _ircInterface.OnIrcCommand += IrcInterfaceOnOnIrcCommand;
                _ircInterface.Client.OnChannelMessage += ClientOnOnChannelMessage;
            }
        }

        public void Update(long secsSinceStart){
            if (_responsesConsumed > _responsesUntilRefresh){
                RefreshResponses();
                _ircInterface.DebugLog("Response cache refreshed");
            }
        }

        #endregion

        void IrcInterfaceOnOnIrcCommand(OnCommandArgs args){
            if (args.AuthLevel == AuthLevel.Admin){
                if (args.Message == ".refreshcache"){
                    RefreshResponses();
                    _ircInterface.DebugLog("Response cache refreshed");
                }
            }
        }

        void RefreshResponses(){
            _responsesConsumed = 0;
            var srcIdx = _rand.Next(_sourceFiles.Length);
            List<Response> responses;
            using (var sr = new StreamReader(_sourceFiles[srcIdx])){
                responses = JsonConvert.DeserializeObject<List<Response>>(sr.ReadToEnd());
            }

            Debug.Assert(responses != null);
            _responses = responses;
        }

        static Dictionary<int, int> GenerateMsgHashes(string msg){
            var hashes = new Dictionary<int, int>(200);
            var split = msg.Split();
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

        void ClientOnOnChannelMessage(object sender, IrcEventArgs ircEventArgs){
            string msg = ircEventArgs.Data.Message;
            if (msg.Contains(_botNick)){
                var msgHashes = GenerateMsgHashes(msg.ToLower());

                var scores = new int[_responses.Count];
                for (int i = 0; i < _responses.Count; i++){
                    scores[i] = CalculateRelevanceScore(msgHashes, _responses[i].Hashes);
                }

                var max = scores.Max();
                var replyIdx = Array.IndexOf(scores, max);
                var replyToUse = _responses[replyIdx];
                var response = replyToUse.Message.Insert(replyToUse.InsertOffset, ircEventArgs.Data.Nick);
                _ircInterface.DebugLog("Response score: " + max);
                _ircInterface.Client.SendMessage(SendType.Message, ircEventArgs.Data.Channel, response);
                _responses.RemoveAt(replyIdx);
                _responsesConsumed++;
            }
        }

        int CalculateRelevanceScore(Dictionary<int, int> hashes1, Dictionary<int, int> hashes2){
            int score = 0;
            foreach (var pair in hashes1){
                if (hashes2.ContainsKey(pair.Key)){
                    score += hashes2[pair.Key]*pair.Value;
                }
            }
            return score;
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

        #region Nested type: Response

        class Response{
            public Dictionary<int, int> Hashes;
            public int InsertOffset;
            public string Message;
        }

        #endregion
    }
}