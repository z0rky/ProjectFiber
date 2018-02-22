using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class History
    {
        [Display(Name = "HistoryDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreatieDatum { get; set; }

        [Display(Name = "HistoryTable", ResourceType = typeof(Resources.Resource))]
        public String Table { get; set; }

        [Display(Name = "HistoryId", ResourceType = typeof(Resources.Resource))]
        public int Id { get; set; }

        [Display(Name = "HistoryUser", ResourceType = typeof(Resources.Resource))]
        public int User { get; set; }

        [Display(Name = "HistoryText", ResourceType = typeof(Resources.Resource))]
        public String Text { get; set; }
    }
}