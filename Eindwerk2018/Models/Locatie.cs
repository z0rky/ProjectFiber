﻿using System;
using System.ComponentModel.DataAnnotations;



namespace Eindwerk2018.Models
{
    public class Locatie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Naam")]
        public string LocatieNaam { get; set; }

        [Required]
        [Range(-180, 180)]
        [Display(Name = "GPS Lengtegraad")]
        public double GpsLong { get; set; }

        [Required]
        [Range(-90, 90)]
        [Display(Name = "GPS Breedtegraad")]
        public double GpsLat { get; set; }

        [Display(Name = "Infrabel Locatie ")]
        public bool LocatieInfrabel { get; set; }

        [StringLength(200)]
        [Display(Name = "Bedrijf")]
        public string LocatieBedrijf { get; set; }


        [Display(Name = "Type van de locatie")]
        public int LocatieTypeId { get; set; }

        //public Locatie(int id, string naam, double lon, double lat, bool infra, int type)
        //{
        //    Id = Id;
        //    LocatieNaam = naam;
        //    GpsLong = lon;
        //    GpsLat = lat;
        //    LocatieInfrabel = infra;
        //    LocatieTypeId = type;

        //}

    }
}