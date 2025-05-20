using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public static class DataTransfer
    {
        public static string LastFilePath { get; set; }

        public static void Clear()
        {
            LastFilePath = string.Empty;
        }
    }

}
