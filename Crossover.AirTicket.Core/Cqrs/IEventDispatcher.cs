namespace Crossover.AirTicket.Core.Cqrs
{
    public interface IEventDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <param name="evt">The command to be passed to the handler</param>
        void Raise<TParameter>(TParameter evt) where TParameter : Event;
    }
}