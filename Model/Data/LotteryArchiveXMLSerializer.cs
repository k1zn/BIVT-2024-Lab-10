using Newtonsoft.Json.Linq;
using Model.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
            public int NumberOfTickets { get; set; }
            public int NumberOfParticipants { get; set; }
            public int PrizeFund { get; set; }
            public string EventName { get; set; }
            public decimal TicketPrice { get; set; }
            public string Winner { get; set; }
            public string TicketID { get; set; }
            public DateTime Timestamp { get; set; }
            public TicketDTO WinnerTicket { get; set; }
            public ParticipantDTO WinnerParticipant { get; set; }
            public bool Refunded { get; set; }
        }

        public class ParticipantDTO
        {
            public List<TicketDTO> Tickets { get; set; }
            public decimal Balance { get; set; }
            public int Greed { get; set; }
            public string FullName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public int UserID { get; set; }
            public string PassportInfo { get; set; }
        }

        public class TicketDTO
        {
            public string TicketID { get; set; }
            public int TicketLen { get; set; }
            public decimal Price { get; set; }
            public string LotteryName { get; set; }
            public bool WinTicket { get; set; }

            public int LotteryPrizeFund { get; set; }
        }

        public override void SerializeLottery(LotteryEvent e)
        {
            if (e == null) return;

            var winnerTicket = e.WinnerTicket;
            string fullName = winnerTicket?.Participant.FullName ?? "-";
            string winnerTicketID = winnerTicket?.TicketID ?? "-";
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            TicketDTO winnerTicketDTO = null;
            ParticipantDTO winnerParticipantDTO = null;

            if (winnerTicket != null)
            {
                winnerTicketDTO = new TicketDTO
                {
                    TicketID = winnerTicket.TicketID,
                    TicketLen = winnerTicket.TicketLen,
                    Price = winnerTicket.Price,
                    LotteryName = winnerTicket.LotteryName,
                    WinTicket = true,
                    LotteryPrizeFund = e.PrizeFund
                };

                var p = winnerTicket.Participant;
                winnerParticipantDTO = new ParticipantDTO
                {
                    Balance = p.Balance,
                    Greed = p.Greed,
                    FullName = p.FullName,
                    Name = p.Name,
                    Surname = p.Surname,
                    Age = p.Age,
                    UserID = p.UserID,
                    PassportInfo = p.GetPassportInfo("admin"),
                    Tickets = p.Tickets.Select(t => new TicketDTO
                    {
                        TicketID = t.TicketID,
                        TicketLen = t.TicketLen,
                        Price = t.Price,
                        LotteryName = t.LotteryName,
                        WinTicket = (t == winnerTicket),
                        LotteryPrizeFund = e.PrizeFund
                    }).ToList()
                };
            }

            var dto = new LotteryEventDTO
            {
                NumberOfTickets = e.NumberOfTickets,
                NumberOfParticipants = e.NumberOfParticipants,
                PrizeFund = e.PrizeFund,
                EventName = e.EventName,
                TicketPrice = e.TicketPrice,
                Winner = fullName,
                TicketID = winnerTicketID,
                Timestamp = getRussiaDateTime(unixTimestampSeconds),
                WinnerTicket = winnerTicketDTO,
                WinnerParticipant = winnerParticipantDTO,
                Refunded = false
            };

            XMLSerializer(dto, this.FilePath);
        }

        public override void SerializeLotteryParticipant(LotteryParticipant participant)
        {
            if (participant == null) return;

            var dto = new ParticipantDTO
            {
                Balance = participant.Balance,
                Greed = participant.Greed,
                FullName = participant.FullName,
                Name = participant.Name,
                Surname = participant.Surname,
                Age = participant.Age,
                UserID = participant.UserID,
                PassportInfo = participant.GetPassportInfo("admin"),
                Tickets = participant.Tickets.Select(t => new TicketDTO
                {
                    TicketID = t.TicketID,
                    TicketLen = t.TicketLen,
                    Price = t.Price,
                    LotteryName = t.LotteryName,
                    WinTicket = t.WinTicket,
                    LotteryPrizeFund = t.LotteryPrizeFund,
                }).ToList()
            };

            XMLSerializer(dto, this.FilePath);
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
                LotteryName = lotteryTicket.LotteryName,
                WinTicket = lotteryTicket.WinTicket,
                LotteryPrizeFund = lotteryTicket.LotteryPrizeFund
            };

            XMLSerializer(dto, this.FilePath);
            if (typeof(T2) == typeof(string))
            {
                return (T2)(object)this.FilePath;
            }

            return default;
        }

        public override LotteryEvent DeserializeLottery()
        {
            var dto = XMLDeserialize<LotteryEventDTO>(this.FilePath);
            LotteryParticipant winner = null;
            LotteryEvent lottery = null;

            if (dto.WinnerParticipant != null)
            {
                winner = new LotteryParticipant(
                    dto.WinnerParticipant.Name,
                    dto.WinnerParticipant.Surname,
                    dto.WinnerParticipant.Age,
                    dto.WinnerParticipant.Balance,
                    dto.WinnerParticipant.PassportInfo,
                    dto.WinnerParticipant.Greed
                );

                foreach (var ticketDto in dto.WinnerParticipant.Tickets)
                {
                    var ticket = new LotteryTicket(
                        int.Parse(ticketDto.TicketID),
                        ticketDto.TicketLen,
                        ticketDto.Price,
                        ticketDto.LotteryName,
                        winner,
                        ticketDto.LotteryPrizeFund
                    );
                    winner.AddTicket(ticket);
                }
            }

            lottery = new LotteryEvent(
                dto.EventName,
                dto.NumberOfTickets,
                dto.NumberOfParticipants,
                dto.PrizeFund,
                dto.TicketPrice,
                winner
            );

            return lottery;
        }

        public override LotteryParticipant DeserializeLotteryParticipant<T>(T obj)
        {
            try
            {
                var dto = XMLDeserialize<ParticipantDTO>(this.FilePath);
                var participant = new LotteryParticipant(
                    dto.Name,
                    dto.Surname,
                    dto.Age,
                    dto.Balance,
                    dto.PassportInfo,
                    dto.Greed
                );

                foreach (var ticketDto in dto.Tickets)
                {
                    var ticket = new LotteryTicket(
                        int.Parse(ticketDto.TicketID),
                        ticketDto.TicketLen,
                        ticketDto.Price,
                        ticketDto.LotteryName,
                        participant,
                        ticketDto.LotteryPrizeFund
                    );
                    ticket.SetWinStatus(ticketDto.WinTicket);
                    participant.AddTicket(ticket);
                }
                return participant;
            }
            catch
            {
                return null;
            }
        }

        public override LotteryTicket DeserializeLotteryTicket(string content, LotteryParticipant participant)
        {
            if (string.IsNullOrEmpty(content)) return null;
            try
            {
                var serializer = new XmlSerializer(typeof(TicketDTO));
                using (var reader = new StringReader(content))
                {
                    var dto = (TicketDTO)serializer.Deserialize(reader);
                    var ticket = new LotteryTicket(
                        int.Parse(dto.TicketID),
                        dto.TicketLen,
                        dto.Price,
                        dto.LotteryName,
                        participant,
                        dto.LotteryPrizeFund
                    );

                    ticket.SetWinStatus(dto.WinTicket);

                    participant?.AddTicket(ticket);
                    return ticket;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
