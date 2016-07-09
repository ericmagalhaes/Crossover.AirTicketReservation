using System;
using System.Runtime.CompilerServices;

namespace Crossover.AirTicket.Core.Exception
{
    public class AirTicketException : ApplicationException
    {
        public AirTicketException(string message) : base(message)
        {
            
        }
    }

    public class AirTicketApplicationException : ApplicationException
    {
        public AirTicketApplicationException(string message)
        {

        }

        /// <summary>
        /// boundedContext should contains os variables used in the context for future 
        /// exception review
        /// </summary>
        /// <param name="message"></param>
        /// <param name="boundedContext"></param>
        /// <param name="member"></param>
        /// <param name="sourceFile"></param>
        /// <param name="sourceLine"></param>
        public AirTicketApplicationException(string message, object[] boundedContext,[CallerMemberName]string member="",[CallerFilePath] string sourceFile="",[CallerLineNumber]int sourceLine=0)
        {

        }
    }
}