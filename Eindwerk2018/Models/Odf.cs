﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eindwerk2018.Models
{
    public class Odf
    {
        int id;
        int location_id, type_id;
        String name;

        public int Id { get; set; }
        public int Location_id { get; set; }
        public int Type_id { get; set; }
        public string Name { get; set; }
    }
}
