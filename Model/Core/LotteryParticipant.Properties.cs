using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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

    }
}
