using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class LotteryTicket
    {
        private int _ticketId;
        private int _ticketLen;
        private LotteryParticipant _participant;

        [JsonIgnore]
        public LotteryParticipant Participant
        {
            get
            {
                return _participant;
            }
        }

        public string LotteryName
        {
            get; private set;
        }

        public int TicketLen
        {
            get
            {
                return _ticketLen;
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
            _ticketId = lottery.GetFutureTicketID();
            _ticketLen = lottery.NumberOfTickets.ToString().Length;
            _participant = participant;
            _price = lottery.TicketPrice;

            LotteryName = lottery.EventName;
        }

        public LotteryTicket(int ticketId, int ticketLen, decimal price, string lotteryName, LotteryParticipant participant)
        {
            _ticketId = ticketId;
            _ticketLen = ticketLen;
            _participant = participant;
            _price = price;

            LotteryName = lotteryName;
        }
    }
}
