using Model;
using Model.Core;
using Model.Data;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Lab_10
{
    public partial class ParticipantsTable : MyForm
    {
        private class ParticipantHiddenData
        {
            private LotteryTicket[] _tickets;
            public LotteryTicket[] Tickets { get { return _tickets; } }

            public string PassportInfo { get; private set; }

            public ParticipantHiddenData(LotteryTicket[] tickets, string passportInfo)
            {
                _tickets = tickets;
                PassportInfo = passportInfo;
            }
        }

        private bool dataChanged = false;
        private string[] RandomNames = new string[] { "Максим", "Роберт", "Николай", "Михаил", "Эмин", "Алексей", "Артём", "Тимур", "Роман", "Сергей", "Леонид", "Иван", "Арсений", "Дмитрий", "Данил", "Глеб", "Фёдор", "Егор", "Демид", "Марк", "Александр", "Владимир", "Даниил", "Никита", "Константин", "Руслан", "Лев", "Григорий", "Пётр", "Ярослав", "Дамир", "Илья", "Георгий", "Захар", "Владислав", "Юрий", "Лука", "Денис", "Богдан", "Гордей", "Кирилл", "Степан", "Святослав", "Вадим", "Матвей", "Виктор", "Камиль", "Василий", "Павел", "Даниэль", "Андрей", "Артур", "Семён", "Платон", "Артемий", "Виталий", "Елисей", "Антон", "Тимофей", "Филипп", "Рустам", "Альберт", "Тихон", "Данила", "Родион", "Али", "Мирослав", "Евгений", "Давид", "Савелий", "Игорь", "Назар", "Валерий", "Олег", "Всеволод", "Арсен", "Макар", "Савва", "Адам", "Карим", "Вячеслав", "Станислав", "Эрик", "Мирон", "Герман", "Ян", "Марсель", "Анатолий", "Борис", "Ибрагим", "Леон", "Ростислав", "Серафим", "Демьян", "Яков", "Марат", "Аркадий", "Эмир", "Тигран", "Рафаэль", "Кира", "Анна", "Злата", "Евгения", "Софья", "Дарья", "Дарина", "Вероника", "Мария", "Аделина", "Анастасия", "Алиса", "Вера", "Виктория", "Сафия", "Варвара", "Полина", "Ева", "Арина", "Валерия", "Ульяна", "Малика", "Ариана", "Мирослава", "Есения", "Адель", "Василиса", "Элина", "София", "Кристина", "Александра", "Таисия", "Амалия", "Ирина", "Елизавета", "Аврора", "Мила", "Эмилия", "Агата", "Стефания", "Ангелина", "Екатерина", "Амина", "Милана", "Ксения", "Яна", "Лилия", "Елена", "Аяна", "Амелия", "Ника", "Маргарита", "Майя", "Алина", "Мира", "Алёна", "Марина", "Пелагея", "Юлия", "Камилла", "Ольга", "Алия", "Камила", "Марьям", "Любовь", "Татьяна", "Валентина", "Николь", "Светлана", "Ясмина", "Владислава", "Сабина", "Марьяна", "Антонина", "Лада", "Василина", "Лия", "Агния", "Мелания", "Айлин", "Мия", "Диана", "Ярослава", "Надежда", "Оливия", "Амира", "Наталья", "Фатима", "Алисия", "Эвелина", "Олеся", "Аиша", "Лидия", "Марианна", "Теона", "Альфия", "Медина", "Асия", "Лиана", "Зоя" };
        string[] RandomSurnames = new string[]
        {
            "Алексо", "Белозёро", "Ветерцо", "Горизо", "Дальино", "Еловцо", "Жемчужино", "Звенисо", "Кристалло", "Лугово",
"Медисо", "Новело", "Орионово", "Пастеро", "Радиано", "Селесто", "Темпесто", "Улыбко", "Фениксово", "Хармонио",
"Цветицо", "Чаривно", "Шансово", "Эквилибро", "Ювелиро", "Ясново", "Амплуа", "Бравуро", "Виражо", "Гармонио"
        };
        private List<LotteryParticipant> participants = new List<LotteryParticipant>();
        public ParticipantsTable()
        {
            InitializeComponent();

            LotteryCreate.LotteryCreated += HandleNewLottery;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            button3.Enabled = false;

            dataGridView1.RowsAdded += DataGridView1_RowsChanged;
            dataGridView1.RowsRemoved += DataGridView1_RowsChanged;

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            UpdateDataGridView();

        }

        private void UpdateDataGridView()
        {
            if (!(dataGridView1.Rows.Count == 0 || dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
                dataGridView1.Rows.Clear();

            var serializer = new LotteryArchiveJSONSerializer();
            serializer.SelectFolder(Path.Combine(Directory.GetCurrentDirectory(), "Participants"));
            foreach (var file in Directory.GetFiles(serializer.FolderPath))
            {
                serializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                LotteryParticipant participant = serializer.DeserializeLotteryParticipant<object>();
                if (participant == null)
                {
                    continue;
                }

                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = participant.Name;
                row.Cells[1].Value = participant.Surname;
                row.Cells[2].Value = participant.Age;
                row.Cells[3].Value = participant.Balance;
                row.Cells[4].Value = participant.Greed;

                row.Tag = new ParticipantHiddenData(participant.Tickets, participant.GetPassportInfo("admin"));

                dataGridView1.Rows.Add(row);
            }
        }

        private void HandleNewLottery(object sender, MyForm.LotteryPathEventArgs e)
        {
            UpdateDataGridView();
        }

        private void addParticipants(int cnt)
        {

            var rand = new Random();
            for (int i = 0; i < cnt; i++)
            {
                var name = RandomNames[rand.Next(RandomNames.Length)];
                var surname = RandomSurnames[rand.Next(RandomSurnames.Length)];
                var age = rand.Next(18, 46);
                var balance = rand.Next(100, 1000);
                var greed = rand.Next(0, 100);

                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = name;
                row.Cells[1].Value = surname;
                row.Cells[2].Value = age;
                row.Cells[3].Value = balance;
                row.Cells[4].Value = greed;

                row.Tag = new ParticipantHiddenData(new LotteryTicket[0], getPassportInfo());

                dataGridView1.Rows.Add(row);
            }
            this.Text = "(*) Таблица участников";
            dataChanged = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (dataChanged)
            {
                DialogResult result = MessageBox.Show(
                    "Вы хотите сохранить изменения?",
                    "Подтверждение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    saveTable(false);
                    LotteryCreate.LotteryCreated -= HandleNewLottery;
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            } else
            {
                LotteryCreate.LotteryCreated -= HandleNewLottery;
            }

                base.OnFormClosing(e);
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataChanged = true;
            this.Text = "(*) Таблица участников";
        }

        private void DataGridView1_RowsChanged(object sender, EventArgs e)
        {
            button3.Enabled = !(dataGridView1.Rows.Count == 0 || dataGridView1.Rows.Count == 1 && dataGridView1.AllowUserToAddRows && dataGridView1.Rows[0].IsNewRow);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveTable(true);
        }

        private string getPassportInfo()
        {
            var rand = new Random();
            return rand.Next(1, 10000).ToString() + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();        
        }

        private void saveTable(bool showMsgBox)
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

            long unixTimestampSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var rowIndex = row.Index + 1;

                if (!int.TryParse(row.Cells["Age"].Value?.ToString(), out int age))
                {
                    ShowMsgBox($"Ошибка в строке {rowIndex}: возраст должен быть числом", false);
                    return;
                }

                if (!decimal.TryParse(row.Cells["Balance"].Value?.ToString(), out decimal balance))
                {
                    ShowMsgBox($"Ошибка в строке {rowIndex}: баланс должен быть числом", false);
                    return;
                }
                else if (balance < 0)
                {
                    ShowMsgBox($"Ошибка в строке {rowIndex}: баланс должен быть положительным", false);
                    return;
                }

                if (!int.TryParse(row.Cells["Greed"].Value?.ToString(), out int greed))
                {
                    ShowMsgBox($"Ошибка в строке {rowIndex}: жадность должна быть числом", false);
                    return;
                }
                else if (greed > 100 || greed < 0)
                {
                    ShowMsgBox($"Ошибка в строке {rowIndex}: значение жадности должно находиться в диапазоне от 0 до 100", false);
                    return;
                }

                var name = row.Cells["Name"].Value.ToString();
                var surname = row.Cells["Surname"].Value.ToString();
                ParticipantHiddenData secret = null;
                if (row.Tag != null)
                    secret = (ParticipantHiddenData)row.Tag;
                var passportInfo = (secret?.PassportInfo != null) ? secret?.PassportInfo : getPassportInfo();
                var participant = new LotteryParticipant(name, surname, age, balance, passportInfo, greed);
                if (secret?.Tickets != null)
                {
                    foreach (LotteryTicket ticket in secret.Tickets)
                    {
                        if (ticket == null) continue;

                        participant.AddTicket(ticket);
                    }
                }

                var serializer = new LotteryArchiveJSONSerializer();
                serializer.SelectFolder(Path.Combine(Directory.GetCurrentDirectory(), "Participants"));
                serializer.SelectFile($"Participant_{participant.FullName}_{participant.GetPassportInfo("admin")}");
                serializer.SerializeLotteryParticipant(participant);
            }
            if (showMsgBox)
            {
                ShowMsgBox("Список сохранен", true);
            }

            dataChanged = false;
            this.Text = "Таблица участников";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Participants");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            button3.Enabled = false;

            dataChanged = true;
            this.Text = "(*) Таблица участников";

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string textboxValue = textBox1.Text;
            int count;
            if (!int.TryParse(textboxValue, out count) || count <= 0)
            {
                ShowMsgBox("Указано некорректное число", false);
                return;
            }
            addParticipants(count);
        }
    }



}
    

