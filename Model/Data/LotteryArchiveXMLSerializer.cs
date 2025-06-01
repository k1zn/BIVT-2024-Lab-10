using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class LotteryArchiveXMLSerializer : LotteryArchiveSerializer
    {
        public override string Extension => "xml";

        public override string SerializeLottery(LotteryEvent e)
        {
            return "";
        }
        public override string SerializeLotteryParticipant(LotteryParticipant participant)
        {
            return "";
        }

        public override LotteryEvent DeserializeLottery(string fileName)
        {
            throw new NotImplementedException();
        }
        public override LotteryParticipant DeserializeLotteryParticipant(string fileName)
        {
            throw new NotImplementedException();
        }

        public override JArray SerializeLotteryTicket(LotteryTicket ticket)
        {
            throw new NotImplementedException();
        }

        public override JArray SerializeLotteryTicket(LotteryTicket[] tickets)
        {
            throw new NotImplementedException();
        }

        public override LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant)
        {
            throw new NotImplementedException();
        }

        public override LotteryParticipant DeserializeLotteryParticipant(JObject json)
        {
            throw new NotImplementedException();
        }
    }
}
