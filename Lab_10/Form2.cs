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
namespace Lab_10
{
    public partial class Form2 : Form
    {
     
        public Form2()
        {
            InitializeComponent();
            //надо еще попробовать потестить, не все перепробовал ещё варианты
        }

        private void ShowError(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

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
                ShowError("Укажите название лотереи");
                return;
            }
            if (!int.TryParse(textBox2.Text, out int countParticipants) || countParticipants <= 0)
            {
                ShowError("Указано некорректное число участников");
                return;
            }
            

            if (!int.TryParse(textBox3.Text, out int countTicket) || countTicket <= 0)
            {
                ShowError("Указано некорректное количество билетов");
                return;
            }
            if (!int.TryParse(textBox5.Text, out int TicketPrice) || TicketPrice < 0)
            {
                ShowError("Указана некорректная цена билета");
                return;
            }
            if (!int.TryParse(textBox4.Text, out int prizeFund) || prizeFund <= 0)
            {
                ShowError("Указан некорректный призовой фонд");
                return;
            }
            if (countParticipants > countTicket)
            {
                ShowError("Недостаточно билетов для проведения лотереи (количество участников превышает количество билетов)");
                return;
            }
            if (files.Length < countParticipants)
            {
                ShowError("Недостаточно участников в таблице участников");
                return;
            }



            string lotteryName = textBox1.Text;
            var Lottery = new LotteryEvent(lotteryName, countParticipants, countTicket, prizeFund, TicketPrice);
            Lottery.FillRandom();
            var winnerTicket = Lottery.GetWinner();
            string id = winnerTicket.TicketID;
            var participant = winnerTicket.Participant;
            string initials = participant.Initials;
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();  
            DateTime russiaDateTime = getRussiaDateTime(unixTimestampSeconds);
            serialize(Lottery, initials, id, unixTimestampSeconds, lotteryName);

            MessageBox.Show($"Победитель: {initials}{Environment.NewLine}ID выигрышного билета: {id}");

        }
        private DateTime getRussiaDateTime(long unixTimestampSeconds)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds);
            DateTime dateTime = dateTimeOffset.DateTime;
            TimeZoneInfo russiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, russiaTimeZone);
        }
        private void serialize<T>(T obj, string initials, string id, long unixTimestampSeconds, string lotteryName)
        {
            var jsonObj = JObject.FromObject(obj);
            jsonObj["Winner"] = initials;
            jsonObj["Ticket_ID"] = id;
            jsonObj["timestamp"] = getRussiaDateTime(unixTimestampSeconds);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "JSON");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, $"{lotteryName}_{unixTimestampSeconds}.json");
            File.WriteAllText(fullPath, jsonObj.ToString());
        }
    }
}
