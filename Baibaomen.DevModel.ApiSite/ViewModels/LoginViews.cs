using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baibaomen.DevModel.ApiSite
{
    public class LoginView {

        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}