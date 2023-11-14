using Microsoft.AspNetCore.Mvc;
using MvcModels.Interfaces;
using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcModels.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository) => _repository = repository;
        public IActionResult Index([FromQuery]int? id=1)
        {
            Person person;
            if (id.HasValue && (person = _repository[id.Value]) != null)
                return View(person);

            return NotFound();
        }

        public ViewResult Create() => View(new Person());

        [HttpPost]
        public ViewResult Create(Person model) => View("Index", model);

        //    public ViewResult DisplaySummary([Bind(include:nameof(AddressSummary.City), Prefix = nameof(Person.HomeAddress))] AddressSummary summary) => View(summary);

        public ViewResult DisplaySummary(AddressSummary summary) => View(summary);

        //public ViewResult Names(string[] names) => View(names??new string[0]);

        public ViewResult Names(IList<string> names) =>
            View(names ?? new List<string>());


        public ViewResult Address(IList<AddressSummary> summaries) =>
            View(summaries??new List<AddressSummary>());

        //public string Header([FromHeader(Name ="Accept-Language")] string accept) =>
        //    $"Header: {accept}";
        public ViewResult Header(HeaderModel model) => View(model);

        public ViewResult Body() => View();

        [HttpPost]
        public Person Body([FromBody] Person model) => model;
    }

}
