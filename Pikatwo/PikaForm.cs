using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PikaIRC;

namespace Pikatwo {
    public partial class PikaForm : Form {
        IrcInstance _irc;

        public PikaForm() {
            InitializeComponent();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory()+@"\data\";
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + @"\data\";
            LoadLastUsedServer();
        }

        #region input

        private void SaveButClick(object sender, EventArgs e) {
            saveFileDialog1.ShowDialog();
        }

        private void LoadButClick(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void DisconnectButClick(object sender, EventArgs e) {
            _irc.Dispose();
            _irc = null;
        }

        private void ConnectButClick(object sender, EventArgs e) {
            var init = new IrcInitData();
            init.Address = ServerAddressTexbox.Text;
            init.Port = int.Parse(ServerPortTexBox.Text);
            init.UserNick = UserNickTextbox.Text;
            init.DefaultChannel = DefaultChannelTexbox.Text;
            init.UserPass = UserPasswordTexbox.Text;

             _irc = new IrcInstance(init);
             _irc.OnIrcMsg += OnLogMsg;
             _irc.Connect();
        }

        private void LogBoxTextChanged(object sender, EventArgs e) {
            LogTextbox.SelectionStart = LogTextbox.Text.Length;
            LogTextbox.ScrollToCaret();
        }

        private void SaveFileDialog1FileOk(object sender, CancelEventArgs e) {
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

        private void OpenFileDialog1FileOk(object sender, CancelEventArgs e) {
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
            using (var sw = new StreamWriter("data/lastusedserver.txt", false)) {
                sw.WriteLine(fileName);
                sw.Flush();
                sw.Close();
            }
        }

        void LoadLastUsedServer(){
            if (!Directory.Exists("data")) {
                Directory.CreateDirectory("data");
            }
            string s;
            try {
                using (var sr = new StreamReader("data/lastusedserver.txt")) {
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

        void OnLogMsg(string str) {
            this.Invoke(new MsgLog(OnLogMsgInv), str);
        }
        delegate void MsgLog(string str);
        void OnLogMsgInv(string str){
            LogTextbox.Text += str + '\n';
        }

        struct SaveFileFmt{
            const string _key = "964d72e72d053d501f2949969849b96c";

            public string ServerName;
            public string ServerPort;
            public string DefaultChannel;
            public string Nick;
            public string NickPass;

            public void CryptPassword(){
                string nStr = "";

                for (int i = 0; i < NickPass.Count(); i++){
                    int keyPosition = i % _key.Length;
                    uint keyCode = _key[keyPosition];
                    uint combinedCode = NickPass[i] ^ keyCode;
                    char combinedChar = (char)combinedCode;
                    nStr += combinedChar;
                }

                NickPass = nStr;
            }
        }

    }
}
