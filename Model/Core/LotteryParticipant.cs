using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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

        public string GetPathToSerialized(string authKey)
        {
            if (authKey != "admin") return "";
            return Path.Combine(Directory.GetCurrentDirectory(), "Participants", $"Participant_{this.Initials}_{this.GetPassportInfo(authKey)}.json");
        }

    }
}