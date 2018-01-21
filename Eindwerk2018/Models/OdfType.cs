using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eindwerk2018.Models
{
    public class OdfType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public OdfType(int id, string name, string description)
        {
            this.Id=id;
            this.Name = name;
            this.Description = description;
        }
    }
}
