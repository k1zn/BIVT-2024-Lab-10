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

        private decimal _addonOnSpent;
        private decimal _addonOnWin;

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
                    Balance += (decimal)0.9 * ticket.Price;
                    this.Save();
                }
            }

            return true;
        }

    }
}
