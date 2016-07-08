using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossover.AirTicket.Core.Security
{
    public interface ISecurityContext
    {
        string UserId { get; set; }
        string UserEmail { get; set; }
    }

    public class MockedSecurityContext : ISecurityContext
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }

        public MockedSecurityContext(string userId)
        {
            UserId = userId;
        }
    }
}
