using Microsoft.AspNetCore.Mvc;
using ModelValidation.Controllers;
using ModelValidation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelValidation.Models
{
    /// <summary>
    /// Класс - встреча
    /// </summary>
    public class Appointment
    {
        [Required]
        [Display(Name ="name")]
        public string ClientName { get; set; }
        [UIHint("Date")]
        [Required(ErrorMessage ="Please enter a date")]
        [Remote(action: nameof(HomeController.ValidateDate), controller:"Home")]
        public DateTime Date { get; set; }
        
        //[MustBeTrue(ErrorMessage ="You must accept the terms")]
        [Range(typeof(bool), "true", "true", ErrorMessage ="You must accept the terms")]
        public bool TermsAccepted { get; set; }
    }
}
