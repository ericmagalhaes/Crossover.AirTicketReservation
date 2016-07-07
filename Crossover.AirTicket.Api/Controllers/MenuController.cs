using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace Crossover.AirTicket.Api.Controllers
{
    public class MenuController : ApiController
    {
        public List<Item> Get()
        {
            var menuList = new List<Item>();
            menuList.Add(new Item() {Name = "Flights", Url = "app.flight", Roles = new[] {"public", "staff"}});
            menuList.Add(new Item() { Name = "My Bookings", Url = "app.bookings", Roles = new[] { "public", "staff" } });
            menuList.Add(new Item() { Name = "Cancel booking", Url = "app.cancel-booking", Roles = new[] { "staff" } });
            menuList.Add(new Item() { Name = "Cancel flight", Url = "app.cancel-flight", Roles = new[] { "staff" } });
            return menuList;
        }

        public class Item
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string[] Roles { get; set; }
        }
    }
}

