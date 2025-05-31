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
        public LotteryArchive()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            T newForm = new T();
            newForm.Show();
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
