using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Crossover.AirTicket.Core.Infraestructure
{
    public static class AirTicketSettings
    {
        public static string SendGridApiKey => KeyValue();

        public static string KeyValue([CallerMemberName] string propertyName = null)
        {
            return ConfigurationManager.AppSettings[propertyName];
        }
    }
}
