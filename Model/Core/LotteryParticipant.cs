using Model.Core;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryParticipant : Person
    {
        public LotteryParticipant(string name, string surname, int age, decimal initialBalance, string passportInfo, int greed=0) : base(name, surname, passportInfo, age)
        {
            Balance = initialBalance;
            Greed = greed;

            _tickets = new LotteryTicket[0];
        }

        private LotteryTicket[] _tickets;

        public LotteryTicket[] Tickets
        {
            get
            {
                return _tickets;
            }
        }

        public decimal MoneySpentOnTickets
        {
            get
            {
                decimal ans = 0;
                foreach (LotteryTicket ticket in Tickets)
                {
                    if (ticket == null) continue;
                    ans += ticket.Price;
                }
                return ans;
            }
        }

        public decimal MoneyWinOnTickets
        {
            get
            {
                decimal ans = 0;
                foreach (LotteryTicket ticket in this.FilterTickets(ticket => ticket.WinTicket))
                {
                    if (ticket == null) continue;
                    if (ticket.WinTicket) ans += ticket.LotteryPrizeFund;
                }
                ans += RefundedMoney;
                return ans;
            }
        }

        public LotteryTicket AddTicket(LotteryEvent lottery)
        {
            if (lottery.TicketPrice <= Balance)
            {
                var ticket = new LotteryTicket(lottery, this);
                Balance -= ticket.Price;
                Array.Resize(ref _tickets, _tickets.Length + 1);
                _tickets[^1] = ticket;
                return ticket;
            }
            else return null;
        }

        public void AddTicket(LotteryTicket ticket)
        {
            if (ticket == null) return;

            Array.Resize(ref _tickets, _tickets.Length + 1);
            _tickets[^1] = ticket;
        }

        public void Save()
        {
            LotteryArchiveSerializer serializer;

            var config = Path.Combine(Directory.GetCurrentDirectory(), "serializetype.txt");
            if (!File.Exists(config)) File.WriteAllText(config, "json");

            if (File.ReadAllText(config) == "json")
            {
                serializer = new LotteryArchiveJSONSerializer();
            }
            else
            {
                serializer = new LotteryArchiveXMLSerializer();
            }

            serializer.SelectFolder(Path.Combine(Directory.GetCurrentDirectory(), "Participants"));
            serializer.SelectFile($"Participant_{this.FullName}_{this.GetPassportInfo("admin")}");

            serializer.SerializeLotteryParticipant(this);
        }

        public delegate bool TicketFilter(LotteryTicket ticket);

        public LotteryTicket[] FilterTickets(TicketFilter filter)
        {
            if (filter == null) return this.Tickets;
            return this.Tickets.Where(t => filter(t)).ToArray();
        }

        public static bool operator ==(LotteryParticipant a, LotteryParticipant b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.UserID == b.UserID;
        }

        public static bool operator !=(LotteryParticipant a, LotteryParticipant b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is LotteryParticipant other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.GetPassportInfo("admin").GetHashCode();
        }
    }
}