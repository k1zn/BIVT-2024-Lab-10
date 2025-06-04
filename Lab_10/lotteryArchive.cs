using Model.Core;
using Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_10
{
    public partial class LotteryArchive : MyForm
    {
        private MyForm[] createdForms;

        private static readonly string configSerializerPath = Path.Combine(Directory.GetCurrentDirectory(), "serializetype.txt");
        public LotteryArchive()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (!File.Exists(configSerializerPath))
            {
                File.WriteAllText(configSerializerPath, "json");
            }
            MyForm.SerializerType = File.ReadAllText(configSerializerPath);
            if (MyForm.SerializerType == "xml")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            UpdateSerializedFiles();

            createdForms = new MyForm[] { new LotteryCreate(), new LotteryStats(), new ParticipantsTable() };
        }

        private void UpdateSerializedFiles()
        {
            //throw new Exception("ive been called");
            var lotteriesPath = Path.Combine(Directory.GetCurrentDirectory(), "Lotteries");

            var jsonSerializer = new LotteryArchiveJSONSerializer();
            var xmlSerializer = new LotteryArchiveXMLSerializer();

            jsonSerializer.SelectFolder(lotteriesPath);
            xmlSerializer.SelectFolder(lotteriesPath);

            foreach (string file in Directory.GetFiles(lotteriesPath))
            {
                string extension = Path.GetExtension(file);
                if (extension == ".json" && MyForm.SerializerType == "xml")
                {
                    jsonSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    LotteryEvent lottery = jsonSerializer.DeserializeLottery();

                    xmlSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    xmlSerializer.SerializeLottery(lottery);

                    File.Delete(file);

                } else if (extension == ".xml" && MyForm.SerializerType == "json")
                {
                    xmlSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    LotteryEvent lottery = xmlSerializer.DeserializeLottery();

                    jsonSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    jsonSerializer.SerializeLottery(lottery);

                    File.Delete(file);
                }
            }

            var participantsPath = Path.Combine(Directory.GetCurrentDirectory(), "Participants");

            jsonSerializer.SelectFolder(participantsPath);
            xmlSerializer.SelectFolder(participantsPath);

            foreach (string file in Directory.GetFiles(participantsPath))
            {
                string extension = Path.GetExtension(file);
                if (extension == ".json" && MyForm.SerializerType == "xml")
                {
                    jsonSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    LotteryParticipant participant = jsonSerializer.DeserializeLotteryParticipant<object>(null);

                    xmlSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    xmlSerializer.SerializeLotteryParticipant(participant);

                    File.Delete(file);

                }
                else if (extension == ".xml" && MyForm.SerializerType == "json")
                {
                    xmlSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    LotteryParticipant participant = xmlSerializer.DeserializeLotteryParticipant<object>(null);

                    jsonSerializer.SelectFile(Path.GetFileNameWithoutExtension(file));
                    jsonSerializer.SerializeLotteryParticipant(participant);

                    File.Delete(file);
                }
            }
        }

        private void OpenAnyForm<T>() where T : MyForm, new()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is T)
                {
                    form.Focus();
                    return;
                }
            }

            for (int i = 0; i < createdForms.Length; i++)
            {
                MyForm form = createdForms[i];

                if (form is T targetForm)
                {
                    if (form.IsDisposed)
                    {
                        createdForms[i] = (MyForm)Activator.CreateInstance(form.GetType());
                        createdForms[i].Show();
                    }
                    else
                    {
                        form.Show();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenAnyForm<LotteryCreate>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenAnyForm<LotteryStats>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenAnyForm<ParticipantsTable>();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MyForm.SerializerType = "json";
            File.WriteAllText(configSerializerPath, "json");

            UpdateSerializedFiles();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MyForm.SerializerType = "xml";
            File.WriteAllText(configSerializerPath, "xml");

            UpdateSerializedFiles();
        }
    }
}
