using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryEvent
    {
        public decimal TicketPrice { get; private set; }
        public LotteryTicket AddTicket (LotteryParticipant participant)
        {
            return participant.AddTicket(this);
        }
    }
}
