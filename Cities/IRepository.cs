using Cities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities
{
    public interface IRepository
    {
        IEnumerable<City> Cities { get; }
        void AddCity(City city);
    }
}
