using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HurtowniaMVC.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<string> RoleNames { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        [DisplayName("rola")]
        public string RoleName { get; set; }
        [DisplayName("nazwa użytkownika")]
        public string UserName { get; set; }

    }
}