﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IPerson
    {
        string Initials { get; }

        int UserID { get; }

        int Age { get; }
    }
}
