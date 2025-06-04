using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class LotteryParticipant
    {
        public decimal Balance {  get;private set; }
        public int Greed { get; private set; }

        public decimal RefundedMoney { get; private set; }

        public void AddBalance(decimal balance, LotteryEvent lottery)
        {
            if (lottery.WinnerParticipant == this)
            {
                Balance += balance;
                this.Save();
            }
        }

        public bool RefundMoney(LotteryEvent lottery)
        {
            if (lottery.Refunded) return true;

            foreach (LotteryTicket ticket in _tickets)
            {
                if (ticket == null) continue;
                if (ticket.LotteryName == lottery.EventName)
                {
                    decimal refund = (decimal)0.9 * ticket.Price;
                    Balance += refund;
                    RefundedMoney += refund;
                    this.Save();
                }
            }

            return true;
        }

    }
}
