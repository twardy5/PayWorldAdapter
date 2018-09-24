namespace Test
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.MessageTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TCPIPCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.StopRecivingBtn = new System.Windows.Forms.Button();
            this.StartReciveBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PIN PAD addr:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PIN PAD port:";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConnectBtn.Location = new System.Drawing.Point(607, 522);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 4;
            this.ConnectBtn.Text = "Send";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // MessageTB
            // 
            this.MessageTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageTB.Location = new System.Drawing.Point(118, 167);
            this.MessageTB.Name = "MessageTB";
            this.MessageTB.Size = new System.Drawing.Size(557, 331);
            this.MessageTB.TabIndex = 5;
            this.MessageTB.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "XML message";
            // 
            // TCPIPCheckBox
            // 
            this.TCPIPCheckBox.AutoSize = true;
            this.TCPIPCheckBox.Location = new System.Drawing.Point(118, 121);
            this.TCPIPCheckBox.Name = "TCPIPCheckBox";
            this.TCPIPCheckBox.Size = new System.Drawing.Size(60, 17);
            this.TCPIPCheckBox.TabIndex = 7;
            this.TCPIPCheckBox.Text = "TCP IP";
            this.TCPIPCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(118, 144);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(88, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Web Service";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // StopRecivingBtn
            // 
            this.StopRecivingBtn.Location = new System.Drawing.Point(263, 521);
            this.StopRecivingBtn.Name = "StopRecivingBtn";
            this.StopRecivingBtn.Size = new System.Drawing.Size(115, 23);
            this.StopRecivingBtn.TabIndex = 9;
            this.StopRecivingBtn.Text = "Stop Reciving";
            this.StopRecivingBtn.UseVisualStyleBackColor = true;
            this.StopRecivingBtn.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StartReciveBtn
            // 
            this.StartReciveBtn.Location = new System.Drawing.Point(118, 521);
            this.StartReciveBtn.Name = "StartReciveBtn";
            this.StartReciveBtn.Size = new System.Drawing.Size(139, 23);
            this.StartReciveBtn.TabIndex = 10;
            this.StartReciveBtn.Text = "Start listing";
            this.StartReciveBtn.UseVisualStyleBackColor = true;
            this.StartReciveBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(396, 521);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Send Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "192.168.2.96",
            "10.0.0.22",
            "5.1.113.136",
            "10.0.0.36",
            "127.0.0.1"});
            this.comboBox1.Location = new System.Drawing.Point(118, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(280, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "192.168.2.96";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "50000",
            "61613"});
            this.comboBox2.Location = new System.Drawing.Point(118, 75);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(280, 21);
            this.comboBox2.TabIndex = 13;
            this.comboBox2.Text = "50000";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "50000",
            "61613"});
            this.comboBox3.Location = new System.Drawing.Point(510, 75);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(146, 21);
            this.comboBox3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(428, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Listinng port:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 522);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "SendByHTTP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.Location = new System.Drawing.Point(517, 521);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Send Amount";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 557);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartReciveBtn);
            this.Controls.Add(this.StopRecivingBtn);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.TCPIPCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MessageTB);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "PayWorld";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.RichTextBox MessageTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox TCPIPCheckBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button StopRecivingBtn;
        private System.Windows.Forms.Button StartReciveBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

