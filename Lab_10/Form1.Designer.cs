namespace Lab_10
{
    partial class Form1
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            label7 = new Label();
            label8 = new Label();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.Control;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Cursor = Cursors.No;
            tableLayoutPanel1.Location = new Point(13, 94);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1269, 470);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.Location = new Point(547, 1);
            label4.Name = "label4";
            label4.Size = new Size(174, 23);
            label4.TabIndex = 3;
            label4.Text = "Фонд";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Location = new Point(728, 1);
            label5.Name = "label5";
            label5.Size = new Size(174, 23);
            label5.TabIndex = 4;
            label5.Text = "Победитель";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Location = new Point(909, 1);
            label6.Name = "label6";
            label6.Size = new Size(174, 23);
            label6.TabIndex = 5;
            label6.Text = "ID билета";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Location = new Point(4, 1);
            label1.Name = "label1";
            label1.Size = new Size(174, 23);
            label1.TabIndex = 0;
            label1.Text = "Название лотереи";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(185, 1);
            label2.Name = "label2";
            label2.Size = new Size(174, 23);
            label2.TabIndex = 1;
            label2.Text = "Количество участников";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Location = new Point(366, 1);
            label3.Name = "label3";
            label3.Size = new Size(174, 23);
            label3.TabIndex = 2;
            label3.Text = "Количество билетов";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Location = new Point(510, 570);
            button1.Name = "button1";
            button1.Size = new Size(200, 38);
            button1.TabIndex = 1;
            button1.Text = "Обновить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel2.ColumnCount = 7;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label8, 6, 0);
            tableLayoutPanel2.Controls.Add(label6, 5, 0);
            tableLayoutPanel2.Controls.Add(label5, 4, 0);
            tableLayoutPanel2.Controls.Add(label4, 3, 0);
            tableLayoutPanel2.Controls.Add(label3, 2, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(label2, 1, 0);
            tableLayoutPanel2.Location = new Point(13, 56);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1269, 36);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // label8
            // 
            label8.Location = new Point(1090, 1);
            label8.Name = "label8";
            label8.Size = new Size(174, 23);
            label8.TabIndex = 6;
            label8.Text = "Время проведения";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1294, 642);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(button1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label7);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label8;
        private Label label7;
    }
}