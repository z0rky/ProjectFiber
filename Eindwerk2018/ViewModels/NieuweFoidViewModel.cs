using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eindwerk2018.Models;

namespace Eindwerk2018.ViewModels
{
    public class NieuweFoidViewModel
    {
        public Foid Foid { get; set; }
        public int OldStatus { get; set; }

        public IEnumerable<User> Users { get; set; }

        //status for edit
        public IEnumerable<Status> Statuses { get; set; }
    }
}