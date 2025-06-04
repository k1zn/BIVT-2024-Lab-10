using Model.Core;
using Newtonsoft.Json.Linq;
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
        public abstract T2 SerializeLotteryTicket<T1, T2>(T1 ticket_s);

        public abstract LotteryEvent DeserializeLottery();
        public abstract LotteryParticipant DeserializeLotteryParticipant<T>(T obj);
        public abstract LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant);
    }
}
