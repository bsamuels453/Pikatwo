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
        public PikaForm() {
            InitializeComponent();

            var init = new IrcInitData();
            init.Address = "chat.freenode.net";
            init.Port = 6667;
            init.UserNick = "pikatwo";
            init.DefaultChannel = "#testtdev";
            init.UserPass = "";
            //, 6667, "pikatwo", null, "#pikadev"
           // var irc = new IrcInstance(init);
            //irc.OnIrcMsg += OnMsgRecv;
           // irc.Connect();
            //LogTextWindow
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            LoadLastUsedServer();
        }

        static void OnMsgRecv(string str) {
            System.Console.WriteLine(str);
        }

        private void SaveButClick(object sender, EventArgs e) {
            saveFileDialog1.ShowDialog();
        }

        private void LoadButClick(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void DisconnectButClick(object sender, EventArgs e) {

        }

        private void ConnectButClick(object sender, EventArgs e) {

        }

        private void LogBoxTextChanged(object sender, EventArgs e) {

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
        }

        private void OpenFileDialog1FileOk(object sender, CancelEventArgs e) {
            LoadFromFile(openFileDialog1.FileName);
        }

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
            using (var sw = new StreamWriter("lastusedserver.txt")){
                sw.WriteLine(fileName);
                sw.Close();
            }
        }

        void LoadLastUsedServer(){
            string s;
            using (var sr = new StreamReader("lastusedserver.txt")){
                s = sr.ReadToEnd();
                sr.Close();
            }
            LoadFromFile(s);
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
