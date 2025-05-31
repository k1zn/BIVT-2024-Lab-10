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
    public partial class LotteryStats : MyForm
    {

        public LotteryStats()
        {
            InitializeComponent();
            ShowInfo(false);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShowInfo(true);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;
        private void ClearTableLayoutPanel(TableLayoutPanel tableLayoutPanel)
        {
            SendMessage(tableLayoutPanel1.Handle, WM_SETREDRAW, false, 0);

            tableLayoutPanel.Controls.Clear();

            tableLayoutPanel.RowStyles.Clear();

            tableLayoutPanel.RowCount = 0;

            SendMessage(tableLayoutPanel1.Handle, WM_SETREDRAW, true, 0);
        }

        private void ShowInfo(bool buttonCaller)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "JSON");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] files = Directory.GetFiles(directoryPath);
            if (files.Length == 0)
            {
                if (buttonCaller)
                {
                    ShowMsgBox("Информация не найдена (#1)", false);
                    ClearTableLayoutPanel(tableLayoutPanel1);
                }
                    
                return;
            }

            tableLayoutPanel1.SuspendLayout();
               
            
            ClearTableLayoutPanel(tableLayoutPanel1);

            foreach (string file in files)
            {

                if (string.IsNullOrEmpty(file))
                {
                    if (buttonCaller)
                        ShowMsgBox("Информация не найдена (#2)", false);
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

            tableLayoutPanel1.ResumeLayout(true);
        }

    }
}
