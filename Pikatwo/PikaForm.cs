#region

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IrcClient;
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
            SendMsgCombobox.Text = "Channel";

            LoadLastUsedServer();
            DisableBotControls();
        }

        #region form input handling

        void SaveButClick(object sender, EventArgs e){
            saveFileDialog1.ShowDialog();
        }

        void LoadButClick(object sender, EventArgs e){
            openFileDialog1.ShowDialog();
        }

        void DisconnectButClick(object sender, EventArgs e){
            _irc.Close();
            _irc = null;
            DisableBotControls();
        }

        void ConnectButClick(object sender, EventArgs e){
            var init = new IrcInitData();
            init.Address = ServerAddressTexbox.Text;
            init.Port = int.Parse(ServerPortTexBox.Text);
            init.UserNick = UserNickTextbox.Text;
            init.DefaultChannel = DefaultChannelTexbox.Text;
            init.UserPass = UserPasswordTexbox.Text;

            _irc = new IrcInstance(init, OnLogMsg, new IrcComponent[]{new Chatlogger(), new RawLogger()});
            _irc.Connect();
            EnableBotControls();
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

        private void PingButClick(object sender, EventArgs e) {
            _irc.SendCmd(IrcCommand.Ping, "", doLog: true);
        }

        private void PartChannelButClick(object sender, EventArgs e) {
            _irc.SendCmd(IrcCommand.Part, DefaultChannelTexbox.Text, doLog: true);
        }

        private void JoinChannelButClick(object sender, EventArgs e) {
            _irc.SendCmd(IrcCommand.Join, DefaultChannelTexbox.Text, doLog: true);
        }

        void PikaFormResize(object sender, EventArgs e){
            if (FormWindowState.Minimized == this.WindowState){
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState){
                notifyIcon1.Visible = false;
                //Application.OpenForms["PikaForm"].BringToFront();
            }
        }

        private void NotifyIcon1Click(object sender, EventArgs e) {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void SendMsgButtonClick(object sender, EventArgs e) {
            var target = SendMsgCombobox.Text;
            var text = SendMsgTextbox.Text;
            if (target == "" || text == "")
                return;

            if (target == "Channel")
                target = DefaultChannelTexbox.Text;

            _irc.SendCmd(IrcCommand.Message, target, text, doLog: true);
            SendMsgTextbox.Text = "";
        }

        #endregion

        void EnableBotControls(){
            ConnectBut.Enabled = false;
            DisconnectBut.Enabled = true;
            PingBut.Enabled = true;
            PartChannelBut.Enabled = true;
            JoinChannelBut.Enabled = true;
            SendMsgContainer.Enabled = true;
            ConnectionCredentials.Enabled = false;
        }

        void DisableBotControls(){
            ConnectBut.Enabled = true;
            DisconnectBut.Enabled = false;
            PingBut.Enabled = false;
            PartChannelBut.Enabled = false;
            JoinChannelBut.Enabled = false;
            SendMsgContainer.Enabled = false;
            ConnectionCredentials.Enabled = true;
        }

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