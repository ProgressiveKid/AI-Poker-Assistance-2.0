namespace WindowsFormsControlLibrary.Components
{
    partial class PanelWithCards
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelPlayer = new System.Windows.Forms.Label();
            this.labelFirstCard = new System.Windows.Forms.Label();
            this.labelSecondCard = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(18, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(81, 101);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(120, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(78, 101);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // labelPlayer
            // 
            this.labelPlayer.AutoSize = true;
            this.labelPlayer.Location = new System.Drawing.Point(94, 30);
            this.labelPlayer.Name = "labelPlayer";
            this.labelPlayer.Size = new System.Drawing.Size(35, 13);
            this.labelPlayer.TabIndex = 3;
            this.labelPlayer.Text = "label1";
            // 
            // labelFirstCard
            // 
            this.labelFirstCard.AutoSize = true;
            this.labelFirstCard.Location = new System.Drawing.Point(43, 187);
            this.labelFirstCard.Name = "labelFirstCard";
            this.labelFirstCard.Size = new System.Drawing.Size(35, 13);
            this.labelFirstCard.TabIndex = 4;
            this.labelFirstCard.Text = "label2";
            // 
            // labelSecondCard
            // 
            this.labelSecondCard.AutoSize = true;
            this.labelSecondCard.Location = new System.Drawing.Point(141, 187);
            this.labelSecondCard.Name = "labelSecondCard";
            this.labelSecondCard.Size = new System.Drawing.Size(35, 13);
            this.labelSecondCard.TabIndex = 5;
            this.labelSecondCard.Text = "label3";
            // 
            // PanelWithCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSecondCard);
            this.Controls.Add(this.labelFirstCard);
            this.Controls.Add(this.labelPlayer);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PanelWithCards";
            this.Size = new System.Drawing.Size(225, 238);
            this.Load += new System.EventHandler(this.PanelWithCards_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelPlayer;
        private System.Windows.Forms.Label labelFirstCard;
        private System.Windows.Forms.Label labelSecondCard;
    }
}
