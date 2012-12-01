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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.LogTextbox = new System.Windows.Forms.TextBox();
            this.Options = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Options.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DefaultChannelTexbox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.UserPasswordTexbox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.UserNickTextbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.LoadBut);
            this.groupBox1.Controls.Add(this.SaveBut);
            this.groupBox1.Controls.Add(this.ServerPortTexBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ServerAddressTexbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 108);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credentials";
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
            this.ConnectBut.Location = new System.Drawing.Point(12, 122);
            this.ConnectBut.Name = "ConnectBut";
            this.ConnectBut.Size = new System.Drawing.Size(58, 22);
            this.ConnectBut.TabIndex = 14;
            this.ConnectBut.Text = "Connect";
            this.ConnectBut.UseVisualStyleBackColor = true;
            this.ConnectBut.Click += new System.EventHandler(this.ConnectButClick);
            // 
            // DisconnectBut
            // 
            this.DisconnectBut.Location = new System.Drawing.Point(76, 121);
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
            this.groupBox2.Location = new System.Drawing.Point(348, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 136);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // LogTextbox
            // 
            this.LogTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogTextbox.Location = new System.Drawing.Point(3, 16);
            this.LogTextbox.Multiline = true;
            this.LogTextbox.Name = "LogTextbox";
            this.LogTextbox.ReadOnly = true;
            this.LogTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextbox.Size = new System.Drawing.Size(311, 117);
            this.LogTextbox.TabIndex = 0;
            this.LogTextbox.Text = "Hippo";
            this.LogTextbox.TextChanged += new System.EventHandler(this.LogBoxTextChanged);
            // 
            // Options
            // 
            this.Options.Controls.Add(this.checkBox3);
            this.Options.Controls.Add(this.checkBox2);
            this.Options.Controls.Add(this.checkBox1);
            this.Options.Location = new System.Drawing.Point(495, 150);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(170, 265);
            this.Options.TabIndex = 17;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(6, 65);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(119, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Replying To People";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(6, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(159, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Replying To Aux Commands";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Logging";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(12, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 265);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fun Stuff";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(680, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
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
            // PikaForm
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(680, 448);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DisconnectBut);
            this.Controls.Add(this.ConnectBut);
            this.Controls.Add(this.groupBox1);
            this.Name = "PikaForm";
            this.Text = "Pikatwo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox1;
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
        private GroupBox Options;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private GroupBox groupBox3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private TextBox LogTextbox;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
    }
}

