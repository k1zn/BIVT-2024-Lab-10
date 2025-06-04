using Model.Core;
using Model.Data;
using Newtonsoft.Json;
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
            ShowAllInfo(false);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            LotteryCreate.LotteryCreated += HandleNewLottery;
        }

        private void HandleNewLottery(object sender, MyForm.LotteryPathEventArgs e)
        {
            AddOneFileToTable(e.LotteryPath);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            LotteryCreate.LotteryCreated -= HandleNewLottery;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAllInfo(true);
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

        private void AddNewLabel(string text, TableLayoutPanel tableLayoutPanel)
        {
            var label = new Label
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            tableLayoutPanel.Controls.Add(label);
        }

        private void AddOneFileToTable(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            string content = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(content))
            {
                if (this.IsHandleCreated)
                    ShowMsgBox($"Лотерея {Path.GetFileNameWithoutExtension(filePath)} не будет отображена — файл пуст", true);
                return;
            }

            LotteryArchiveSerializer serializer;

            if (filePath.EndsWith(".json"))
                serializer = new LotteryArchiveJSONSerializer();
            else if (filePath.EndsWith(".xml"))
                serializer = new LotteryArchiveXMLSerializer();
            else
                return;

            serializer.SelectFolder(Path.GetDirectoryName(filePath));
            serializer.SelectFile(Path.GetFileNameWithoutExtension(filePath));
            
            LotteryEvent lottery;
            try
            {
                lottery = serializer.DeserializeLottery();
            }
            catch
            {
                ShowMsgBox($"Лотерея {Path.GetFileNameWithoutExtension(filePath)} не будет отображена — ошибка при чтении", true);
                return;
            }

            if (lottery == null)
                return;

            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            string ticketID = lottery.WinnerTicket?.TicketID ?? "-1";
            string winner = lottery.WinnerParticipant?.FullName ?? "отсутствует";
            string timestamp = (filePath.EndsWith(".json"))
                ? JObject.Parse(content)["Timestamp"]?.ToString() ?? "-"
                : File.GetLastWriteTime(filePath).ToString("yyyy-MM-dd HH:mm:ss");

            if (ticketID == "-1")
            {
                winner = "отсутствует";
                ticketID = "отменена";
                timestamp = "-";
            }

            AddNewLabel(lottery.EventName, tableLayoutPanel1);
            AddNewLabel(lottery.NumberOfParticipants.ToString(), tableLayoutPanel1);
            AddNewLabel(lottery.NumberOfTickets.ToString(), tableLayoutPanel1);
            AddNewLabel(lottery.PrizeFund.ToString(), tableLayoutPanel1);
            AddNewLabel(winner, tableLayoutPanel1);
            AddNewLabel(ticketID, tableLayoutPanel1);
            AddNewLabel(timestamp, tableLayoutPanel1);
        }

        private void ShowAllInfo(bool buttonCaller)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Lotteries");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] files = Directory.GetFiles(directoryPath);
            if (files.Length == 0)
            {
                if (buttonCaller)
                {
                    ShowMsgBox("Информация о проведенных лотереях не найдена", false);
                    ClearTableLayoutPanel(tableLayoutPanel1);
                }

                return;
            }

            tableLayoutPanel1.SuspendLayout();


            ClearTableLayoutPanel(tableLayoutPanel1);

            foreach (string file in files)
            {
                AddOneFileToTable(file);
            }

            tableLayoutPanel1.ResumeLayout(true);
        }
    }
}
