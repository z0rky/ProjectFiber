﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class SearchFoidResultViewModel
    {
        public IEnumerable<Foid> GezochteFoids { get; set; }
    }
}