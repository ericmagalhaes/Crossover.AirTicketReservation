using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Crossover.AirTicket.Core.Repository
{
    public interface IRepository<TEntity> where TEntity:class
    {
        IQueryable<TEntity> AsQueryable();
        TEntity Insert(TEntity entity);
    }

    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity:class
    {
        private readonly MongoCollection<TEntity> _mongoCollection = null;

        public MongoRepository()
        {
            var dataBase = ConfigurationManager.AppSettings["MongoDB"];
            var mongoClient = new MongoClient();
            var database = mongoClient.GetServer().GetDatabase(dataBase);
            _mongoCollection = database.GetCollection<TEntity>(typeof (TEntity).Name);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _mongoCollection.AsQueryable<TEntity>();
        }

        public TEntity Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }
        
    }
}
