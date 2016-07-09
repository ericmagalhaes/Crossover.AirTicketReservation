using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Crossover.AirTicket.Core.Cqrs;
using Crossover.AirTicket.Core.Queue;
using Crossover.AirTicket.Core.Repository;
using Crossover.AirTicket.Core.Security;
using Crossover.AirTicket.Logic.Domain;
using Crossover.AirTicket.Logic.Handlers;
using Crossover.AirTicket.Logic.Queue;
using Crossover.AirTicket.Logic.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Messaging;
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
            var database = client.GetDatabase("AirTicketReservation");
            database.DropCollection("Flight");
            database.CreateCollection("Flight");
            var flightCollection = database.GetCollection<Flight>("Flight");
            var betweenDays = 15;
            var flights = new List<Flight>();
            for (int i = 0; i < 150; i++)
            {
               
                var random = new Random();
                var daysRandom = random.Next(0, betweenDays);
                var hoursRandom = random.Next(0, 24);
                var departure = DateTime.Now.AddDays(daysRandom).AddHours(hoursRandom);
                var landingHoursRandom = random.Next(1, 5);
                var landingMinutesRandom = random.Next(1, 60);
                var landing = departure.AddHours(landingHoursRandom).AddMinutes(landingMinutesRandom);
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
                var To = locations[to];
                var from = random.Next(0, 7);
                if (to == from)
                    from = (from == 0) ? (from + 1) : (from - 1);
                var From = locations[from];
                var Name = $"{To.Name.Substring(0, 3)}{departure.Month}{departure.Day}".ToUpper();
                var flight = new Flight(Name, From, To, departure, landing, 150);
                
                var price = random.Next(150, 200);
                flight.AjustPrice(price);
                //var seatList = new List<Seat>();
                //for (int j = 0; j < 150; j++)
                //{
                //    var seatNumber = flight.Name + "S" + j;
                //    var seat = new Seat(seatNumber);
                //    seatList.Add(seat);
                //}
                flight.Id = ObjectId.GenerateNewId().ToString();
                flights.Add(flight);
            }

            flightCollection.InsertMany(flights);

        }

        public static Container InitContainer()
        {
            var container = new Container();
            var logicAssembly = Assembly.GetExecutingAssembly();
            container.Register(typeof(ISecurityContext), ()=> new MockedSecurityContext("ericm"));
            container.Register(typeof(IRepository<>), new[] { typeof(MongoRepository<>).Assembly});
            container.Register(typeof(IRepository<>), new[] { typeof(Event).Assembly });
            container.Register(typeof(IRepository<Flight>), typeof(MongoRepository<Flight>));
            container.Register(typeof(IRepository<Booking>), typeof(MongoRepository<Booking>));
            container.Register(typeof(IRepository<Event>), typeof(MongoRepository<Event>));
            container.Register(typeof(IRepository<EmailNotification>), typeof(MongoRepository<EmailNotification>));
            container.Register<IQueryDispatcher, QueryDispatcher>();
            container.Register<ICommandDispatcher, CommandDispatcher>();
            container.Register<IEventDispatcher, EventDispatcher>();

            container.Register(typeof (IQueryHandler<,>), typeof (FlightsQueryHandler));
            container.Register(typeof(ICommandHandler<>), typeof(BookingCommandHandler));
            //container.Register(typeof (IEventHandler<>), typeof (NotificationEventHandler));
            var logic = typeof(BookingRequestCreateEvent).Assembly;
            var registrations =
                logic.GetExportedTypes()
                    .Where(xt => xt.GetInterfaces().Count(i => i.Name == typeof(IEventHandler<>).Name) > 0);

            container.RegisterCollection(typeof(IEventHandler<>), registrations);
            container.Register(typeof (FlightBookingRepository), typeof (FlightBookingRepository));
            container.Register(typeof(IEmailNotificationQueue<EmailNotification>),typeof(MongoEmailNotificationQueue));
            container.Verify();
            
            //container.RegisterCollection(typeof(ICommandHandler<>), Assembly.GetExecutingAssembly());
            return container;
        }

        public static void InitQueue()
        {
            MongoEmailNotificationQueue.Init();
        }
    }
}
