using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Handlers;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleInjector;

namespace Crossover.AirTicket.Logic.Demo
{
    public static class Bootstrap
    {
        public static string Brasilia = "BSB";
        public static string Orlando = "ORL";
        public static string Paris = "CDG";
        public static string Quebec = "YQB";
        public static void InitDatabase()
        {
            var client = new MongoClient();
            var database = client.GetServer().GetDatabase("AirTicketReservation");
            database.DropCollection("Flight");
            database.CreateCollection("Flight");
            var flightCollection = database.GetCollection<Flight>("Flight");
            var betweenDays = 15;
            var flights = new List<Flight>();
            for (int i = 0; i < 150; i++)
            {
                var flight = new Flight();
                var random = new Random();
                var daysRandom = random.Next(0, betweenDays);
                var hoursRandom = random.Next(0, 24);
                flight.Departure = DateTime.Now.AddDays(daysRandom).AddHours(hoursRandom);
                var landingHoursRandom = random.Next(1, 5);
                var landingMinutesRandom = random.Next(1, 60);
                flight.Landing = flight.Departure.AddHours(landingHoursRandom).AddMinutes(landingMinutesRandom);
                var locations = new List<Location>()
                {
                    new Location(Brasilia,"Brasilia","Distrito Federal","Brazil" ),
                    new Location(Orlando,"Orlando","Florida","United States" ),
                    new Location(Paris,"Paris","Ilê-de-France","França" ),
                    new Location(Quebec,"Quebec","Quebec","Canada" ),
                    new Location(Brasilia,"Brasilia","Distrito Federal","Brazil" ),
                    new Location(Orlando,"Orlando","Florida","United States" ),
                    new Location(Paris,"Paris","Ilê-de-France","França" ),
                    new Location(Quebec,"Quebec","Quebec","Canada" ),
                };
                var to = random.Next(0, 7);
                flight.To = locations[to];
                var from = random.Next(0, 7);
                if (to == from)
                    from = (from == 0) ? (from + 1) : (from - 1);
                flight.From = locations[from];
                flight.Name = $"{flight.To.Name.Substring(0, 3)}{flight.Departure.Month}{flight.Departure.Day}".ToUpper();
                flight.Closed = false;
                var price = random.Next(150, 200);
                flight.Price = price;
                var seatList = new List<Seat>();
                for (int j = 0; j < 150; j++)
                {
                    var seatNumber = flight.Name + "S" + j;
                    var seat = new Seat(seatNumber);
                    seatList.Add(seat);
                }
                flight.Seats = seatList.ToArray();
                flight.Closed = false;
                flight.Id = ObjectId.GenerateNewId().ToString();
                flights.Add(flight);
            }

            flightCollection.InsertBatch(flights);

        }

        public static Container InitContainer()
        {
            var container = new Container();
            var logicAssembly = Assembly.GetExecutingAssembly();
            container.Register(typeof(IRepository<>), typeof(MongoRepository<>));
            container.Register<IQueryDispatcher, QueryDispatcher>();
            container.Register<ICommandDispatcher, CommandDispatcher>();
            container.Register(typeof (IQueryHandler<,>), typeof (FlightsQueryHandler));
            container.Register(typeof(ICommandHandler<>), typeof(FlightsCommandHandler));








            container.RegisterCollection(typeof(ICommandHandler<>), Assembly.GetExecutingAssembly());
            return container;
        }
    }
}
