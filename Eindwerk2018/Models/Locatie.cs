using System;
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

        [Required]
        [Range(-180, 180)]
        [Display(Name = "LocationGpsLong", ResourceType = typeof(Resources.Resource))]
        public double GpsLong { get; set; }

        [Required]
        [Range(-90, 90)]
        [Display(Name = "LocationGpsLat", ResourceType = typeof(Resources.Resource))]
        public double GpsLat { get; set; }

        [Display(Name = "LocationInfrabel", ResourceType = typeof(Resources.Resource))]
        public bool LocatieInfrabel { get; set; }

        [StringLength(200)]
        [Display(Name = "LocationBedrijf", ResourceType = typeof(Resources.Resource))]
        public string LocatieBedrijf { get; set; }

        [Display(Name = "LocationTypeId", ResourceType = typeof(Resources.Resource))]
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