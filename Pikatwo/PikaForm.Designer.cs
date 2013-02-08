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
            this.BotControlContainer = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CompileLogsBut = new System.Windows.Forms.Button();
            this.JoinChannelBut = new System.Windows.Forms.Button();
            this.PartChannelBut = new System.Windows.Forms.Button();
            this.PingBut = new System.Windows.Forms.Button();
            this.SendMsgContainer = new System.Windows.Forms.GroupBox();
            this.SendMsgButton = new System.Windows.Forms.Button();
            this.SendMsgCombobox = new System.Windows.Forms.ComboBox();
            this.SendMsgTextbox = new System.Windows.Forms.TextBox();
            this.ConnectionCredentials.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.BotControlContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox2.Location = new System.Drawing.Point(12, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 389);
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
            this.LogTextbox.Size = new System.Drawing.Size(316, 370);
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
            // BotControlContainer
            // 
            this.BotControlContainer.BackColor = System.Drawing.SystemColors.Control;
            this.BotControlContainer.Controls.Add(this.groupBox1);
            this.BotControlContainer.Controls.Add(this.CompileLogsBut);
            this.BotControlContainer.Controls.Add(this.JoinChannelBut);
            this.BotControlContainer.Controls.Add(this.PartChannelBut);
            this.BotControlContainer.Controls.Add(this.PingBut);
            this.BotControlContainer.Controls.Add(this.SendMsgContainer);
            this.BotControlContainer.Controls.Add(this.ConnectBut);
            this.BotControlContainer.Controls.Add(this.DisconnectBut);
            this.BotControlContainer.Location = new System.Drawing.Point(348, 12);
            this.BotControlContainer.Name = "BotControlContainer";
            this.BotControlContainer.Size = new System.Drawing.Size(273, 499);
            this.BotControlContainer.TabIndex = 18;
            this.BotControlContainer.TabStop = false;
            this.BotControlContainer.Text = "Bot Control";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button13);
            this.groupBox1.Controls.Add(this.button14);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button12);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 222);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 216);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feature";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Chat Logging";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Chat Responding";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "NOTIMPLEMENTED";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "NOTIMPLEMENTED";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "NOTIMPLEMENTED";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "NOTIMPLEMENTED";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "NOTIMPLEMENTED";
            // 
            // button13
            // 
            this.button13.Enabled = false;
            this.button13.Location = new System.Drawing.Point(211, 13);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(33, 23);
            this.button13.TabIndex = 24;
            this.button13.Text = "Off";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Enabled = false;
            this.button14.Location = new System.Drawing.Point(175, 13);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(30, 23);
            this.button14.TabIndex = 23;
            this.button14.Text = "On";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(211, 42);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(33, 23);
            this.button11.TabIndex = 20;
            this.button11.Text = "Off";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(211, 71);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(33, 23);
            this.button9.TabIndex = 20;
            this.button9.Text = "Off";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(175, 42);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(30, 23);
            this.button12.TabIndex = 19;
            this.button12.Text = "On";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(211, 100);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(33, 23);
            this.button7.TabIndex = 22;
            this.button7.Text = "Off";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(175, 71);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(30, 23);
            this.button10.TabIndex = 19;
            this.button10.Text = "On";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(175, 100);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(30, 23);
            this.button8.TabIndex = 21;
            this.button8.Text = "On";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(211, 129);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(33, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "Off";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(211, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(33, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Off";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(175, 129);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(30, 23);
            this.button6.TabIndex = 19;
            this.button6.Text = "On";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(175, 158);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "On";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(211, 187);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Off";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(175, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "On";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CompileLogsBut
            // 
            this.CompileLogsBut.Location = new System.Drawing.Point(9, 95);
            this.CompileLogsBut.Name = "CompileLogsBut";
            this.CompileLogsBut.Size = new System.Drawing.Size(82, 23);
            this.CompileLogsBut.TabIndex = 20;
            this.CompileLogsBut.Text = "Compile Logs";
            this.CompileLogsBut.UseVisualStyleBackColor = true;
            this.CompileLogsBut.Click += new System.EventHandler(this.CompileLogsButClick);
            // 
            // JoinChannelBut
            // 
            this.JoinChannelBut.Location = new System.Drawing.Point(145, 47);
            this.JoinChannelBut.Name = "JoinChannelBut";
            this.JoinChannelBut.Size = new System.Drawing.Size(81, 23);
            this.JoinChannelBut.TabIndex = 19;
            this.JoinChannelBut.Text = "Join Channel";
            this.JoinChannelBut.UseVisualStyleBackColor = true;
            this.JoinChannelBut.Click += new System.EventHandler(this.JoinChannelButClick);
            // 
            // PartChannelBut
            // 
            this.PartChannelBut.Location = new System.Drawing.Point(59, 47);
            this.PartChannelBut.Name = "PartChannelBut";
            this.PartChannelBut.Size = new System.Drawing.Size(80, 23);
            this.PartChannelBut.TabIndex = 18;
            this.PartChannelBut.Text = "Part Channel";
            this.PartChannelBut.UseVisualStyleBackColor = true;
            this.PartChannelBut.Click += new System.EventHandler(this.PartChannelButClick);
            // 
            // PingBut
            // 
            this.PingBut.Location = new System.Drawing.Point(9, 47);
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
            this.SendMsgContainer.Location = new System.Drawing.Point(6, 444);
            this.SendMsgContainer.Name = "SendMsgContainer";
            this.SendMsgContainer.Size = new System.Drawing.Size(259, 49);
            this.SendMsgContainer.TabIndex = 16;
            this.SendMsgContainer.TabStop = false;
            this.SendMsgContainer.Text = "Send Msg";
            // 
            // SendMsgButton
            // 
            this.SendMsgButton.Location = new System.Drawing.Point(206, 18);
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
            this.SendMsgTextbox.Size = new System.Drawing.Size(98, 20);
            this.SendMsgTextbox.TabIndex = 0;
            // 
            // PikaForm
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(629, 523);
            this.Controls.Add(this.BotControlContainer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ConnectionCredentials);
            this.Name = "PikaForm";
            this.Text = "Pikatwo";
            this.Resize += new System.EventHandler(this.PikaFormResize);
            this.ConnectionCredentials.ResumeLayout(false);
            this.ConnectionCredentials.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.BotControlContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private GroupBox BotControlContainer;
        private GroupBox SendMsgContainer;
        private TextBox SendMsgTextbox;
        private Button SendMsgButton;
        private ComboBox SendMsgCombobox;
        private Button JoinChannelBut;
        private Button PartChannelBut;
        private Button PingBut;
        private Button CompileLogsBut;
        private GroupBox groupBox1;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Button button13;
        private Button button14;
        private Button button11;
        private Button button9;
        private Button button12;
        private Button button7;
        private Button button10;
        private Button button8;
        private Button button5;
        private Button button3;
        private Button button6;
        private Button button4;
        private Button button2;
        private Button button1;
    }
}

