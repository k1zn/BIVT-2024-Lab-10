using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public abstract class LotteryArchiveSerializer : FileSerializer
    {
        public abstract void SerializeLottery(LotteryEvent e);
        public abstract void SerializeLotteryParticipant(LotteryParticipant participant);

        public abstract LotteryEvent DeserializeLottery(string fileName);
        public abstract LotteryParticipant DeserializeLotteryParticipant(string fileName);
    }
}
