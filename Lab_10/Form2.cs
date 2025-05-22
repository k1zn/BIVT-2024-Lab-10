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
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите название Лотереи!!!");
                return;
            }
            //int countParticipants, countTicket, prizeFund;
            if (!int.TryParse(textBox2.Text, out int countParticipants) || countParticipants <= 0)
            {
                MessageBox.Show("Некорректное количество участников!");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int countTicket) || countTicket <= 0)
            {
                MessageBox.Show("Некорректное количество билетов!");
                return;
            }

            if (!int.TryParse(textBox4.Text, out int prizeFund) || prizeFund <= 0)
            {
                MessageBox.Show("Некорректный призовой фонд!");
                return;
            }

            string lotteryName = textBox1.Text;
            var Lottery = new LotteryEvent(lotteryName, countParticipants, countTicket, prizeFund);
            Lottery.FillRandom();
            var winnerTicket = Lottery.GetWinner();
            string id = winnerTicket.TicketID;
            var participant = winnerTicket.Participant;
            string initials = participant.Initials;
            
            var jsonObj = JObject.FromObject(Lottery);
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds);
            DateTime dateTime = dateTimeOffset.DateTime;
            TimeZoneInfo russiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            DateTime russiaDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, russiaTimeZone);
            MessageBox.Show(russiaDateTime.ToString());
            jsonObj["Winner"] = initials;
            jsonObj["Ticket_ID"] = id;
            jsonObj["timestamp"] = russiaDateTime.ToString();
            string path = @"C:\Users\user\Documents\GitHub\BIVT-2024-Lab-10\Lab_10\JSON";
            string fullPath = Path.Combine(path, $"{lotteryName}_{unixTimestampSeconds}.txt"); 
            File.WriteAllText(fullPath, jsonObj.ToString());

            MessageBox.Show($"Победитель: {initials}{Environment.NewLine}ID выигрышного билета: {id}");

        }
    }
}
