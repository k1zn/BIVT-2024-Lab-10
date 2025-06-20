﻿using Model.Data;
using Newtonsoft.Json.Linq;

namespace Model.Core
{
    public partial class LotteryEvent
    {

        private int _futureTicketID = 1;
        public int NumberOfTickets
        {
            get; private set;
        }

        public int NumberOfParticipants
        {
            get; private set;
        }

        public int PrizeFund
        {
            get; private set;
        }

        public string EventName
        {
            get; private set;
        }

        private bool _isWinnerExist;
        private LotteryTicket _winnerTicket;
        private LotteryParticipant _winnerParticipant;

        public LotteryTicket WinnerTicket
        {
            get
            {
                return _winnerTicket;
            }
        }

        public LotteryParticipant WinnerParticipant
        {
            get
            {
                return _winnerParticipant;
            }
        }

        private LotteryParticipant[] _lotteryParticipants;

        private LotteryTicket[] _tickets;

        public LotteryEvent(string eventName, int numberOfTickets, int numberOfParticipants, int prizeFund, decimal ticketPrice)
        {
            if (numberOfTickets <= 0)
                throw new ArgumentException("Number of tickets must be > 0");

            EventName = eventName;
            NumberOfTickets = numberOfTickets;
            NumberOfParticipants = numberOfParticipants;
            PrizeFund = prizeFund;
            TicketPrice = ticketPrice;
            _tickets = new LotteryTicket[0];
            _lotteryParticipants = new LotteryParticipant[0];
        }

        public LotteryEvent(string eventName, int numberOfTickets, int numberOfParticipants, int prizeFund, decimal ticketPrice, LotteryParticipant winner) : this(eventName, numberOfTickets, numberOfParticipants, prizeFund, ticketPrice)
        {
            _isWinnerExist = true;
            _winnerParticipant = winner;
            if (_winnerParticipant.Tickets == null)
            {
                throw new Exception("Winner can't have null tickets!");
            }
            foreach (var ticket in _winnerParticipant.Tickets)
            {
                if (ticket == null) continue;
                if (ticket.LotteryName == eventName)
                {
                    _winnerTicket = ticket;
                    break;
                }
            }
        }

        public int GetFutureTicketID()
        {
            int ans = _futureTicketID;
            _futureTicketID++;

            return ans;
        }

        public void AddParticipantAndHisTickets(LotteryParticipant participant)
        {
            if (_lotteryParticipants.Length > NumberOfParticipants) return;
            if (participant == null) return;

            Array.Resize(ref _lotteryParticipants, _lotteryParticipants.Length + 1);
            _lotteryParticipants[_lotteryParticipants.Length - 1] = participant;

            if (participant.Tickets.Length == 0) return;
            _tickets = _tickets.Concat(participant.Tickets.Where(t => t != null).ToArray()).ToArray();
        }

        public void AddParticipantAndHisTickets(LotteryParticipant[] participants)
        {
            if (participants == null) return;
            foreach (var participant in participants)
            {
                AddParticipantAndHisTickets(participant);
            }
        }

        public void FillRandom()
        {
            var rand = new Random();

            LotteryArchiveSerializer serializer;

            var config = Path.Combine(Directory.GetCurrentDirectory(), "serializetype.txt");
            if (!File.Exists(config)) File.WriteAllText(config, "json");

            if (File.ReadAllText(config) == "json")
            {
                serializer = new LotteryArchiveJSONSerializer();
            }
            else
            {
                serializer = new LotteryArchiveXMLSerializer();
            }

            serializer.SelectFolder(Path.Combine(Directory.GetCurrentDirectory(), "Participants"));

            var files = Directory.GetFiles(serializer.FolderPath);
            if (files.Length == 0) return;
            if (files.Length > NumberOfParticipants)
            {
                files = files.OrderBy(f => rand.Next()).Take(NumberOfParticipants).ToArray();
            }
            foreach (var file in files)
            {
                serializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                var participant = serializer.DeserializeLotteryParticipant<object>(null);
                if (participant != null)
                {
                    Array.Resize(ref _lotteryParticipants, _lotteryParticipants.Length + 1);
                    _lotteryParticipants[_lotteryParticipants.Length - 1] = participant;
                }

            }

            _lotteryParticipants = _lotteryParticipants.Where(r => r != null)
                .OrderByDescending(participant => participant.Greed).ToArray();

            while (this._tickets.Length <= this.NumberOfTickets)
            {
                bool canAnyParticipantBuyTicket = false;

                foreach (var participant in _lotteryParticipants)
                {
                    if (this._tickets.Length >= this.NumberOfTickets) break;

                    var ticket = participant.AddTicket(this);
                    if (ticket != null)
                    {
                        canAnyParticipantBuyTicket = true;
                        serializer.SelectFile($"Participant_{participant.FullName}_{participant.GetPassportInfo("admin")}");
                        serializer.SerializeLotteryParticipant(participant);

                        Array.Resize(ref _tickets, _tickets.Length + 1);
                        _tickets[_tickets.Length - 1] = ticket;
                    }
                }

                if (!canAnyParticipantBuyTicket) break;
            }
        }
    }
}
