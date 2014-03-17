﻿#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Meebey.SmartIrc4net;
using Newtonsoft.Json;

#endregion

namespace Pikatwo{
    internal class Responder : IrcComponent{
        const int _responsesUntilRefresh = 50;
        const int _reusePreventionSize = 1000;
        readonly string _botNick;
        readonly Random _rand;
        readonly string[] _sourceFiles;
        readonly List<Response> _usedResponses;
        ClientInterface _ircInterface;
        List<Response> _responses;
        int _responsesConsumed;

        public Responder(string botNick){
            _botNick = botNick;
            _usedResponses = new List<Response>(_reusePreventionSize);
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
                _ircInterface.Client.OnChannelAction += ClientOnOnChannelMessage;
            }
        }

        public void Update(long secsSinceStart){
            if (_responsesConsumed > _responsesUntilRefresh){
                RefreshResponses();
                _ircInterface.DebugLog("Response cache refreshed");
            }
        }

        public string[] GetCmdDocs(){
            return new string[0];
        }

        #endregion

        void IrcInterfaceOnOnIrcCommand(OnCommandArgs args){
            if (args.AuthLevel == AuthLevel.Admin){
                if (args.Message == ".flushcache"){
                    RefreshResponses();
                    const string logMsg = "Response cache manually flushed and reloaded successfully.";
                    _ircInterface.Client.SendMessage(SendType.Message, args.Source, logMsg);
                    _ircInterface.DebugLog(logMsg);
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

            responses = responses.Where(response => !_usedResponses.Contains(response)).ToList();

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
            if (msg == null){
                return;
            }
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

                //var numwords = response.Split().Length;
                //Thread.Sleep((numwords*200));

                _ircInterface.Client.SendMessage(SendType.Message, ircEventArgs.Data.Channel, response);
                _responses.RemoveAt(replyIdx);
                _responsesConsumed++;

                _usedResponses.Add(replyToUse);
                if (_usedResponses.Count > _reusePreventionSize){
                    _usedResponses.RemoveAt(0);
                }
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

        class Response : IEquatable<Response>{
            public Dictionary<int, int> Hashes;
            public int InsertOffset;
            public string Message;

            #region IEquatable<Response> Members

            public bool Equals(Response other){
                if (Message.Equals(other.Message))
                    return true;
                return false;
            }

            #endregion
        }

        #endregion
    }
}