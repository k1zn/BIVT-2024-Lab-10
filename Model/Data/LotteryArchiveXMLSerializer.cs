﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class LotteryArchiveXMLSerializer : LotteryArchiveSerializer
    {
        public override string Extension => "xml";

        public override void SerializeLottery(LotteryEvent e)
        {

        }
        public override void SerializeLotteryParticipant(LotteryParticipant participant)
        {

        }

        public override LotteryEvent DeserializeLottery(string fileName)
        {
            throw new NotImplementedException();
        }
        public override LotteryParticipant DeserializeLotteryParticipant(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
