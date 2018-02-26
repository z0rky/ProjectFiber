using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eindwerk2018.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "UserFirstName", ResourceType = typeof(Resources.Resource))]
        public String FirstName { get; set; }

        [Display(Name = "UserLastName", ResourceType = typeof(Resources.Resource))]
        public String LastName { get; set; }

        [Required]
        [Display(Name = "UserUserName", ResourceType = typeof(Resources.Resource))]
        public String UserName { get; set; }

        [Display(Name = "UserEmail", ResourceType = typeof(Resources.Resource))]
        public String Email { get; set; }
    }
}