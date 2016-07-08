using System.Configuration;
using System.Linq;
using Crossover.AirTicket.Core.Common;
using MongoDB.Driver;

namespace Crossover.AirTicket.Core.Repository
{
    public interface IRepository<TEntity> where TEntity:class
    {
        IQueryable<TEntity> AsQueryable();
        TEntity Save(TEntity entity);
    }

    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity:Entity
    {
        private readonly IMongoCollection<TEntity> _mongoCollection = null;

        public MongoRepository()
        {
            var dataBase = ConfigurationManager.AppSettings["MongoDB"];
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase(dataBase);
            _mongoCollection = database.GetCollection<TEntity>(typeof (TEntity).Name);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _mongoCollection.AsQueryable();
        }

        public TEntity Save(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                _mongoCollection.InsertOne(entity);
            }
            else
            {
                _mongoCollection.FindOneAndReplace(e => e.Id == entity.Id, entity);
            }

            return entity;
        }

    }
}
