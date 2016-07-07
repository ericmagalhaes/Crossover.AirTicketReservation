using System;
using SimpleInjector;

namespace Crossover.AirTicket.Core.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Container _serviceProvider;

        public QueryDispatcher(Container serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            _serviceProvider = serviceProvider;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _serviceProvider.GetInstance<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }

    }
}