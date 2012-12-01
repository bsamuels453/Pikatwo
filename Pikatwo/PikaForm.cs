#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IRCBackend;
using Newtonsoft.Json;

#endregion

namespace Pikatwo{
    public partial class PikaForm : Form{
        IrcInstance _irc;

        public PikaForm(){
            InitializeComponent();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + @"\data\";
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + @"\data\";
            DisconnectBut.Enabled = false;
            LoadLastUsedServer();
        }

        #region input

        void SaveButClick(object sender, EventArgs e){
            saveFileDialog1.ShowDialog();
        }

        void LoadButClick(object sender, EventArgs e){
            openFileDialog1.ShowDialog();
        }

        void DisconnectButClick(object sender, EventArgs e){
            OnLogMsgInv("Disconnected from server");
            OnLogMsgInv("------------------------");
            _irc.Dispose();
            _irc = null;
            DisconnectBut.Enabled = false;
            ConnectBut.Enabled = true;
        }

        void ConnectButClick(object sender, EventArgs e){
            var init = new IrcInitData();
            init.Address = ServerAddressTexbox.Text;
            init.Port = int.Parse(ServerPortTexBox.Text);
            init.UserNick = UserNickTextbox.Text;
            init.DefaultChannel = DefaultChannelTexbox.Text;
            init.UserPass = UserPasswordTexbox.Text;

            _irc = new IrcInstance(init, OnLogMsg, new []{new Chatlogger()});
            _irc.Connect();
            ConnectBut.Enabled = false;
            DisconnectBut.Enabled = true;
        }

        void LogBoxTextChanged(object sender, EventArgs e){
            LogTextbox.SelectionStart = LogTextbox.Text.Length;
            LogTextbox.ScrollToCaret();
        }

        void SaveFileDialog1FileOk(object sender, CancelEventArgs e){
            var saveData = new SaveFileFmt();
            saveData.ServerName = ServerAddressTexbox.Text;
            saveData.ServerPort = ServerPortTexBox.Text;
            saveData.DefaultChannel = DefaultChannelTexbox.Text;
            saveData.Nick = UserNickTextbox.Text;
            saveData.NickPass = UserPasswordTexbox.Text;
            saveData.CryptPassword();

            string s = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            using (var sw = new StreamWriter(saveFileDialog1.FileName)){
                sw.Write(s);
                sw.Close();
            }
            SaveLastUsedServer(saveFileDialog1.FileName);
        }

        void OpenFileDialog1FileOk(object sender, CancelEventArgs e){
            LoadFromFile(openFileDialog1.FileName);
            SaveLastUsedServer(openFileDialog1.FileName);
        }

        #endregion

        #region loading/unloading

        void LoadFromFile(string fileName){
            string s;
            using (var sr = new StreamReader(fileName)){
                s = sr.ReadToEnd();
                sr.Close();
            }
            var saveData = JsonConvert.DeserializeObject<SaveFileFmt>(s);
            saveData.CryptPassword();
            ServerAddressTexbox.Text = saveData.ServerName;
            ServerPortTexBox.Text = saveData.ServerPort;
            DefaultChannelTexbox.Text = saveData.DefaultChannel;
            UserNickTextbox.Text = saveData.Nick;
            UserPasswordTexbox.Text = saveData.NickPass;
        }

        void SaveLastUsedServer(string fileName){
            if (!Directory.Exists("/data")){
                Directory.CreateDirectory("data");
            }
            using (var sw = new StreamWriter("data/lastusedserver.txt", false)){
                sw.WriteLine(fileName);
                sw.Flush();
                sw.Close();
            }
        }

        void LoadLastUsedServer(){
            if (!Directory.Exists("data")){
                Directory.CreateDirectory("data");
            }
            string s;
            try{
                using (var sr = new StreamReader("data/lastusedserver.txt")){
                    s = sr.ReadLine();
                    sr.Close();
                    if (s == "")
                        return;
                    LoadFromFile(s);
                }
            }
            catch (Exception){
                using (var sw = new StreamWriter("data/lastusedserver.txt")){
                    sw.Close();
                }
            }
        }

        #endregion

        void OnLogMsg(string str){
            Invoke(new MsgLog(OnLogMsgInv), str);
        }

        void OnLogMsgInv(string str){
            LogTextbox.Text += ":" + str + Environment.NewLine;
        }

        #region Nested type: MsgLog

        delegate void MsgLog(string str);

        #endregion

        #region Nested type: SaveFileFmt

        struct SaveFileFmt{
            const string _key = "964d72e72d053d501f2949969849b96c";

            public string DefaultChannel;
            public string Nick;
            public string NickPass;
            public string ServerName;
            public string ServerPort;

            public void CryptPassword(){
                string nStr = "";

                for (int i = 0; i < NickPass.Count(); i++){
                    int keyPosition = i%_key.Length;
                    uint keyCode = _key[keyPosition];
                    uint combinedCode = NickPass[i] ^ keyCode;
                    char combinedChar = (char) combinedCode;
                    nStr += combinedChar;
                }

                NickPass = nStr;
            }
        }

        #endregion
    }
}