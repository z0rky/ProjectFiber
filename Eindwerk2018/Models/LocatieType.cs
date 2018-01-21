using System;
using System.ComponentModel.DataAnnotations;
            
namespace Eindwerk2018.Models
{
    public class LocatieType
    {

        
        public int Id   {get;set;}

        [StringLength(45)]
        public string naamNL    {get;set;}

        [StringLength(45)]
        public string naamFR    {get;set;}

        [StringLength(250)]
        public string descNL    {get;set;}

        [StringLength(250)]
        public string descFR    {get;set;}

        public LocatieType(int id, string nl, string fr, string descnl, string descfr)
        {
            Id = id;
            naamNL = nl;
            naamFR = fr;
            descNL = descNL;
            descFR = descFR;
        }


    }
}
