using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class LotteryTicket
    {
        private static int _ticketCounter = 0;
        private int _ticketId;
        private int _ticketLen;
        private LotteryParticipant _participant;
        
        public LotteryParticipant Participant
        {
            get
            {
                return _participant;
            }
        }

        public string TicketID
        {
            get
            {
                return _ticketId.ToString($"D{_ticketLen}");
            }
        }


        public LotteryTicket(LotteryEvent lottery, LotteryParticipant participant)
        {
            //

            _ticketId = _ticketCounter++;
            _ticketLen = lottery.NumberOfTickets.ToString().Length;
            _participant = participant;
            _price = lottery.TicketPrice;
        }
    }
}
