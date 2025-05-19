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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Honeydew;
            button1.Location = new Point(175, 258);
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
            button2.Location = new Point(626, 258);
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
            button3.Location = new Point(396, 383);
            button3.Name = "button3";
            button3.Size = new Size(251, 81);
            button3.TabIndex = 2;
            button3.Text = "Таблица участников";
            button3.UseVisualStyleBackColor = false;
            // 
            // lotteryArchive
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(1062, 626);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "lotteryArchive";
            Text = "lotteryArchive";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
    }
}