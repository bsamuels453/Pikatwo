using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Pikatwo {
    partial class PikaForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PikaForm));
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConnectionCredentials = new System.Windows.Forms.GroupBox();
            this.DefaultChannelTexbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UserPasswordTexbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UserNickTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LoadBut = new System.Windows.Forms.Button();
            this.SaveBut = new System.Windows.Forms.Button();
            this.ServerPortTexBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerAddressTexbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectBut = new System.Windows.Forms.Button();
            this.DisconnectBut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LogTextbox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BotControlContainer = new System.Windows.Forms.GroupBox();
            this.JoinChannelBut = new System.Windows.Forms.Button();
            this.PartChannelBut = new System.Windows.Forms.Button();
            this.PingBut = new System.Windows.Forms.Button();
            this.SendMsgContainer = new System.Windows.Forms.GroupBox();
            this.SendMsgButton = new System.Windows.Forms.Button();
            this.SendMsgCombobox = new System.Windows.Forms.ComboBox();
            this.SendMsgTextbox = new System.Windows.Forms.TextBox();
            this.ConnectionCredentials.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.BotControlContainer.SuspendLayout();
            this.SendMsgContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ConnectionCredentials
            // 
            this.ConnectionCredentials.Controls.Add(this.DefaultChannelTexbox);
            this.ConnectionCredentials.Controls.Add(this.label5);
            this.ConnectionCredentials.Controls.Add(this.UserPasswordTexbox);
            this.ConnectionCredentials.Controls.Add(this.label4);
            this.ConnectionCredentials.Controls.Add(this.UserNickTextbox);
            this.ConnectionCredentials.Controls.Add(this.label3);
            this.ConnectionCredentials.Controls.Add(this.LoadBut);
            this.ConnectionCredentials.Controls.Add(this.SaveBut);
            this.ConnectionCredentials.Controls.Add(this.ServerPortTexBox);
            this.ConnectionCredentials.Controls.Add(this.label2);
            this.ConnectionCredentials.Controls.Add(this.ServerAddressTexbox);
            this.ConnectionCredentials.Controls.Add(this.label1);
            this.ConnectionCredentials.Location = new System.Drawing.Point(12, 8);
            this.ConnectionCredentials.Name = "ConnectionCredentials";
            this.ConnectionCredentials.Size = new System.Drawing.Size(330, 108);
            this.ConnectionCredentials.TabIndex = 13;
            this.ConnectionCredentials.TabStop = false;
            this.ConnectionCredentials.Text = "Connection Credentials";
            // 
            // DefaultChannelTexbox
            // 
            this.DefaultChannelTexbox.Location = new System.Drawing.Point(222, 33);
            this.DefaultChannelTexbox.Name = "DefaultChannelTexbox";
            this.DefaultChannelTexbox.Size = new System.Drawing.Size(100, 20);
            this.DefaultChannelTexbox.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Channel";
            // 
            // UserPasswordTexbox
            // 
            this.UserPasswordTexbox.Location = new System.Drawing.Point(116, 77);
            this.UserPasswordTexbox.Name = "UserPasswordTexbox";
            this.UserPasswordTexbox.PasswordChar = '*';
            this.UserPasswordTexbox.ShortcutsEnabled = false;
            this.UserPasswordTexbox.Size = new System.Drawing.Size(100, 20);
            this.UserPasswordTexbox.TabIndex = 22;
            this.UserPasswordTexbox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Nick Password";
            // 
            // UserNickTextbox
            // 
            this.UserNickTextbox.Location = new System.Drawing.Point(6, 77);
            this.UserNickTextbox.Name = "UserNickTextbox";
            this.UserNickTextbox.Size = new System.Drawing.Size(100, 20);
            this.UserNickTextbox.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Nick";
            // 
            // LoadBut
            // 
            this.LoadBut.Location = new System.Drawing.Point(274, 73);
            this.LoadBut.Name = "LoadBut";
            this.LoadBut.Size = new System.Drawing.Size(44, 23);
            this.LoadBut.TabIndex = 18;
            this.LoadBut.Text = "Load";
            this.LoadBut.UseVisualStyleBackColor = true;
            this.LoadBut.Click += new System.EventHandler(this.LoadButClick);
            // 
            // SaveBut
            // 
            this.SaveBut.Location = new System.Drawing.Point(222, 74);
            this.SaveBut.Name = "SaveBut";
            this.SaveBut.Size = new System.Drawing.Size(46, 23);
            this.SaveBut.TabIndex = 17;
            this.SaveBut.Text = "Save";
            this.SaveBut.UseVisualStyleBackColor = true;
            this.SaveBut.Click += new System.EventHandler(this.SaveButClick);
            // 
            // ServerPortTexBox
            // 
            this.ServerPortTexBox.Location = new System.Drawing.Point(116, 33);
            this.ServerPortTexBox.Name = "ServerPortTexBox";
            this.ServerPortTexBox.Size = new System.Drawing.Size(100, 20);
            this.ServerPortTexBox.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Port";
            // 
            // ServerAddressTexbox
            // 
            this.ServerAddressTexbox.Location = new System.Drawing.Point(6, 33);
            this.ServerAddressTexbox.Name = "ServerAddressTexbox";
            this.ServerAddressTexbox.Size = new System.Drawing.Size(100, 20);
            this.ServerAddressTexbox.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Server Address";
            // 
            // ConnectBut
            // 
            this.ConnectBut.Location = new System.Drawing.Point(9, 19);
            this.ConnectBut.Name = "ConnectBut";
            this.ConnectBut.Size = new System.Drawing.Size(58, 22);
            this.ConnectBut.TabIndex = 14;
            this.ConnectBut.Text = "Connect";
            this.ConnectBut.UseVisualStyleBackColor = true;
            this.ConnectBut.Click += new System.EventHandler(this.ConnectButClick);
            // 
            // DisconnectBut
            // 
            this.DisconnectBut.Location = new System.Drawing.Point(73, 18);
            this.DisconnectBut.Name = "DisconnectBut";
            this.DisconnectBut.Size = new System.Drawing.Size(75, 23);
            this.DisconnectBut.TabIndex = 15;
            this.DisconnectBut.Text = "Disconnect";
            this.DisconnectBut.UseVisualStyleBackColor = true;
            this.DisconnectBut.Click += new System.EventHandler(this.DisconnectButClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LogTextbox);
            this.groupBox2.Location = new System.Drawing.Point(348, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 346);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // LogTextbox
            // 
            this.LogTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogTextbox.Location = new System.Drawing.Point(3, 16);
            this.LogTextbox.Name = "LogTextbox";
            this.LogTextbox.ReadOnly = true;
            this.LogTextbox.Size = new System.Drawing.Size(403, 327);
            this.LogTextbox.TabIndex = 0;
            this.LogTextbox.Text = "";
            this.LogTextbox.WordWrap = false;
            this.LogTextbox.TextChanged += new System.EventHandler(this.LogBoxTextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "|*.irc";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "irc";
            this.saveFileDialog1.FileName = "server.irc";
            this.saveFileDialog1.Filter = "|*.irc";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1FileOk);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Sent to tray";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(348, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 108);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connection Info";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Connection Uptime:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Uptime:";
            // 
            // BotControlContainer
            // 
            this.BotControlContainer.BackColor = System.Drawing.SystemColors.Control;
            this.BotControlContainer.Controls.Add(this.JoinChannelBut);
            this.BotControlContainer.Controls.Add(this.PartChannelBut);
            this.BotControlContainer.Controls.Add(this.PingBut);
            this.BotControlContainer.Controls.Add(this.SendMsgContainer);
            this.BotControlContainer.Controls.Add(this.ConnectBut);
            this.BotControlContainer.Controls.Add(this.DisconnectBut);
            this.BotControlContainer.Location = new System.Drawing.Point(12, 122);
            this.BotControlContainer.Name = "BotControlContainer";
            this.BotControlContainer.Size = new System.Drawing.Size(330, 343);
            this.BotControlContainer.TabIndex = 18;
            this.BotControlContainer.TabStop = false;
            this.BotControlContainer.Text = "Bot Control";
            // 
            // JoinChannelBut
            // 
            this.JoinChannelBut.Location = new System.Drawing.Point(148, 47);
            this.JoinChannelBut.Name = "JoinChannelBut";
            this.JoinChannelBut.Size = new System.Drawing.Size(81, 23);
            this.JoinChannelBut.TabIndex = 19;
            this.JoinChannelBut.Text = "Join Channel";
            this.JoinChannelBut.UseVisualStyleBackColor = true;
            this.JoinChannelBut.Click += new System.EventHandler(this.JoinChannelButClick);
            // 
            // PartChannelBut
            // 
            this.PartChannelBut.Location = new System.Drawing.Point(62, 47);
            this.PartChannelBut.Name = "PartChannelBut";
            this.PartChannelBut.Size = new System.Drawing.Size(80, 23);
            this.PartChannelBut.TabIndex = 18;
            this.PartChannelBut.Text = "Part Channel";
            this.PartChannelBut.UseVisualStyleBackColor = true;
            this.PartChannelBut.Click += new System.EventHandler(this.PartChannelButClick);
            // 
            // PingBut
            // 
            this.PingBut.Location = new System.Drawing.Point(12, 47);
            this.PingBut.Name = "PingBut";
            this.PingBut.Size = new System.Drawing.Size(44, 23);
            this.PingBut.TabIndex = 17;
            this.PingBut.Text = "Ping";
            this.PingBut.UseVisualStyleBackColor = true;
            this.PingBut.Click += new System.EventHandler(this.PingButClick);
            // 
            // SendMsgContainer
            // 
            this.SendMsgContainer.Controls.Add(this.SendMsgButton);
            this.SendMsgContainer.Controls.Add(this.SendMsgCombobox);
            this.SendMsgContainer.Controls.Add(this.SendMsgTextbox);
            this.SendMsgContainer.Location = new System.Drawing.Point(6, 288);
            this.SendMsgContainer.Name = "SendMsgContainer";
            this.SendMsgContainer.Size = new System.Drawing.Size(315, 49);
            this.SendMsgContainer.TabIndex = 16;
            this.SendMsgContainer.TabStop = false;
            this.SendMsgContainer.Text = "Send Msg";
            // 
            // SendMsgButton
            // 
            this.SendMsgButton.Location = new System.Drawing.Point(265, 20);
            this.SendMsgButton.Name = "SendMsgButton";
            this.SendMsgButton.Size = new System.Drawing.Size(44, 20);
            this.SendMsgButton.TabIndex = 2;
            this.SendMsgButton.Text = "Send";
            this.SendMsgButton.UseVisualStyleBackColor = true;
            this.SendMsgButton.Click += new System.EventHandler(this.SendMsgButtonClick);
            // 
            // SendMsgCombobox
            // 
            this.SendMsgCombobox.FormattingEnabled = true;
            this.SendMsgCombobox.Items.AddRange(new object[] {
            "Nickserv",
            "Channel"});
            this.SendMsgCombobox.Location = new System.Drawing.Point(6, 19);
            this.SendMsgCombobox.Name = "SendMsgCombobox";
            this.SendMsgCombobox.Size = new System.Drawing.Size(91, 21);
            this.SendMsgCombobox.TabIndex = 1;
            // 
            // SendMsgTextbox
            // 
            this.SendMsgTextbox.Location = new System.Drawing.Point(103, 20);
            this.SendMsgTextbox.Name = "SendMsgTextbox";
            this.SendMsgTextbox.Size = new System.Drawing.Size(156, 20);
            this.SendMsgTextbox.TabIndex = 0;
            // 
            // PikaForm
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(971, 523);
            this.Controls.Add(this.BotControlContainer);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ConnectionCredentials);
            this.Name = "PikaForm";
            this.Text = "Pikatwo";
            this.Resize += new System.EventHandler(this.PikaFormResize);
            this.ConnectionCredentials.ResumeLayout(false);
            this.ConnectionCredentials.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.BotControlContainer.ResumeLayout(false);
            this.SendMsgContainer.ResumeLayout(false);
            this.SendMsgContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox ConnectionCredentials;
        private TextBox DefaultChannelTexbox;
        private Label label5;
        private TextBox UserPasswordTexbox;
        private Label label4;
        private TextBox UserNickTextbox;
        private Label label3;
        private Button LoadBut;
        private Button SaveBut;
        private TextBox ServerPortTexBox;
        private Label label2;
        private TextBox ServerAddressTexbox;
        private Label label1;
        private Button ConnectBut;
        private Button DisconnectBut;
        private GroupBox groupBox2;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private RichTextBox LogTextbox;
        private NotifyIcon notifyIcon1;
        private GroupBox groupBox3;
        private Label label6;
        private GroupBox BotControlContainer;
        private GroupBox SendMsgContainer;
        private TextBox SendMsgTextbox;
        private Button SendMsgButton;
        private ComboBox SendMsgCombobox;
        private Label label7;
        private Button JoinChannelBut;
        private Button PartChannelBut;
        private Button PingBut;
    }
}

