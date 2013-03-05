using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IrcClient;
using Newtonsoft.Json.Linq;

namespace Pikatwo {
    class LewdResponder : IrcComponent {
        readonly string _trigger;
        readonly string _channelName;
        readonly Random _rand;
        readonly string[][] _sentenceStructure;

        readonly string[] _introPhrases;
        readonly string[] _saidAliases;

        string[] _femaleAdjectives;
        string[] _femaleNouns;
        string[] _maleAdjectives;
        string[] _maleNouns;

        string[] _coitusVerbs;
        string[] _lewdVerbs;

        string[] _breastAdjectives;
        string[] _breastNouns;

        string[] _penisAdjectives;
        string[] _penisNouns;

        string[] _clitAdjectives;
        string[] _clitNouns;

        public LewdResponder(string trigger, string channelName){
            _trigger = trigger;
            _channelName = channelName;
            _rand = new Random(DateTime.Now.Millisecond);
            var sr = new StreamReader("lewdWords.json");
            var jobj = JObject.Parse(sr.ReadToEnd());

            _introPhrases = jobj["IntroPhrases"].ToObject<string[]>();
            _saidAliases = jobj["SaidAliases"].ToObject<string[]>();
            _femaleAdjectives = jobj["FemaleAdjectives"].ToObject<string[]>();
            _femaleNouns = jobj["FemaleNouns"].ToObject<string[]>();
            _maleAdjectives = jobj["MaleAdjectives"].ToObject<string[]>();
            _maleNouns = jobj["MaleNouns"].ToObject<string[]>();

            _coitusVerbs = jobj["CoitusVerbs"].ToObject<string[]>();
            _lewdVerbs = jobj["LewdVerbs"].ToObject<string[]>();

            _breastAdjectives = jobj["BreastAdjectives"].ToObject<string[]>();
            _breastNouns = jobj["BreastNouns"].ToObject<string[]>();

            _penisAdjectives = jobj["PenisAdjectives"].ToObject<string[]>();
            _penisNouns = jobj["PenisNouns"].ToObject<string[]>();

            _clitAdjectives = jobj["ClitAdjectives"].ToObject<string[]>();
            _clitNouns = jobj["ClitNouns"].ToObject<string[]>();

            _sentenceStructure = new string[20][];
            _sentenceStructure[0] = _introPhrases;
            _sentenceStructure[1] = _saidAliases;
            _sentenceStructure[2] = new [] { "the" };
            _sentenceStructure[3] = _femaleAdjectives;
            _sentenceStructure[4] = new[] {"$USER"};
            _sentenceStructure[5] = new[] { "as the" };
            _sentenceStructure[6] = _maleAdjectives;
            _sentenceStructure[7] = _maleNouns;
            _sentenceStructure[8] = _lewdVerbs;
            _sentenceStructure[9] = new[] { "her" };
            _sentenceStructure[10] = _breastAdjectives;
            _sentenceStructure[11] = _breastNouns;
            _sentenceStructure[12] = new[] { "and" };
            _sentenceStructure[13] = _coitusVerbs;
            _sentenceStructure[14] = new[] { "his" };
            _sentenceStructure[15] = _penisAdjectives;
            _sentenceStructure[16] = _penisNouns;
            _sentenceStructure[17] = new[] { "into her" };
            _sentenceStructure[18] = _clitAdjectives;
            _sentenceStructure[19] = _clitNouns;

        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod) {
            if (msg.Command == "PRIVMSG") {
                if (msg.CommandParams[0].Contains("#")) { //be certain we're recieving this from channel
                    if (msg.Trailing.Contains("pikatwo")) {
                        string message = "";
                        foreach (var phrases in _sentenceStructure){
                            message += phrases[_rand.Next(0, phrases.Count())] + " ";
                        }

                        string parsedName = msg.Prefix.Substring(1);
                        if (parsedName.Contains("bro-bot")) {
                            return;
                        }
                        int delimitierIdx = parsedName.IndexOf('!');
                        parsedName = parsedName.Substring(0, delimitierIdx);

                        message = message.Replace("$USER", parsedName);

                        sendMethod.Invoke(IrcCommand.Message, _channelName, message);
                    }
                }
            }
        }


        public void Dispose(){
        }

        public void Reset(){
        }
    }
}
