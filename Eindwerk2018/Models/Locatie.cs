using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eindwerk2018.Models
{
    public class Locatie
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [StringLength(200)]
        [Display(Name = "LocationName", ResourceType = typeof(Resources.Resource))]
        public string LocatieNaam { get; set; }

        [Range(-180, 180)] //makes it required, not something we want
        [Display(Name = "LocationGpsLong", ResourceType = typeof(Resources.Resource))]
        public double? GpsLong { get; set; }

        [Range(-90, 90)] //makes it required, not something we want
        [Display(Name = "LocationGpsLat", ResourceType = typeof(Resources.Resource))]
        public double? GpsLat { get; set; }

        [Display(Name = "LocationInfrabel", ResourceType = typeof(Resources.Resource))]
        public bool LocatieInfrabel { get; set; }

        [StringLength(200)]
        [Display(Name = "LocationBedrijf", ResourceType = typeof(Resources.Resource))]
        public string LocatieBedrijf { get; set; }

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "LocationTypeId", ResourceType = typeof(Resources.Resource))]
        public int LocatieTypeId { get; set; }

        [Display(Name= "AdresStraat", ResourceType = typeof(Resources.Resource))]
        public string Straat { get; set; }

        [Display(Name="AdresNummer", ResourceType = typeof(Resources.Resource))]
        public string HuisNr { get; set; }

        [Range(999, 9999, ErrorMessageResourceName = "ErrorFieldNumber", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "AdresPostcode", ResourceType = typeof(Resources.Resource))]
        public int? PostCode { get; set; } //the ? makes it not required

        [Display(Name = "AdresGemeente", ResourceType = typeof(Resources.Resource))]
        public string Plaats { get; set; }

        [Range(8,8)]
        [Display(Name = "LocationLcode", ResourceType = typeof(Resources.Resource))]
        public String Lcode { get; set; }

        [Display(Name = "LocationLijnNr", ResourceType = typeof(Resources.Resource))]
        public String LijnNr { get; set; }

        //[Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorFieldNumber", ErrorMessageResourceType = typeof(Resources.Resource))]
        //[DataType(DataType.Currency, ErrorMessage = "......")]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")] //nope doesnt work
        //int check komt altijd eerst :-(
        [Display(Name = "LocationBK", ResourceType = typeof(Resources.Resource))]
        public int? BK { get; set; }
    }
}