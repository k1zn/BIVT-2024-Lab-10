using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryParticipant
    {
        public decimal Balance {  get;private set; }
        public int Greed { get; private set; }

        public void AddBalance(decimal balance, LotteryEvent lottery)
        {
            if (lottery.WinnerParticipant == this)
                Balance += balance;
        }

        public void RefundMoney(LotteryEvent lottery)
        {
            LotteryTicket[] newTickets = new LotteryTicket[0];
            foreach (LotteryTicket ticket in _tickets)
            {
                if (ticket == null) continue;
                if (ticket.LotteryName == lottery.EventName)
                {
                    Balance += (decimal)0.9 * ticket.Price;
                } else
                {
                    Array.Resize(ref newTickets, newTickets.Length + 1);
                    newTickets[^1] = ticket;
                }
            }

            if (newTickets.Length != 0)
                _tickets = newTickets;
        }

    }
}
