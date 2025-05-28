using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_10
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "JSON");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] files = Directory.GetFiles(directoryPath);
            if (files.Length == 0 )
            {
                MessageBox.Show("Информация не найдена");
                return;
            }
            ClearTableLayoutPanel(tableLayoutPanel1);

            tableLayoutPanel1.SuspendLayout();

            foreach (string file in files)
            {

                if (string.IsNullOrEmpty(file))
                {
                    MessageBox.Show("Информация не найдена!!!");
                    return;
                }
                var jsonObj = JObject.Parse(File.ReadAllText(file));
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                var label1 = new Label
                {
                    Text = jsonObj["EventName"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var label2 = new Label
                {
                    Text = jsonObj["NumberOfParticipants"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var label3 = new Label
                {
                    Text = jsonObj["NumberOfTickets"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var label4 = new Label
                {
                    Text = jsonObj["PrizeFund"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var label5 = new Label
                {
                    Text = jsonObj["Winner"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var label6 = new Label
                {
                    Text = jsonObj["Ticket_ID"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var label7 = new Label
                {
                    Text = jsonObj["timestamp"].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                tableLayoutPanel1.Controls.Add(label1);
                tableLayoutPanel1.Controls.Add(label2);
                tableLayoutPanel1.Controls.Add(label3);
                tableLayoutPanel1.Controls.Add(label4);
                tableLayoutPanel1.Controls.Add(label5);
                tableLayoutPanel1.Controls.Add(label6);
                tableLayoutPanel1.Controls.Add(label7);
            }

            tableLayoutPanel1.ResumeLayout();


           
        }
        private void ClearTableLayoutPanel(TableLayoutPanel tableLayoutPanel)
        {
            tableLayoutPanel.Controls.Clear();

            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowCount = 0;
        }

    }
}
