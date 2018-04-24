﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class Locatie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "LocationName", ResourceType = typeof(Resources.Resource))]
        public string LocatieNaam { get; set; }

        //[Range(-180, 180)] //makes it required, not something we want
        [Display(Name = "LocationGpsLong", ResourceType = typeof(Resources.Resource))]
        public double GpsLong { get; set; }

        //[Range(-90, 90)] //makes it required, not something we want
        [Display(Name = "LocationGpsLat", ResourceType = typeof(Resources.Resource))]
        public double GpsLat { get; set; }

        [Display(Name = "LocationInfrabel", ResourceType = typeof(Resources.Resource))]
        public bool LocatieInfrabel { get; set; }

        [StringLength(200)]
        [Display(Name = "LocationBedrijf", ResourceType = typeof(Resources.Resource))]
        public string LocatieBedrijf { get; set; }

        [Display(Name = "LocationTypeId", ResourceType = typeof(Resources.Resource))]
        public int LocatieTypeId { get; set; }

        [Display(Name= "AdresStraat", ResourceType = typeof(Resources.Resource))]
        public string Straat { get; set; }

        [Display(Name="AdresNummer", ResourceType = typeof(Resources.Resource))]
        public string HuisNr { get; set; }

        //[Range(999, 9999)] //makes it required, not something we want
        [Display(Name = "AdresPostcode", ResourceType = typeof(Resources.Resource))]
        public int PostCode { get; set; }

        [Display(Name = "AdresGemeente", ResourceType = typeof(Resources.Resource))]
        public string Plaats { get; set; }

        //[Range(8,8)]  //makes it required, not something we want
        [Display(Name = "LocationLcode", ResourceType = typeof(Resources.Resource))]
        public String Lcode { get; set; }

        [Display(Name = "LocationLijnNr", ResourceType = typeof(Resources.Resource))]
        public String LijnNr { get; set; }

        [Display(Name = "LocationBK", ResourceType = typeof(Resources.Resource))]
        public int BK { get; set; }
    }
}