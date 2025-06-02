using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;

namespace Model.Core
{
    public partial class LotteryEvent
    {
        public LotteryTicket GetWinner()
        {
            if (_isWinnerExist) return _winnerTicket;


            _tickets = _tickets.Where(t => t != null).ToArray();
            if (_tickets.Length == 0)
            {
                return null;
            }

            LotteryParticipant winnerParticipant = null;

            if (_tickets.Length <= 0.25 * NumberOfTickets)
            {
                return this.CancelThisLottery();
            } else
            {
                var rand = new Random();
                _isWinnerExist = true;

                var i = rand.Next(_tickets.Length);
                var winner = _tickets[i];
                if (winner == null)
                {
                    throw new Exception($"Error! {_tickets.Length} {i}");
                }
                _winnerTicket = winner;

                winnerParticipant = winner.Participant;
                _winnerParticipant = winnerParticipant;
                winnerParticipant.AddBalance(PrizeFund, this);
                var serializer = new LotteryArchiveJSONSerializer();
                serializer.SelectFolder(Path.Combine(Directory.GetCurrentDirectory(), "Participants"));
                serializer.SelectFile($"Participant_{winnerParticipant.Initials}_{winnerParticipant.GetPassportInfo("admin")}");
                serializer.SerializeLotteryParticipant(winnerParticipant);

                return winner;
            }
        }

        private LotteryTicket CancelThisLottery()
        {
            _isWinnerExist = true;
            var participant = new LotteryParticipant("-", "-", 0, 0, "-", 0);
            var ticket = new LotteryTicket(-1, 0, 0, this.EventName, participant);
            participant.AddTicket(ticket);
            _winnerTicket = ticket;
            _winnerParticipant = participant;

            foreach (LotteryParticipant p in this._lotteryParticipants)
            {
                if (p == null) continue;
                //p.AddBalance...
                // добавлять 90% от денег потраченных именно на ЭТУ лотерею
                // перед этим исправить логику жадности
            }

            return ticket;
        }

    }
}
