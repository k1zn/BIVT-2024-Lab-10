using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public LotteryArchive()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            createdForms = new MyForm[] { new LotteryCreate(), new LotteryStats(), new ParticipantsTable() };
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
                    } else
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
    }
}
