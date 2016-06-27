using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baibaomen.CA.ViewModels
{
    /// <summary>
    /// login view.
    /// </summary>
    public class LoginView {
        /// <summary>
        /// user account.
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// user password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}