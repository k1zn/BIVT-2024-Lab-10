using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class LotteryEvent
    {
        public decimal TicketPrice { get; private set; }
        public LotteryTicket BuyTicket (LotteryParticipant participant)
        {
            return participant.BuyTicket(this);
        }
    }
}
