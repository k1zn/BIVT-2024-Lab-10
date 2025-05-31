using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public override void SerializeLottery(LotteryEvent e)
        {
            if (e == null) return;

            var winnerTicket = e.GetWinner();
            if (winnerTicket == null)
            {
                // smthn
            }
            var participant = winnerTicket.Participant;
            var initials = participant.Initials;

            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var jsonObj = JObject.FromObject(e);
            jsonObj["Winner"] = initials;
            jsonObj["TicketID"] = winnerTicket.TicketID;
            jsonObj["Timestamp"] = getRussiaDateTime(unixTimestampSeconds);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "JSON");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = Path.Combine(path, $"{e.EventName}_{unixTimestampSeconds}.json");
            File.WriteAllText(fullPath, jsonObj.ToString());
        }
        public override void SerializeLotteryParticipant(LotteryParticipant participant)
        {
            if (participant == null) return;

            var jsonObj = JObject.FromObject(participant);
            jsonObj["PassportInfo"] = participant.GetPassportInfo("admin");

            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            string fullPath = Path.Combine(folderPath, $"Participant_{participant.Initials}_{unixTimestampSeconds}.json");

            if (File.Exists(fullPath))
            {
                int count = Directory.GetFiles(folderPath).Count(file => Path.GetFileName(file).StartsWith($"Participant_{participant.Initials}_{unixTimestampSeconds}.json", StringComparison.Ordinal));
                fullPath = Path.Combine(folderPath, $"Participant_{participant.Initials}_{unixTimestampSeconds}_{count}.json");
            }

            File.WriteAllText(fullPath, jsonObj.ToString());
        }

        public override LotteryEvent DeserializeLottery(string fileName)
        {
            throw new NotImplementedException();
        }
        public override LotteryParticipant DeserializeLotteryParticipant(string fileName)
        {
            throw new NotImplementedException();
            //var jsonObj = JObject.Parse(File.ReadAllText(file));
            //if (jsonObj == null) return null;
            //var initials = jsonObj["Initials"].ToString();
            //var initialsSplit = initials.Split(" ");
            //var age = Convert.ToInt32(jsonObj["Age"]);
            //var balance = Convert.ToInt32(jsonObj["Balance"]);
            //var greed = Convert.ToInt32(jsonObj["Greed"]);
            //var participant = new LotteryParticipant(initialsSplit[0], initialsSplit[1].ToString(), age, balance, greed);

            //_participantFileMap[participant] = filePath;
            //return participant;
        }
    }
}
