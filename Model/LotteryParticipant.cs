using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class LotteryParticipant : Person
    {
        public LotteryParticipant(string name, string surname, int age, decimal initialBalance, int greed=0) : base(name, surname, age)
        {
            Balance = initialBalance;
            Greed = greed;
        }

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
            if (ticket.Price <= Balance)
            {
                Balance-=ticket.Price;
                Array.Resize(ref _tickets, Tickets.Length + 1);
                Tickets[^1] = ticket;
                return ticket;
            }
            else return null;

            
        }
    }
}