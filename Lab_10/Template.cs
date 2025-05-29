using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Lab_10
{
    public partial class Template : Form
    {
        private string[] RandomNames = new string[] { "Максим", "Роберт", "Николай", "Михаил", "Эмин", "Алексей", "Артём", "Тимур", "Роман", "Сергей", "Леонид", "Иван", "Арсений", "Дмитрий", "Данил", "Глеб", "Фёдор", "Егор", "Демид", "Марк", "Александр", "Владимир", "Даниил", "Никита", "Константин", "Руслан", "Лев", "Григорий", "Пётр", "Ярослав", "Дамир", "Илья", "Георгий", "Захар", "Владислав", "Юрий", "Лука", "Денис", "Богдан", "Гордей", "Кирилл", "Степан", "Святослав", "Вадим", "Матвей", "Виктор", "Камиль", "Василий", "Павел", "Даниэль", "Андрей", "Артур", "Семён", "Платон", "Артемий", "Виталий", "Елисей", "Антон", "Тимофей", "Филипп", "Рустам", "Альберт", "Тихон", "Данила", "Родион", "Али", "Мирослав", "Евгений", "Давид", "Савелий", "Игорь", "Назар", "Валерий", "Олег", "Всеволод", "Арсен", "Макар", "Савва", "Адам", "Карим", "Вячеслав", "Станислав", "Эрик", "Мирон", "Герман", "Ян", "Марсель", "Анатолий", "Борис", "Ибрагим", "Леон", "Ростислав", "Серафим", "Демьян", "Яков", "Марат", "Аркадий", "Эмир", "Тигран", "Рафаэль", "Кира", "Анна", "Злата", "Евгения", "Софья", "Дарья", "Дарина", "Вероника", "Мария", "Аделина", "Анастасия", "Алиса", "Вера", "Виктория", "Сафия", "Варвара", "Полина", "Ева", "Арина", "Валерия", "Ульяна", "Малика", "Ариана", "Мирослава", "Есения", "Адель", "Василиса", "Элина", "София", "Кристина", "Александра", "Таисия", "Амалия", "Ирина", "Елизавета", "Аврора", "Мила", "Эмилия", "Агата", "Стефания", "Ангелина", "Екатерина", "Амина", "Милана", "Ксения", "Яна", "Лилия", "Елена", "Аяна", "Амелия", "Ника", "Маргарита", "Майя", "Алина", "Мира", "Алёна", "Марина", "Пелагея", "Юлия", "Камилла", "Ольга", "Алия", "Камила", "Марьям", "Любовь", "Татьяна", "Валентина", "Николь", "Светлана", "Ясмина", "Владислава", "Сабина", "Марьяна", "Антонина", "Лада", "Василина", "Лия", "Агния", "Мелания", "Айлин", "Мия", "Диана", "Ярослава", "Надежда", "Оливия", "Амира", "Наталья", "Фатима", "Алисия", "Эвелина", "Олеся", "Аиша", "Лидия", "Марианна", "Теона", "Альфия", "Медина", "Асия", "Лиана", "Зоя" };
        string[] RandomCyrillicChars = new string[] {
            "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й",
            "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф",
            "Х", "Ц", "Ч", "Ш", "Щ", "Э", "Ю", "Я"
        };
        private List<LotteryParticipant> participants = new List<LotteryParticipant>();
        public Template()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("Name", "Имя");
            dataGridView1.Columns.Add("Surname", "Фамилия");
            dataGridView1.Columns.Add("Age", "Возраст");
            dataGridView1.Columns.Add("Balance", "Баланс");
            dataGridView1.Columns.Add("Greed", "Жадность");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var files = Directory.GetFiles(path);
            deserialize(files);

        }
        private void deserialize(string[] files)
        {
            if (files.Length == 0)
            {
                return;
            }
            foreach (string file in files)
            {
                if (string.IsNullOrEmpty(file)) return;

                var jsonObj = JObject.Parse(File.ReadAllText(file));
                var initials = jsonObj["Initials"].ToString().Split(" ");
                dataGridView1.Rows.Add(initials[0], initials[1].Trim(), jsonObj["Age"], jsonObj["Balance"], jsonObj["Greed"]);
            }
        }


        private void addParticipants(int cnt)
        {

            var rand = new Random();
            for (int i = 0; i < cnt; i++)
            {
                var name = RandomNames[rand.Next(RandomNames.Length)];
                var surname = RandomCyrillicChars[rand.Next(RandomCyrillicChars.Length)];
                var age = rand.Next(18, 46);
                var balance = rand.Next(100, 1000);
                var greed = rand.Next(0, 100);
                dataGridView1.Rows.Add(name, surname, age, balance, greed);

            }


        }




        private void button2_Click(object sender, EventArgs e)
        {
            var form = new countParticipants();
            form.ShowDialog();
            int cnt = form.count;

            addParticipants(cnt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            int rowIndex = 1;
            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                
                if (!int.TryParse(row.Cells["Age"].Value?.ToString(), out int age))
                {
                    MessageBox.Show($"Ошибка в строке {rowIndex}: Возраст должен быть числом!");
                    return;
                }

                if (!decimal.TryParse(row.Cells["Balance"].Value?.ToString(), out decimal balance))
                {
                    MessageBox.Show($"Ошибка в строке {rowIndex}: Баланс должен быть числом!");
                    return;
                }
                else if(balance < 0)
                {
                    MessageBox.Show($"Ошибка в строке {rowIndex}: Баланс должен быть положительным!");
                    return;
                }

                if (!int.TryParse(row.Cells["Greed"].Value?.ToString(), out int greed))
                {
                    MessageBox.Show($"Ошибка в строке {rowIndex}: Жадность должна быть числом!");
                    return;
                }
                else if(greed > 100 || greed < 0)
                {
                    MessageBox.Show($"Ошибка в строке {rowIndex}: Значение жадности должно находиться в диапазоне от 0 до 100");
                    return;
                }
                var name = row.Cells["Name"].Value.ToString();
                var surname = row.Cells["Surname"].Value.ToString();
                var participant = new LotteryParticipant(name, surname, age, balance, greed);
                var jsonObj = JObject.FromObject(participant);
                string fullpath = Path.Combine(path, $"Participant_{unixTimestampSeconds++}.json");
                File.WriteAllText(fullpath, jsonObj.ToString());
                rowIndex++;
            }
            MessageBox.Show("Список сохранен");
            this.Close();

        }

        
    }



}
    

