using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Models
{
    /// <summary>
    /// Ccылка на аттрибуты анатаций
    /// https://metanit.com/sharp/aspnet5/19.6.php
    /// </summary>
    public class LoginModel
    {
        [Required]
        [UIHint("EmailAddress")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
