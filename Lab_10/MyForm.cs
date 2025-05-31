using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class MyForm : Form
    {
        protected static void ShowMsgBox(string text, bool isInfo)
        {
            MessageBox.Show(text, isInfo ? "Информация" : "Ошибка", MessageBoxButtons.OK, isInfo ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        public class LotteryPathEventArgs : EventArgs
        {
            public string LotteryPath { get; set; }
        }
    }
}
