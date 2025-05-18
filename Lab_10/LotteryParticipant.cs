using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class LotteryParticipant : Person
    {
        public LotteryParticipant(string name, string surname, int age) : base(name, surname, age) { }

        private LotteryTicket[] _tickets;

        public LotteryTicket[] Tickets
        {
            get
            {
                if (_tickets == null)
                    return new LotteryTicket[0];

                return (LotteryTicket[])_tickets.Clone(); 
            }
        }

        public LotteryTicket BuyTicket(LotteryEvent lottery)
        {
            var ticket = new LotteryTicket(lottery, this);
            Array.Resize(ref _tickets, Tickets.Length + 1);
            Tickets[^1] = ticket;

            return ticket;
        }
    }
}
