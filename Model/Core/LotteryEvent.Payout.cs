﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Data;

namespace Model.Core
{
    public partial class LotteryEvent
    {
        public bool Refunded
        {
            get; private set;
        }

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
                _winnerTicket.SetWinStatus(true);

                winnerParticipant = winner.Participant;
                _winnerParticipant = winnerParticipant;
                winnerParticipant.AddBalance(PrizeFund, this);

                winnerParticipant.Save();

                return winner;
            }
        }

        private LotteryTicket CancelThisLottery()
        {
            if (this.Refunded) return null;

            _isWinnerExist = true;
            var participant = new LotteryParticipant("-", "-", 0, 0, "-", 0);
            var ticket = new LotteryTicket(-1, 0, 0, this.EventName, participant, 0);
            participant.AddTicket(ticket);
            _winnerTicket = ticket;
            _winnerParticipant = participant;

            foreach (LotteryParticipant p in this._lotteryParticipants)
            {
                if (p == null) continue;
                this.Refunded = p.RefundMoney(this);
            }

            return ticket;
        }

    }
}
