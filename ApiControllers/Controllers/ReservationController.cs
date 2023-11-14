using ApiControllers.Interfaces;
using ApiControllers.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly IRepository _repository; 
        public ReservationController(IRepository repository) => _repository = repository;

        [HttpGet]
        public IEnumerable<Reservation> Get() => _repository.Reservations;

        [HttpGet("{id}")]
        public Reservation Get(int id) => _repository[id]; 

        [HttpPost]
        public Reservation Post([FromBody]Reservation reservation) {
            var newReservation = new Reservation {
                ClientName = reservation.ClientName,
                Location = reservation.Location
            };

            return _repository.Add(newReservation);
        }

        /// <summary>
        /// Обновляет ресурс полностью
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPut]
        public Reservation Put([FromBody]Reservation reservation) {
            return _repository.Update(reservation);
        }

        /// <summary>
        /// Обновляет элемент ресурса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Reservation> patch) {
            var result = Get(id);
            if (result != null) {
                patch.ApplyTo(result, LogError);

                return Ok();
            }

            return NotFound();
        }

        private void LogError(JsonPatchError obj)
        {
             
        }

        [HttpDelete]
        public void Delete(int id) {
            _repository.Delete(id);
        }
    }
}
