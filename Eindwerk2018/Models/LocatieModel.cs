using System;
using System.ComponentModel.DataAnnotations;
using Eindwerk2018.Models;


namespace Eindwerk2018.Models
{
    public class LocatieModel
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
        public object GpsLat { get; set; }

        [Display(Name = "Infrabel Locatie ?")]
        public bool LocatieInfrabel { get; set; }

        [Display(Name = "Type van de locatie")]
        public LocatieType locatieType { get; set; }

        public string Ja { get; set; }
        public string Neen { get; set; }

    }
}