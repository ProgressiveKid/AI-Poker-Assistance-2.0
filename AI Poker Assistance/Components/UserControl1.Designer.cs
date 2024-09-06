namespace WindowsFormsControlLibrary
{
    partial class UserControl1
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
            this.positionlabel = new System.Windows.Forms.Label();
            this.nametexbox = new System.Windows.Forms.TextBox();
            this.cardsButton = new System.Windows.Forms.Button();
            this.stacklabel = new System.Windows.Forms.Label();
            this.UserActions = new System.Windows.Forms.GroupBox();
            this.raisesumTextBox = new System.Windows.Forms.TextBox();
            this.raisebutton = new System.Windows.Forms.Button();
            this.foldbutton = new System.Windows.Forms.Button();
            this.callbutton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.currentactionlabel = new System.Windows.Forms.Label();
            this.currectbet = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.indicatorOfTurn = new System.Windows.Forms.Panel();
            this.UserActions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // positionlabel
            // 
            this.positionlabel.AutoSize = true;
            this.positionlabel.Location = new System.Drawing.Point(16, 31);
            this.positionlabel.Name = "positionlabel";
            this.positionlabel.Size = new System.Drawing.Size(35, 13);
            this.positionlabel.TabIndex = 0;
            this.positionlabel.Text = "label1";
            this.positionlabel.Click += new System.EventHandler(this.positionlabel_Click);
            // 
            // nametexbox
            // 
            this.nametexbox.Location = new System.Drawing.Point(70, 15);
            this.nametexbox.Name = "nametexbox";
            this.nametexbox.Size = new System.Drawing.Size(100, 20);
            this.nametexbox.TabIndex = 1;
            // 
            // cardsButton
            // 
            this.cardsButton.Location = new System.Drawing.Point(83, 41);
            this.cardsButton.Name = "cardsButton";
            this.cardsButton.Size = new System.Drawing.Size(75, 23);
            this.cardsButton.TabIndex = 3;
            this.cardsButton.Text = "Cards";
            this.cardsButton.UseVisualStyleBackColor = true;
            this.cardsButton.Click += new System.EventHandler(this.notebutton_Click);
            // 
            // stacklabel
            // 
            this.stacklabel.AutoSize = true;
            this.stacklabel.Location = new System.Drawing.Point(186, 10);
            this.stacklabel.Name = "stacklabel";
            this.stacklabel.Size = new System.Drawing.Size(55, 13);
            this.stacklabel.TabIndex = 4;
            this.stacklabel.Text = "stacklabel";
            // 
            // UserActions
            // 
            this.UserActions.Controls.Add(this.raisesumTextBox);
            this.UserActions.Controls.Add(this.raisebutton);
            this.UserActions.Controls.Add(this.foldbutton);
            this.UserActions.Controls.Add(this.callbutton);
            this.UserActions.Location = new System.Drawing.Point(180, 27);
            this.UserActions.Name = "UserActions";
            this.UserActions.Size = new System.Drawing.Size(185, 42);
            this.UserActions.TabIndex = 5;
            this.UserActions.TabStop = false;
            this.UserActions.Text = "Actions";
            // 
            // raisesumTextBox
            // 
            this.raisesumTextBox.Location = new System.Drawing.Point(9, 16);
            this.raisesumTextBox.Name = "raisesumTextBox";
            this.raisesumTextBox.Size = new System.Drawing.Size(100, 20);
            this.raisesumTextBox.TabIndex = 3;
            this.raisesumTextBox.Visible = false;
            // 
            // raisebutton
            // 
            this.raisebutton.Location = new System.Drawing.Point(127, 14);
            this.raisebutton.Name = "raisebutton";
            this.raisebutton.Size = new System.Drawing.Size(53, 21);
            this.raisebutton.TabIndex = 2;
            this.raisebutton.Text = "raise";
            this.raisebutton.UseVisualStyleBackColor = true;
            this.raisebutton.Click += new System.EventHandler(this.raisebutton_Click);
            // 
            // foldbutton
            // 
            this.foldbutton.Location = new System.Drawing.Point(68, 15);
            this.foldbutton.Name = "foldbutton";
            this.foldbutton.Size = new System.Drawing.Size(53, 21);
            this.foldbutton.TabIndex = 1;
            this.foldbutton.Text = "fold";
            this.foldbutton.UseVisualStyleBackColor = true;
            this.foldbutton.Click += new System.EventHandler(this.fold_Click);
            // 
            // callbutton
            // 
            this.callbutton.Location = new System.Drawing.Point(9, 15);
            this.callbutton.Name = "callbutton";
            this.callbutton.Size = new System.Drawing.Size(53, 21);
            this.callbutton.TabIndex = 0;
            this.callbutton.Text = "call";
            this.callbutton.UseVisualStyleBackColor = true;
            this.callbutton.Click += new System.EventHandler(this.callbutton_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(70, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 10);
            this.panel1.TabIndex = 7;
            // 
            // currentactionlabel
            // 
            this.currentactionlabel.AutoSize = true;
            this.currentactionlabel.Location = new System.Drawing.Point(8, 16);
            this.currentactionlabel.Name = "currentactionlabel";
            this.currentactionlabel.Size = new System.Drawing.Size(0, 13);
            this.currentactionlabel.TabIndex = 9;
            // 
            // currectbet
            // 
            this.currectbet.AutoSize = true;
            this.currectbet.Location = new System.Drawing.Point(8, 39);
            this.currectbet.Name = "currectbet";
            this.currectbet.Size = new System.Drawing.Size(0, 13);
            this.currectbet.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.currectbet);
            this.groupBox1.Controls.Add(this.currentactionlabel);
            this.groupBox1.Location = new System.Drawing.Point(366, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(56, 57);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "History";
            // 
            // indicatorOfTurn
            // 
            this.indicatorOfTurn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.indicatorOfTurn.Location = new System.Drawing.Point(424, 26);
            this.indicatorOfTurn.Name = "indicatorOfTurn";
            this.indicatorOfTurn.Size = new System.Drawing.Size(31, 28);
            this.indicatorOfTurn.TabIndex = 8;
            this.indicatorOfTurn.Visible = false;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.indicatorOfTurn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UserActions);
            this.Controls.Add(this.stacklabel);
            this.Controls.Add(this.cardsButton);
            this.Controls.Add(this.nametexbox);
            this.Controls.Add(this.positionlabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(467, 68);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.UserActions.ResumeLayout(false);
            this.UserActions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label positionlabel;
        public System.Windows.Forms.TextBox nametexbox;
        public System.Windows.Forms.Button cardsButton;
        public System.Windows.Forms.Label stacklabel;
        public System.Windows.Forms.GroupBox UserActions;
        public System.Windows.Forms.Button raisebutton;
        public System.Windows.Forms.Button foldbutton;
        public System.Windows.Forms.Button callbutton;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox raisesumTextBox;
        public System.Windows.Forms.Label currentactionlabel;
        public System.Windows.Forms.Label currectbet;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Panel indicatorOfTurn;
    }
}
