using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class LotteryEvent
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

        private string[] RandomNames = new string[] { "Максим", "Роберт", "Николай", "Михаил", "Эмин", "Алексей", "Артём", "Тимур", "Роман", "Сергей", "Леонид", "Иван", "Арсений", "Дмитрий", "Данил", "Глеб", "Фёдор", "Егор", "Демид", "Марк", "Александр", "Владимир", "Даниил", "Никита", "Константин", "Руслан", "Лев", "Григорий", "Пётр", "Ярослав", "Дамир", "Илья", "Георгий", "Захар", "Владислав", "Юрий", "Лука", "Денис", "Богдан", "Гордей", "Кирилл", "Степан", "Святослав", "Вадим", "Матвей", "Виктор", "Камиль", "Василий", "Павел", "Даниэль", "Андрей", "Артур", "Семён", "Платон", "Артемий", "Виталий", "Елисей", "Антон", "Тимофей", "Филипп", "Рустам", "Альберт", "Тихон", "Данила", "Родион", "Али", "Мирослав", "Евгений", "Давид", "Савелий", "Игорь", "Назар", "Валерий", "Олег", "Всеволод", "Арсен", "Макар", "Савва", "Адам", "Карим", "Вячеслав", "Станислав", "Эрик", "Мирон", "Герман", "Ян", "Марсель", "Анатолий", "Борис", "Ибрагим", "Леон", "Ростислав", "Серафим", "Демьян", "Яков", "Марат", "Аркадий", "Эмир", "Тигран", "Рафаэль", "Кира", "Анна", "Злата", "Евгения", "Софья", "Дарья", "Дарина", "Вероника", "Мария", "Аделина", "Анастасия", "Алиса", "Вера", "Виктория", "Сафия", "Варвара", "Полина", "Ева", "Арина", "Валерия", "Ульяна", "Малика", "Ариана", "Мирослава", "Есения", "Адель", "Василиса", "Элина", "София", "Кристина", "Александра", "Таисия", "Амалия", "Ирина", "Елизавета", "Аврора", "Мила", "Эмилия", "Агата", "Стефания", "Ангелина", "Екатерина", "Амина", "Милана", "Ксения", "Яна", "Лилия", "Елена", "Аяна", "Амелия", "Ника", "Маргарита", "Майя", "Алина", "Мира", "Алёна", "Марина", "Пелагея", "Юлия", "Камилла", "Ольга", "Алия", "Камила", "Марьям", "Любовь", "Татьяна", "Валентина", "Николь", "Светлана", "Ясмина", "Владислава", "Сабина", "Марьяна", "Антонина", "Лада", "Василина", "Лия", "Агния", "Мелания", "Айлин", "Мия", "Диана", "Ярослава", "Надежда", "Оливия", "Амира", "Наталья", "Фатима", "Алисия", "Эвелина", "Олеся", "Аиша", "Лидия", "Марианна", "Теона", "Альфия", "Медина", "Асия", "Лиана", "Зоя" };

        string[] RandomCyrillicChars = new string[] {
            "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й",
            "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф",
            "Х", "Ц", "Ч", "Ш", "Щ", "Э", "Ю", "Я"
        };

        private LotteryTicket[] _tickets;

        public LotteryEvent(string eventName, int numberOfTickets, int numberOfParticipants, int prizeFund)
        {
            if (numberOfTickets <= 0)
                throw new ArgumentException("Number of tickets must be > 0");

            EventName = eventName;
            NumberOfTickets = numberOfTickets;
            NumberOfParticipants = numberOfParticipants;
            PrizeFund = prizeFund;
        }

        public void FillRandom()
        {
            Random rand = new Random();

            for (int i = 0; i < NumberOfTickets; i++)
            {

                var name = RandomNames[rand.Next(RandomNames.Length)];
                var surname = RandomCyrillicChars[rand.Next(RandomCyrillicChars.Length)];
                var age = rand.Next(18, 46);

                // из будущих проблем - тут никак не учитывается numberOfParticipants, так что если numberOfTickets > numberOfParticipants то людей окажется больше чем было задано

                var person = new LotteryParticipant(name, surname, age);
                var ticket = person.BuyTicket(this);

                Array.Resize(ref _tickets, _tickets.Length + 1);
                _tickets[^1] = ticket;
            }
        }

        public LotteryTicket GetWinner()
        {
            if (_isWinnerExist) return _winnerTicket;

            var rand = new Random();

            _isWinnerExist = true;
            var winner = _tickets[rand.Next(_tickets.Length)];
            _winnerTicket = winner;

            return winner;
        }
    }
}
