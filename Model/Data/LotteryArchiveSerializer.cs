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
        public abstract string SerializeLottery(LotteryEvent e);
        public abstract string SerializeLotteryParticipant(LotteryParticipant participant);
        public abstract JArray SerializeLotteryTicket(LotteryTicket ticket);
        public abstract JArray SerializeLotteryTicket(LotteryTicket[] tickets);

        public abstract LotteryEvent DeserializeLottery(string fileName);
        public abstract LotteryParticipant DeserializeLotteryParticipant(string fileName);
        public abstract LotteryParticipant DeserializeLotteryParticipant(JObject json);
        public abstract LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant);
    }
}
