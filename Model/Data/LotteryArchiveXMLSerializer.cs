using Newtonsoft.Json.Linq;
using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Net.Sockets;

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

        private T XMLDeserialize<T>(string fullPath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(fullPath))
            {
                return (T)serializer.Deserialize(reader);
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
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XML");
            string fullPath = Path.Combine(folderPath, fileName);
            if (!File.Exists(fullPath)) return null;
            XmlSerializer serializer = new XmlSerializer(typeof(LotteryEventDTO));
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                var dto = (LotteryEventDTO)serializer.Deserialize(stream);
                return new LotteryEvent
                (
                    dto.lotteryName,
                    dto.numberOfTickets,
                    dto.numberOfParticipants,
                    dto.PrizeFund,
                    dto.TicketPrice
                   
                );
            }
        }
        public override LotteryParticipant DeserializeLotteryParticipant<T>(T obj)
        {
            if (obj is string fileName)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XML");
                string fullPath = Path.Combine(folderPath, fileName);
                if (!File.Exists(fullPath)) return null;

                try
                {
                    var dto = XMLDeserialize<ParticipantDTO>(fullPath);
                    var participant = new LotteryParticipant(
                        dto.Initials,
                        dto.Initials,
                        dto.Age,
                        dto.Balance,
                        dto.PassportInfo,
                        dto.Greed
                    );

                    foreach (var ticketDto in dto.Tickets)
                    {
                        participant.AddTicket(new LotteryTicket(
                            int.Parse(ticketDto.TicketID),
                            ticketDto.TicketLen,
                            ticketDto.Price,
                            ticketDto.LotteryName,
                            participant
                        ));
                    }
                    return participant;
                }
                catch
                {
                    return null;
                }
            }
            throw new ArgumentException("некорректный тип");
        }

    

    public override LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant)
    {
        if (string.IsNullOrEmpty(jsonContent)) return null;
            try
            {
                var serializer = new XmlSerializer(typeof(TicketDTO));
                using (var reader = new StringReader(jsonContent))
                {
                    var dto = (TicketDTO)serializer.Deserialize(reader);
                    var ticket = new LotteryTicket(
                        int.Parse(dto.TicketID),
                        dto.TicketLen,
                        dto.Price,
                        dto.LotteryName,
                        participant
                    );

                    participant?.AddTicket(ticket);
                    return ticket;
                }
            }
            catch
            {
                return null;
            }
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
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XML");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            string fullPath = Path.Combine(folderPath, $"Participant_{participant.Initials}_{unixTimestampSeconds}.xml");

            XMLSerializer(dto, fullPath);
            return fullPath;
        }
        public override T2 SerializeLotteryTicket<T1, T2>(T1 ticket_s)
        {
            if (ticket_s == null || !(ticket_s is LotteryTicket lotteryTicket))
                return default(T2);

            var dto = new TicketDTO
            {
                TicketID = lotteryTicket.TicketID,
                TicketLen = lotteryTicket.TicketLen,
                Price = lotteryTicket.Price,
                LotteryName = lotteryTicket.LotteryName
            };

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XML");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string fullPath = Path.Combine(folderPath, $"Ticket_{lotteryTicket.TicketID}_{unixTimestampSeconds}.xml");
            XMLSerializer(dto, fullPath);
            if (typeof(T2) == typeof(string))
            {
                return (T2)(object)fullPath;
            }

            return default;

        }


    }

        
}
