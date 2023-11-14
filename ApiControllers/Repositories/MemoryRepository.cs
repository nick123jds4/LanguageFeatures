using ApiControllers.Interfaces;
using ApiControllers.Models;
using System;
using System.Collections.Generic;

namespace ApiControllers.Repositories
{
    public class MemoryRepository : IRepository
    {
        private readonly Dictionary<int, Reservation> _items;
        public MemoryRepository()
        {
            _items = new Dictionary<int, Reservation>();
            new List<Reservation>() {
                new Reservation{ ClientName = "Alice", Location="Board Room" },
                new Reservation{ ClientName = "Bob", Location="Lecture Hall" },
                new Reservation{ ClientName = "Joe", Location="Meeting Room 1" },  
            }.ForEach(r=>Add(r));
        }
        public Reservation this[int id] => _items.ContainsKey(id)?_items[id]:null;

        public IEnumerable<Reservation> Reservations => _items.Values;

        public Reservation Add(Reservation reservation)
        {
            if (reservation.Id == 0)
            {
                int key = _items.Count;
                while (_items.ContainsKey(key))
                    key++;
                reservation.Id = key;
            }
            _items[reservation.Id] = reservation;

            return reservation;
        }

        public void Delete(int id)
        {
            if (_items.ContainsKey(id))
                _items.Remove(id);
        }

        public Reservation Update(Reservation reservation)
        {
           return Add(reservation);
        }
    }
}
