using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcModels.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Person> People { get; }
        Person this[int id] { get;set; }
    }
}
