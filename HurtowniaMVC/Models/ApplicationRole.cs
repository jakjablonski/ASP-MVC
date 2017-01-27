using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }

        public ApplicationRole() : base()
        {
        }


        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}