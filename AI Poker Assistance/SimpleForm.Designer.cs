namespace AI_Poker_Assistance
{
    partial class SimpleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StreetGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flopbutton = new System.Windows.Forms.Button();
            this.turnbutton = new System.Windows.Forms.Button();
            this.riverbutton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.CurrentBankLabel = new System.Windows.Forms.Label();
            this.StreetGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // StreetGroupBox
            // 
            this.StreetGroupBox.Controls.Add(this.riverbutton);
            this.StreetGroupBox.Controls.Add(this.turnbutton);
            this.StreetGroupBox.Controls.Add(this.flopbutton);
            this.StreetGroupBox.Location = new System.Drawing.Point(489, 6);
            this.StreetGroupBox.Name = "StreetGroupBox";
            this.StreetGroupBox.Size = new System.Drawing.Size(140, 106);
            this.StreetGroupBox.TabIndex = 1;
            this.StreetGroupBox.TabStop = false;
            this.StreetGroupBox.Text = "Street";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox5);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(489, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 142);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kards";
            // 
            // flopbutton
            // 
            this.flopbutton.Location = new System.Drawing.Point(6, 19);
            this.flopbutton.Name = "flopbutton";
            this.flopbutton.Size = new System.Drawing.Size(115, 23);
            this.flopbutton.TabIndex = 0;
            this.flopbutton.Text = "Flop";
            this.flopbutton.UseVisualStyleBackColor = true;
            // 
            // turnbutton
            // 
            this.turnbutton.Location = new System.Drawing.Point(6, 48);
            this.turnbutton.Name = "turnbutton";
            this.turnbutton.Size = new System.Drawing.Size(115, 23);
            this.turnbutton.TabIndex = 1;
            this.turnbutton.Text = "Turn";
            this.turnbutton.UseVisualStyleBackColor = true;
            // 
            // riverbutton
            // 
            this.riverbutton.Location = new System.Drawing.Point(6, 77);
            this.riverbutton.Name = "riverbutton";
            this.riverbutton.Size = new System.Drawing.Size(115, 23);
            this.riverbutton.TabIndex = 2;
            this.riverbutton.Text = "River";
            this.riverbutton.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(21, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 81);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(88, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(61, 81);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(155, 31);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(61, 81);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(222, 31);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(61, 81);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(289, 31);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(61, 81);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // CurrentBankLabel
            // 
            this.CurrentBankLabel.AutoSize = true;
            this.CurrentBankLabel.Location = new System.Drawing.Point(644, 34);
            this.CurrentBankLabel.Name = "CurrentBankLabel";
            this.CurrentBankLabel.Size = new System.Drawing.Size(72, 13);
            this.CurrentBankLabel.TabIndex = 3;
            this.CurrentBankLabel.Text = "CurrentBank: ";
            this.CurrentBankLabel.TextChanged += new System.EventHandler(this.CurrentBankLabel_TextChanged);
            this.CurrentBankLabel.Click += new System.EventHandler(this.CurrentBankLabel_Click);
            // 
            // SimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 569);
            this.Controls.Add(this.CurrentBankLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StreetGroupBox);
            this.Name = "SimpleForm";
            this.Text = "SimpleForm";
            this.Load += new System.EventHandler(this.SimpleForm_Load);
            this.StreetGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox StreetGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button riverbutton;
        private System.Windows.Forms.Button turnbutton;
        private System.Windows.Forms.Button flopbutton;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label CurrentBankLabel;
    }
}