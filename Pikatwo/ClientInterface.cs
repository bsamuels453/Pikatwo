#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Meebey.SmartIrc4net;

#endregion

namespace Pikatwo{
    internal delegate void OnIrcCommand(OnCommandArgs args);

    /// <summary>
    /// exists to hide most of the ircclient stuff we dont want to expose to irccomponents
    /// </summary>
    internal class ClientInterface{
        const char _commandChar = '.';
        readonly Authenticator _authenticator;

        readonly List<IrcComponent> _components;
        readonly StreamWriter _debugWriter;
        readonly IrcLoginCreds _loginCreds;
        readonly StreamWriter _rawWriter;
        public bool KillClient;

        public ClientInterface(IrcLoginCreds loginCreds, List<IrcComponent> auxComponents){
            KillClient = false;
            Client = new IrcClient();
            Client.SendDelay = 200;
            Client.ActiveChannelSyncing = true;
            //Client.AutoRetry = true;
            _loginCreds = loginCreds;
            Client.CtcpVersion = "Pikatwo - Interactive chatbot with lifelike texture by zalzane.";

            _authenticator = new Authenticator();

            _components = auxComponents;
            _components.Add(_authenticator);
            _components.Add(new Reconnector());

            foreach (var component in _components){
                component.IrcInterface = this;
            }

            Client.OnChannelMessage += HandleCommands;
            Client.OnQueryMessage += HandleCommands;
            Client.OnRawMessage += ClientOnOnRawMessage;

            _debugWriter = new StreamWriter("debugOut.txt", true);
            _rawWriter = new StreamWriter("rawOut.txt", true);
        }


        public IrcClient Client { get; private set; }

        public string Nick{
            get { return Client.Nickname; }
        }

        void ClientOnOnRawMessage(object sender, IrcEventArgs ircEventArgs){
            var unixTime = (long) ((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
            _rawWriter.WriteLine(unixTime + " " + ircEventArgs.Data.RawMessage);
            _rawWriter.Flush();
        }

        void HandleCommands(object sender, IrcEventArgs ircEventArgs){
            if (ircEventArgs.Data.Message[0] == _commandChar){
                var authLevel = _authenticator.GetUserAuthLevel(ircEventArgs.Data.Host);
                var onCommand = new OnCommandArgs
                    (
                    authLevel,
                    ircEventArgs.Data.Channel,
                    ircEventArgs.Data.Host,
                    ircEventArgs.Data.Ident,
                    ircEventArgs.Data.Message,
                    ircEventArgs.Data.Nick
                    );

                DebugLog("Command; Nick:" + ircEventArgs.Data.Nick + " Authlevel:" + authLevel.ToString() + " Trailing:" + ircEventArgs.Data.Message);
                if (OnIrcCommand != null){
                    OnIrcCommand.Invoke(onCommand);
                }
                if (ircEventArgs.Data.Message.Equals(".help")){
                    PrintCommandHelp(ircEventArgs.Data.Channel);
                }
            }
        }

        void PrintCommandHelp(string channel){
            var commands = new List<string>();
            foreach (var component in _components){
                var cpntCmd = component.GetCmdDocs().ToList();
                commands.AddRange(cpntCmd);
            }
            var reply = "Commands: ";
            foreach (var command in commands){
                reply += command + ", ";
            }
            if (commands.Count > 0){
                reply = reply.Substring(0, reply.Length - 2);
            }
            Client.SendMessage(SendType.Message, channel, reply);
        }

        public event OnIrcCommand OnIrcCommand;


        public void Run(){
            Connect();
            var lastUpdate = DateTime.Now;
            long numSecondsSinceStart = 0;
            while (!KillClient){
                Client.ReadLine(true);
                if ((DateTime.Now - lastUpdate).TotalSeconds > 1){
                    foreach (var component in _components){
                        component.Update(numSecondsSinceStart);
                    }
                    numSecondsSinceStart += (int) (DateTime.Now - lastUpdate).TotalSeconds;
                    lastUpdate = DateTime.Now;
                }
            }
        }

        public void DebugLog(string text){
            var unixTime = (long) ((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
            _debugWriter.WriteLine(unixTime + " " + text);
            _debugWriter.Flush();
        }

        public void Connect(){
            bool connectionSuccessful = false;
            do{
                try{
                    Client.Connect(_loginCreds.Server, _loginCreds.Port);
                    Client.Login(_loginCreds.Nick, _loginCreds.RealName, 0, _loginCreds.UserName, _loginCreds.Password);
                    connectionSuccessful = true;
                }
                catch (ConnectionException e){
                }
            } while (!connectionSuccessful);
        }
    }
}