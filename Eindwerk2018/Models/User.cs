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

        [Required(ErrorMessageResourceName = "ErrorFieldRequired", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "UserUserName", ResourceType = typeof(Resources.Resource))]
        public String UserName { get; set; }

        [EmailAddress(ErrorMessageResourceName = "ErrorFieldEmail", ErrorMessageResourceType = typeof(Resources.Resource))]
        //some jquery get in front of this before sending to controler.
        [Display(Name = "UserEmail", ResourceType = typeof(Resources.Resource))]
        public String Email { get; set; }

        //om een combinatie te krijgen in de drop downlist, bv in Foid/create
        public String FullName { get { return FirstName+" "+LastName+" ("+UserName+")"; } }
    }
}