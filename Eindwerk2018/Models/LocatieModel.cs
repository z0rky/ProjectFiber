using System;
namespace Eindwerk2018.Models
{
    public class LocatieModel
    {
        public LocatieModel()
        {
        }

        public String LocatieNaam
        {
            get;
            set;
        }
        public String GpsLong
        {
            get;
            set;
        }
		
        public String GpsLat
        {
            get;
            set;
        }

        public String LocatieCode
        {
            get;
            set;
        }

        public Boolean LocatieInfrabel
        {
            get;
            set;
        }

        public Int16 LocatieType
        {
            get;
            set;
        }
   
    }
}
