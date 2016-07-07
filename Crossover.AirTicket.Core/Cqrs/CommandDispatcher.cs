using System;
using SimpleInjector;

namespace Crossover.AirTicket.Core.Cqrs
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Container _container;

        public CommandDispatcher(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("kernel");
            }
            
            _container = container;
        }

        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _container.GetInstance<ICommandHandler<TParameter>>();
            handler.Execute(command);
        }

    }
}