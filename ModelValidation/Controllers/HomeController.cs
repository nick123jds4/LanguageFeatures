using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidation.Models;
using System;

namespace ModelValidation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("MakeBooking", new Appointment { Date = DateTime.Now });
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appointment)
        {
            //if (string.IsNullOrEmpty(appointment.ClientName))
            //{
            //    ModelState.AddModelError(nameof(appointment.ClientName), "Please enter your name");
            //}

            //if (ModelState.GetValidationState("Date") == ModelValidationState.Valid && DateTime.Now > appointment.Date)
            //{
            //    ModelState.AddModelError(nameof(appointment.Date), "Please enter a date in the future");
            //}

            //if (!appointment.TermsAccepted)
            //{
            //    ModelState.AddModelError(nameof(appointment.TermsAccepted), "You must accept the terms");
            //}
            if (ModelState.GetValidationState(nameof(appointment.Date)) == ModelValidationState.Valid
                && ModelState.GetValidationState(nameof(appointment.ClientName)) == ModelValidationState.Valid
                && appointment.ClientName.Equals("Joe", StringComparison.OrdinalIgnoreCase)
                && appointment.Date.DayOfWeek == DayOfWeek.Monday)
            {
                ModelState.AddModelError(string.Empty, "Joe cannot book appointments on Mondays");
            }
            if (ModelState.IsValid)
            {
                return View("Completed", appointment);
            }

            return View();
        }

        //Удаленная проверка на сервере
        public JsonResult ValidateDate(string Date)
        {

            if (!DateTime.TryParse(Date, out var parseDate))
            {
                return Json("Please enter a valid date (mm/dd/yyyy)");
            }
            else if (DateTime.Now > parseDate)
            {
                return Json("Please enter a date in the future");
            }
            else
            {
                return Json(true);
            }
        }


    }
}
