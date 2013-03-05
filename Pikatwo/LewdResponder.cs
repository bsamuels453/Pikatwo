using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IrcClient;
using Newtonsoft.Json.Linq;

namespace Pikatwo {
    class LewdResponder : IrcComponent {
        string[] _saidAliases;

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

        public LewdResponder(){
            var sr = new StreamReader("lewdWords.json");
            var jobj = JObject.Parse(sr.ReadToEnd());

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


            int f = 4;
        }


        public void Dispose(){
            throw new NotImplementedException();
        }

        public void Reset(){
            throw new NotImplementedException();
        }

        public void HandleMsg(IrcMsg msg, IrcInstance.SendIrcCmd sendMethod){
            throw new NotImplementedException();
        }
    }
}
