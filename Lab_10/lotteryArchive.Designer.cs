namespace Lab_10
{
    partial class lotteryArchive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lotteryArchive));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Honeydew;
            button1.Location = new Point(179, 290);
            button1.Name = "button1";
            button1.Size = new Size(251, 81);
            button1.TabIndex = 0;
            button1.Text = "Новая лотерея";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.GradientInactiveCaption;
            button2.Location = new Point(630, 290);
            button2.Name = "button2";
            button2.Size = new Size(251, 81);
            button2.TabIndex = 1;
            button2.Text = "Посмотреть статистику";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.MistyRose;
            button3.ForeColor = SystemColors.ControlText;
            button3.Location = new Point(400, 415);
            button3.Name = "button3";
            button3.Size = new Size(251, 81);
            button3.TabIndex = 2;
            button3.Text = "Таблица участников";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources._26da1392_5950_4bb9_a844_7fa6c91ae02e1;
            pictureBox1.Location = new Point(219, 29);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(618, 182);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // lotteryArchive
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(1062, 626);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            MaximizeBox = false;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "lotteryArchive";
            Text = "ООПЛото";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
    }
}