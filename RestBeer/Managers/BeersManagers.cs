using BeerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestBeer.Managers
{
    public class BeersManagers
    {
        private static int _nextId = 1;
        private static readonly List<Beer> Data = new List<Beer>
        {
            new Beer("Turbog", 74, 4.6) {ID = _nextId++},
            new Beer("Gobrut", 47, 6.4) {ID = _nextId++},
            new Beer("Carlsberg", 73, 5) {ID = _nextId++}
        };

        public List<Beer> GetAll(string name = null, string sortBy = null)
        {
            List<Beer> beers = new List<Beer>(Data);

            if (name != null)
            {
                beers = beers.FindAll(beer => beer.Name.StartsWith(name));
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        beers = beers.OrderBy(beer => beer.Name).ToList();
                        break;
                    case "price":
                        beers = beers.OrderBy(beer => beer.Price).ToList();
                        break;
                    case "adv":
                        beers = beers.OrderBy(beer => beer.Abv).ToList();
                        break;
                }
            }
            return beers;
        }

        public Beer GetById(int id)
        {
            return Data.Find(beer => beer.ID == id);
        }

        public Beer Add(Beer newBeer)
        {
            newBeer.ID = _nextId++;
            Data.Add(newBeer);
            return newBeer;
        }

        public Beer Delete(int id)
        {
            Beer beer = Data.Find(beer1 => beer1.ID == id);
            if (beer == null) return null;
            Data.Remove(beer);
            return beer;
        }

        public Beer Update(int id, Beer updates)
        {
            Beer beer = Data.Find(beer1 => beer1.ID == id);
            if (beer == null) return null;
            beer.Name = updates.Name;
            beer.Price = updates.Price;
            beer.Abv = updates.Abv;
            return beer;
        }
    }
}
