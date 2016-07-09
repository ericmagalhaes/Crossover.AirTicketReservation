using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Crossover.AirTicket.Core.Infraestructure
{
    public interface IEmailService
    {
        bool Send(IEmailMessage mailMessage);
    }
}
