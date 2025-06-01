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
            {
                return;
            }

            var content = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            JObject jsonObj = null;
            bool success = false;

            try
            {
                using (var jsonTextReader = new JsonTextReader(new StringReader(content)))
                {
                    jsonObj = JObject.Load(jsonTextReader);
                    success = true;
                }
            } catch (Exception)
            {
                ShowMsgBox($"Лотерея {Path.GetFileNameWithoutExtension(filePath)} не будет отображена из-за ошибки чтения", true);
            }

            if (!success || jsonObj == null) return;

            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            AddNewLabel(jsonObj["EventName"].ToString(), tableLayoutPanel1);
            AddNewLabel(jsonObj["NumberOfParticipants"].ToString(), tableLayoutPanel1);
            AddNewLabel(jsonObj["NumberOfTickets"].ToString(), tableLayoutPanel1);
            AddNewLabel(jsonObj["PrizeFund"].ToString(), tableLayoutPanel1);
            AddNewLabel(jsonObj["Winner"].ToString(), tableLayoutPanel1);
            AddNewLabel(jsonObj["TicketID"].ToString(), tableLayoutPanel1);
            AddNewLabel(jsonObj["Timestamp"].ToString(), tableLayoutPanel1);
        }

        private void ShowAllInfo(bool buttonCaller)
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
