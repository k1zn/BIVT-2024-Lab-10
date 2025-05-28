using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class LotteryEvent
    {
        public LotteryTicket BuyTicket (LotteryParticipant participant)
        {
            return participant.BuyTicket(this);
        }
    }
}
