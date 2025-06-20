﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class Person : IPerson
    {
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public int UserID
        {
            get; private set;
        }

        public int Age
        {
            get; private set;
        }

        protected string _passportInfo;

        private static int _userIdCounter = 0;

        public Person(string name, string surname, string passportInfo, int age)
        {
            Name = name;
            Surname = surname;
            UserID = _userIdCounter++;
            Age = age;

            _passportInfo = passportInfo;
        }

        public string GetPassportInfo(string authKey)
        {
            if (authKey == "admin")
                return _passportInfo;
            else
                return "";
        }
    }
}
