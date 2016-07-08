namespace Crossover.AirTicket.Core.Cqrs
{
    public interface IEventHandler<in TParameter> where TParameter : IEvent
    {
        /// <summary>
        /// Executes a command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        void Raise(TParameter command);
    }
}