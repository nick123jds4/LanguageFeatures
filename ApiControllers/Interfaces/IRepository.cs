using ApiControllers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControllers.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Reservation> Reservations { get;}
        Reservation this[int id] { get; }
        Reservation Add(Reservation reservation);
        Reservation Update(Reservation reservation);
        void Delete(int id);
    }
}
