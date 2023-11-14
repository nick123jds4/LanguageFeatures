using MvcModels.Interfaces;
using System;
using System.Collections.Generic;

namespace MvcModels.Models.Repostiory
{
    public class MemoryRepository : IRepository
    {
        #region _people
        private Dictionary<int, Person> _people = new Dictionary<int, Person>
        {
            [1] = new Person
            {
                Id = 1,
                FirstName = "ВоЬ",
                LastName = "Smith",
                Role = Role.Admin
            },
            [2] = new Person
            {
                Id = 2,
                FirstName = "Anne",
                LastName = "Douglas",
                Role = Role.User
            },
            [3] = new Person
            {
                Id = 3,
                FirstName = "Joe",
                LastName = "АЫе",
                Role = Role.User
            },
            [4] = new Person
            {
                Id = 4,
                FirstName = "Mary",
                LastName = "Peters",
                Role = Role.Guest
            }
        };
        #endregion
        public Person this[int id] {
            get => _people.ContainsKey(id) ? _people[id] : null;
            set => _people[id] = value;
        }

        public IEnumerable<Person> People => _people.Values;
    }
}
