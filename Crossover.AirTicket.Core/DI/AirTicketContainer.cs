using System.Security.Cryptography.X509Certificates;
using SimpleInjector;

namespace Crossover.AirTicket.Core.DI
{
    
    public static class AirTicketDI //teste
    {
        private static Container Container { get; set; }
        public static TInstance Get<TInstance>() where TInstance : class
        {
            return Container.GetInstance<TInstance>();
        }
    }
}
