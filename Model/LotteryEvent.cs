using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace Model
{
    public partial class LotteryEvent
    {
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

        private LotteryParticipant[] _lotteryParticipants;

        private LotteryTicket[] _tickets;

        private Dictionary<LotteryParticipant, string> _participantFileMap = new Dictionary<LotteryParticipant, string>();

        public LotteryEvent(string eventName, int numberOfTickets, int numberOfParticipants, int prizeFund, decimal ticketprice)
        {
            if (numberOfTickets <= 0)
                throw new ArgumentException("Number of tickets must be > 0");

            EventName = eventName;
            NumberOfTickets = numberOfTickets;
            NumberOfParticipants = numberOfParticipants;
            PrizeFund = prizeFund;
            TicketPrice = ticketprice;
            _tickets = new LotteryTicket[numberOfTickets];
            _lotteryParticipants = new LotteryParticipant[numberOfParticipants];
        }

        public void FillRandom()
        {
            var rand = new Random();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var files = Directory.GetFiles(path);
            if (files.Length == 0) return;
            if (files.Length > NumberOfParticipants)
            {
                files = files.OrderBy(f => rand.Next()).Take(NumberOfParticipants).ToArray();
            }
            int index = 0;
            foreach (var file in files)
            {
                var jsonObj = JObject.Parse(File.ReadAllText(file));
                var participant = deserialize(jsonObj, file);
                if (participant != null) 
                    _lotteryParticipants[index++] = participant;
            }
            
            _lotteryParticipants = _lotteryParticipants.Where(r => r != null)
                .OrderByDescending(participant => participant.Greed).ToArray();
            index = 0;
            foreach (var participant in _lotteryParticipants)
            {
                var ticket = participant.BuyTicket(this);
                string pathToParticipant = _participantFileMap[participant];
                if (pathToParticipant != null && ticket!=null)
                {
                    File.WriteAllText(pathToParticipant, JObject.FromObject(participant).ToString());
                }
                if (ticket != null) _tickets[index++] = ticket;
            }
            
            
        }
        private LotteryParticipant deserialize(JObject jsonObj, string filePath)
        {
            if (jsonObj == null) return null;
            var initials = jsonObj["Initials"].ToString();
            var initialsSplit = initials.Split(" ");
            var age = Convert.ToInt32(jsonObj["Age"]);
            var balance = Convert.ToInt32(jsonObj["Balance"]);
            var greed = Convert.ToInt32(jsonObj["Greed"]);
            var participant = new LotteryParticipant(initialsSplit[0], initialsSplit[1].ToString(), age, balance, greed);

            _participantFileMap[participant] = filePath;
            return participant;
        }
        public LotteryTicket GetWinner()
        {
            if (_isWinnerExist) return _winnerTicket;

            var rand = new Random();

            _isWinnerExist = true;
            _tickets = _tickets.Where(t => t != null).ToArray();
            if (_tickets.Length == 0)
            {
                return null;
            }
            var i = rand.Next(_tickets.Length);
            var winner = _tickets[i];
            var winnerParticipant = winner.Participant;
            var newBalance = winnerParticipant.Balance+PrizeFund;
            string pathToParticipant = _participantFileMap[winnerParticipant];
            var jsonObj = JObject.FromObject(winnerParticipant);
            jsonObj["Balance"] = newBalance;
            File.WriteAllText(pathToParticipant,jsonObj.ToString());

            if (winner == null)
            {
                throw new Exception($"error! {_tickets.Length} {i}");
            }
            _winnerTicket = winner;

            return winner;
        }

    }
}
