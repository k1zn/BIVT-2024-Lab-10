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
    public partial class RandomFill : Form
    {
        public int count {  get; private set; }
        public RandomFill()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                MessageBox.Show("Некорректный ввод");
                return;
            }
        }
    }
}
