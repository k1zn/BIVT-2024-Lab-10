namespace Lab_10
{
    partial class Form2
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            button1 = new Button();
            label5 = new Label();
            textBox5 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 27);
            label1.Name = "label1";
            label1.Size = new Size(199, 20);
            label1.TabIndex = 3;
            label1.Text = "Введите название лотереи:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 103);
            label2.Name = "label2";
            label2.Size = new Size(233, 20);
            label2.TabIndex = 4;
            label2.Text = "Введите количество участников:\r\n";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 180);
            label3.Name = "label3";
            label3.Size = new Size(212, 20);
            label3.TabIndex = 5;
            label3.Text = "Введите количество билетов:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 333);
            label4.Name = "label4";
            label4.Size = new Size(181, 20);
            label4.TabIndex = 7;
            label4.Text = "Введите призовой фонд:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(22, 50);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(212, 27);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(22, 126);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(38, 27);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(22, 203);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(38, 27);
            textBox3.TabIndex = 2;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(22, 356);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(107, 430);
            button1.Name = "button1";
            button1.Size = new Size(195, 29);
            button1.TabIndex = 9;
            button1.Text = "Запустить лотерею";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 257);
            label5.Name = "label5";
            label5.Size = new Size(157, 20);
            label5.TabIndex = 10;
            label5.Text = "Введите цену билета:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(22, 280);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(38, 27);
            textBox5.TabIndex = 11;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(407, 471);
            Controls.Add(textBox5);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(textBox4);
            Controls.Add(button1);
            MaximizeBox = false;
            Name = "Form2";
            Text = "Новая лотерея";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Label label4;
        private TextBox textBox4;
        private Label label5;
        private TextBox textBox5;
    }
}