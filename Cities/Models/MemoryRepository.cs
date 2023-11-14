using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities.Models
{
    public class MemoryRepository : IRepository
    {
        private List<City> _cities = new List<City> {
            new City { Name = "London", Country = "UK", Population = 8539000 },
            new City { Name = "New York", Country = "USA", Population = 8406000 } ,
            new City { Name = "San Jose", Country = "USA", Population = 998537 },
            new City { Name = "Paris", Country = "France", Population = 2244000 },
        };

        public IEnumerable<City> Cities => _cities;

        public void AddCity(City city)
        {
            _cities.Add(city);
        }
    }
}
