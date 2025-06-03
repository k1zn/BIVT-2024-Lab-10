using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model;
using Model.Data;
using Model.Core;
namespace Lab_10
{
    public partial class LotteryCreate : MyForm
    {
     
        public LotteryCreate()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public static event EventHandler<MyForm.LotteryPathEventArgs> LotteryCreated;

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var files = Directory.GetFiles(path);
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                ShowMsgBox("Укажите название лотереи", false);
                textBox1.Focus();
                return;
            }
            if (!int.TryParse(textBox2.Text, out int lotteryParticipantsCount) || lotteryParticipantsCount <= 0)
            {
                ShowMsgBox("Указано некорректное число участников", false);
                textBox2.Focus();
                return;
            }
            

            if (!int.TryParse(textBox3.Text, out int countTicket) || countTicket <= 0)
            {
                ShowMsgBox("Указано некорректное количество билетов", false);
                textBox3.Focus();
                return;
            }
            if (!int.TryParse(textBox4.Text, out int TicketPrice) || TicketPrice <= 0)
            {
                ShowMsgBox("Указана некорректная цена билета", false);
                textBox4.Focus();
                return;
            }
            if (!int.TryParse(textBox5.Text, out int prizeFund) || prizeFund <= 0)
            {
                ShowMsgBox("Указан некорректный призовой фонд", false);
                textBox5.Focus();
                return;
            }
            if (lotteryParticipantsCount > countTicket)
            {
                ShowMsgBox("Недостаточно билетов для проведения лотереи (количество участников превышает количество билетов)", false);
                textBox3.Focus();
                return;
            }
            if (files.Length < lotteryParticipantsCount)
            {
                ShowMsgBox("Недостаточно участников в таблице участников", false);
                return;
            }

            string lotteryName = textBox1.Text;

            var Lottery = new LotteryEvent(lotteryName, countTicket, lotteryParticipantsCount, prizeFund, TicketPrice);
            Lottery.FillRandom();

            var winnerTicket = Lottery.GetWinner();
            if (winnerTicket == null)
            {
                ShowMsgBox("В данной лотерее нет победителей. Возможная причина: у выбранных участников не хватает денег на покупку билета", false);
                return;
            }

            string id = winnerTicket.TicketID;
            var participant = winnerTicket.Participant;
            string fullName = participant.FullName;
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var serializer = new LotteryArchiveJSONSerializer();
            serializer.SelectFolder(Path.Combine(Directory.GetCurrentDirectory(), "JSON"));
            serializer.SelectFile($"{Lottery.EventName}_{unixTimestampSeconds}");

            LotteryCreated?.Invoke(this, new MyForm.LotteryPathEventArgs { LotteryPath = serializer.FilePath });

            MessageBox.Show($"Победитель: {fullName}{Environment.NewLine}ID выигрышного билета: {id}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private DateTime getRussiaDateTime(long unixTimestampSeconds)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds);
            DateTime dateTime = dateTimeOffset.DateTime;
            TimeZoneInfo russiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, russiaTimeZone);
        }
    }
}
