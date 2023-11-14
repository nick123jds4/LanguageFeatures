using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingViewComponents.Interfaces;

namespace UsingViewComponents.Components
{
    public class PocoViewComponent
    {
        private readonly ICityRepository _cityRepository;

        public PocoViewComponent(ICityRepository cityRepository) => 
            _cityRepository = cityRepository;

        public string Invoke() {
            var str = $"{_cityRepository.Cities.Count()} cities, " +
                $"{_cityRepository.Cities.Sum(c=>c.Population)} people";

            return str;
        }
    }
}
