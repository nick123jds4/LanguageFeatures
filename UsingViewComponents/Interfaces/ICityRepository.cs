using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingViewComponents.Models;

namespace UsingViewComponents.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> Cities { get; }
        void AddCity(City city);
    }
}
