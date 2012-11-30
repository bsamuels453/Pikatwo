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
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.groupBox1 = new GroupBox();
            this.textBox5 = new TextBox();
            this.label5 = new Label();
            this.textBox4 = new TextBox();
            this.label4 = new Label();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.button2 = new Button();
            this.button1 = new Button();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(330, 108);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credentials";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Point(222, 33);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(100, 20);
            this.textBox5.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new Point(219, 17);
            this.label5.Name = "label5";
            this.label5.Size = new Size(83, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Default Channel";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Point(116, 77);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(100, 20);
            this.textBox4.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new Point(113, 61);
            this.label4.Name = "label4";
            this.label4.Size = new Size(78, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Nick Password";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Point(6, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(100, 20);
            this.textBox3.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new Size(55, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Nickname";
            // 
            // button2
            // 
            this.button2.Location = new Point(274, 73);
            this.button2.Name = "button2";
            this.button2.Size = new Size(44, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new Point(222, 74);
            this.button1.Name = "button1";
            this.button1.Size = new Size(46, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new Point(116, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(100, 20);
            this.textBox2.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(113, 17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(26, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Port";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Point(6, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 20);
            this.textBox1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(79, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Server Address";
            // 
            // PikaForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.ClientSize = new Size(778, 517);
            this.Controls.Add(this.groupBox1);
            this.Name = "PikaForm";
            this.Text = "Pikatwo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox1;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox3;
        private Label label3;
        private Button button2;
        private Button button1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox1;
        private Label label1;
    }
}

