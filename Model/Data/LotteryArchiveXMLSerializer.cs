using Newtonsoft.Json.Linq;
using Model.Core;
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

        public override LotteryEvent DeserializeLottery(string fileName)
        {
            throw new NotImplementedException();
        }

        public override LotteryParticipant DeserializeLotteryParticipant<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override LotteryTicket DeserializeLotteryTicket(string jsonContent, LotteryParticipant participant)
        {
            throw new NotImplementedException();
        }

        public override string SerializeLottery(LotteryEvent e)
        {
            throw new NotImplementedException();
        }

        public override string SerializeLotteryParticipant(LotteryParticipant participant)
        {
            throw new NotImplementedException();
        }

        public override JArray SerializeLotteryTicket<T>(T ticket_s)
        {
            throw new NotImplementedException();
        }
    }
}
