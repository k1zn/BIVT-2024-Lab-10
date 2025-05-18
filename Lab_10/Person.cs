using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    public class Person : IPerson
    {
        public string Initials
        {
            get; private set;
        }

        public int UserID {
            get; private set;
        }

        public int Age
        {
            get; private set;
        }

        private static int _userIdCounter = 0;

        public Person(string name, string surname, int age)
        {
            Initials = name + " " + surname[0] + ".";
            UserID = _userIdCounter++;
            Age = age;
        }
    }
}
