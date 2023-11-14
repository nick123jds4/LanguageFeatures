using ApiControllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ContentController : Controller
    {
        /// <summary>
        /// ContentType="text/plain"
        /// </summary>
        /// <returns></returns>
        [HttpGet("string")]
        public string GetString() {
            return "This is a string response";
        }

        /// <summary>
        /// ContentType="application/json"
        /// </summary>
        /// <returns></returns>
        [HttpGet("object")]
        [Produces("application/json")]
        public Reservation GetObject() {
            var reservation = new Reservation() { 
            Id = 100,
            ClientName="Joe",
            Location = "Board Room"
            };

            return reservation;
        }
    }
}
