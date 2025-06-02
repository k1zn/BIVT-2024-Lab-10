using Newtonsoft.Json.Linq;
using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Model.Data
{
    public class LotteryArchiveXMLSerializer : LotteryArchiveSerializer
    {
        public override string Extension => "xml";

        private DateTime getRussiaDateTime(long unixTimestampSeconds)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds);
            DateTime dateTime = dateTimeOffset.DateTime;
            TimeZoneInfo russiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, russiaTimeZone);
        }

        private void XMLSerializer<T>(T obj, string FullPath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = XmlWriter.Create(FullPath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public class LotteryEventDTO
        {
            public string lotteryName { get; set; }
            public int numberOfParticipants { get; set; }
            public int numberOfTickets { get; set; }
            public int PrizeFund { get; set; }
            public string Winner { get; set; }
            public decimal TicketPrice { get; set; }
            public string ID { get; set; }
            public DateTime TimeStamp { get; set; }

        }

        public class ParticipantDTO
        {
            public string Initials { get; set; }
            public int Age { get; set; }
            public decimal Balance { get; set; }
            public int Greed { get; set; }
            public string PassportInfo { get; set; }

            public List<TicketDTO> Tickets { get; set; }
        }

        public class TicketDTO
        {
            public string TicketID { get; set; }
            public int TicketLen { get; set; }
            public decimal Price { get; set; }
            public string LotteryName { get; set; }
        }

        public override LotteryEvent DeserializeLottery(string fileName)
        {
            throw new NotImplementedException();
        }

        public override LotteryParticipant DeserializeLotteryParticipant<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant)
        {
            throw new NotImplementedException();
        }

        public override string SerializeLottery(LotteryEvent e)
        {
            if (e == null) return "";
            var winnerTicket = e.WinnerTicket;
            string initials;
            string winnerTicketID;

            if (winnerTicket == null)
            {
                initials = "-";
                winnerTicketID = "-";

            }
            else
            {
                initials = winnerTicket.Participant.Initials;
                winnerTicketID = winnerTicket.TicketID;
            }
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var dto = new LotteryEventDTO
            {
                lotteryName = e.EventName,
                numberOfParticipants = e.NumberOfParticipants,
                numberOfTickets = e.NumberOfTickets,
                PrizeFund = e.PrizeFund,
                Winner = initials,
                ID = winnerTicketID,
                TicketPrice = e.TicketPrice,
                TimeStamp = getRussiaDateTime(unixTimestampSeconds)
            };
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XML");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fullPath = Path.Combine(folderPath, $"{e.EventName}_{unixTimestampSeconds}.xml");

            XMLSerializer(dto, fullPath);
            return fullPath;
        }

        public override string SerializeLotteryParticipant(LotteryParticipant participant)
        {
            if (participant == null) return "";
            var dto = new ParticipantDTO
            {
                Initials = participant.Initials,
                Age = participant.Age,
                Balance = participant.Balance,
                Greed = participant.Greed,
                PassportInfo = participant.GetPassportInfo("admin"),
                Tickets = new List<TicketDTO>()
            };


            foreach (var ticket in participant.Tickets)
            {
                dto.Tickets.Add(new TicketDTO
                {
                    TicketID = ticket.TicketID,
                    TicketLen = ticket.TicketLen,
                    Price = ticket.Price,
                    LotteryName = ticket.LotteryName
                });
            }




            return "";

        }

        public override JArray SerializeLotteryTicket<T>(T ticket_s)
        {
            throw new NotImplementedException();
        }
    }
}
