using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PikaIRC;

namespace Pikatwo {
    public partial class PikaForm : Form {
        public PikaForm() {
            InitializeComponent();

            var init = new IrcInitData();
            init.Address = "chat.freenode.net";
            init.Port = 6667;
            init.UserNick = "pikatwo";
            init.DefaultChannel = "#pikadev";
            init.UserPass = "benjamin1";
            //, 6667, "pikatwo", null, "#pikadev"
            var irc = new IrcInstance(init);
            irc.OnIrcMsg += OnMsgRecv;
            irc.Connect();

        }

        static void OnMsgRecv(string str) {
            System.Console.WriteLine(str);
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
