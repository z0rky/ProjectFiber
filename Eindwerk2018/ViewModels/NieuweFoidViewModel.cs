﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class NieuweFoidViewModel
    {


        public Foid Foid { get; set; }

        public IEnumerable<Locatie> Locaties { get; set; }

        public IEnumerable<Sectie> Secties{ get; set; }


    }
}