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
    public partial class countParticipants : Form
    {
        public int count {  get; private set; }
        public countParticipants()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cnt;
            if (int.TryParse(textBox1.Text, out cnt))
            {
                count = cnt;
                Close();
            }
            else
            {
                MessageBox.Show("некорректный ввод");
                return;
            }
        }
    }
}
