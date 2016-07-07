using System;
using Crossover.AirTicket.Core.Common;

namespace Crossover.AirTicket.Logic.Domain
{
    public class Location : Entity
    {
        public Location(string id,string name,string state,string country)
        {
            Id = id;
            Name = name;
            State = state;
            Country = country;
        }

        public string Name { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
    }
}