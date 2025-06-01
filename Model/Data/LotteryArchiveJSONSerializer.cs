using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Data
{
    public class LotteryArchiveJSONSerializer : LotteryArchiveSerializer
    {
        public override string Extension => "json";

        private DateTime getRussiaDateTime(long unixTimestampSeconds)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds);
            DateTime dateTime = dateTimeOffset.DateTime;
            TimeZoneInfo russiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, russiaTimeZone);
        }

        public override string SerializeLottery(LotteryEvent e)
        {
            if (e == null) return "";

            var winnerTicket = e.GetWinner();

            string initials;
            string winnerTicketID;

            if (winnerTicket == null)
            {
                initials = "-";
                winnerTicketID = "-";

            } else
            {
                initials = winnerTicket.Participant.Initials;
                winnerTicketID = winnerTicket.TicketID;
            }
               
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var jsonObj = JObject.FromObject(e);
            jsonObj["Winner"] = initials;
            jsonObj["TicketID"] = winnerTicketID;
            jsonObj["Timestamp"] = getRussiaDateTime(unixTimestampSeconds);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "JSON");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, $"{e.EventName}_{unixTimestampSeconds}.json");
            File.WriteAllText(fullPath, jsonObj.ToString());

            return fullPath;
        }

        public override string SerializeLotteryParticipant(LotteryParticipant participant)
        {
            if (participant == null) return "";

            var jsonObj = JObject.FromObject(participant);
            jsonObj["Tickets"] = SerializeLotteryTicket(participant.Tickets);
            jsonObj["PassportInfo"] = participant.GetPassportInfo("admin");

            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            string fullPath = Path.Combine(folderPath, $"Participant_{participant.Initials}_{participant.GetPassportInfo("admin")}.json");

            File.WriteAllText(fullPath, jsonObj.ToString());

            return fullPath;
        }

        public override JArray SerializeLotteryTicket(LotteryTicket ticket)
        {
            if (ticket == null) return new JArray();

            JObject jsonObj = JObject.FromObject(ticket);

            return new JArray(jsonObj);
        }

        public override JArray SerializeLotteryTicket(LotteryTicket[] tickets)
        {
            var jArray = new JArray();
            if (tickets == null) return jArray;
            
            foreach (LotteryTicket ticket in tickets)
            {
                if (ticket == null) continue;

                JObject jsonObj = JObject.FromObject(ticket);
                jArray.Add(jsonObj);
            }

            return jArray;
        }


        public override LotteryEvent DeserializeLottery(string fileName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "JSON");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);
            if (!File.Exists(fullPath)) return null;

            JObject jsonObj = null;
            bool success = false;

            var content = File.ReadAllText(fullPath);

            try
            {
                using (var jsonTextReader = new JsonTextReader(new StringReader(content)))
                {
                    jsonObj = JObject.Load(jsonTextReader);
                    success = true;
                }
            }
            catch (Exception)
            {
                return null;
            }

            if (!success || jsonObj == null) return null;

            var numberOfTickets = Convert.ToInt32(jsonObj["NumberOfTickets"]);
            var numberOfParticipants = Convert.ToInt32(jsonObj["NumberOfParticipants"]);
            var prizeFund = Convert.ToInt32(jsonObj["PrizeFund"]);
            var eventName = Convert.ToString(jsonObj["EventName"]);
            var ticketPrice = Convert.ToInt32(jsonObj["TicketPrice"]);
            var winner = this.DeserializeLotteryParticipant((JObject)jsonObj["WinnerParticipant"]); 

            var lottery = new LotteryEvent(eventName, numberOfTickets, numberOfParticipants, prizeFund, ticketPrice, winner);

            return lottery;

        }
        public override LotteryParticipant DeserializeLotteryParticipant(string fileName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName); // fileName == "example.json"
            if (!File.Exists(fullPath)) return null;

            JObject jsonObj = null;
            bool success = false;

            var content = File.ReadAllText(fullPath);

            try
            {
                using (var jsonTextReader = new JsonTextReader(new StringReader(content)))
                {
                    jsonObj = JObject.Load(jsonTextReader);
                    success = true;
                }
            }
            catch (Exception)
            {
                return null;
            }

            if (!success || jsonObj == null) return null;

            return this.DeserializeLotteryParticipant(jsonObj);
        }

        public override LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant)
        {
            if (string.IsNullOrWhiteSpace(jsonContent)) return null;

            JObject jsonObj = null;
            bool success = false;

            try
            {
                jsonObj = JObject.Parse(jsonContent);
                success = true;
            } catch (Exception)
            {
                return null;
            }

            if (!success || jsonObj == null) return null;
           
            var ticketId = Convert.ToInt32(jsonObj["TicketID"]);
            var ticketLen = Convert.ToInt32(jsonObj["TicketLen"]);
            var price = Convert.ToDecimal(jsonObj["Price"]);
            var lotteryName = Convert.ToString(jsonObj["LotteryName"]);

            return new LotteryTicket(ticketId, ticketLen, price, lotteryName, participant);
        }

        public override LotteryParticipant DeserializeLotteryParticipant(JObject jsonObj)
        {
            var initials = Convert.ToString(jsonObj["Initials"]);
            var initialsSplit = initials.Split(" ");
            var age = Convert.ToInt32(jsonObj["Age"]);
            var balance = Convert.ToInt32(jsonObj["Balance"]);
            var greed = Convert.ToInt32(jsonObj["Greed"]);
            var passportInfo = Convert.ToString(jsonObj["PassportInfo"]);
            var participant = new LotteryParticipant(initialsSplit[0], initialsSplit[1], age, balance, passportInfo, greed);

            if (jsonObj["Tickets"] != null)
            {
                var ticketsJArray = (JArray)jsonObj["Tickets"];
                if (ticketsJArray != null)
                {
                    foreach (JObject ticketJson in ticketsJArray)
                    {
                        participant.AddTicket(DeserializeLotteryTicket(ticketJson.ToString(), participant));
                    }
                }

            }

            return participant;
        }
    }
}
