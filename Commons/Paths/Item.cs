﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Paths
{
    public abstract class Item
    {
        public string Name { get; set; }

        public Item Parent { get; set; }
    }
}
