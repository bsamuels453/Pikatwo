#region

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

#endregion

namespace Pikatwo{
    internal static class Program{
        const string _configFile = "config.json";

        public static void Main(string[] args){
            var sr = new StreamReader(_configFile);
            var config = JObject.Parse(sr.ReadToEnd());

            var creds = new IrcLoginCreds();
            creds.Nick = config["Nick"].ToObject<string>();
            creds.Password = config["Password"].ToObject<string>();
            creds.Port = config["Port"].ToObject<int>();
            creds.Server = config["Server"].ToObject<string>();
            creds.RealName = config["RealName"].ToObject<string>();
            creds.UserName = config["UserName"].ToObject<string>();

            var components = new List<IrcComponent>();
            var channels = config["Channels"].ToObject<string[]>();
            components.Add(new ChannelManage(channels));
            components.Add(new Responder(creds.Nick));
            components.Add(new GithubTracker());

            var client = new ClientInterface(creds, components);
            client.Run();
        }
    }
}