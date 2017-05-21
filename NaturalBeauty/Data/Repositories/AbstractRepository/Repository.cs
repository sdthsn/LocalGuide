using Data.Common;
using System.Linq;
using MongoDB.Driver;
using System.Diagnostics;
using System.Threading.Tasks;
using Data.Interfaces.AbstractInterface;
using System.Threading;

namespace Data.Repositories.AbstractRepository
{
    public abstract class Repository<T, TEntityId> : IRepository<T,TEntityId>
       where T : Entity<TEntityId>
       where TEntityId : class
    {
        private readonly IMongoCollection<T> _collection;
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;
        private readonly string _databaseName;


        public Repository()
        {
        }

        public Repository(MongoClient mongoClient, string databaseName ,string collectionName = null)
        {
            _mongoClient = mongoClient;
            _collection = _mongoClient.GetDatabase(databaseName).GetCollection<T>(collectionName ?? typeof(T).Name);
        }
        [DebuggerStepThrough]
        public async Task CreateAsync(T entity)
        {
            //CancellationTokenSource source= new CancellationTokenSource(5000);
            var token = CancellationToken.None;
            //T result =
            await _collection.InsertOneAsync(entity, null, token);

            //return result;
        }


        [DebuggerStepThrough]
        public async Task<T> GetByIdAsync(TEntityId id)
        {
            var result = await _collection.FindAsync(r => r.Id == id);
            return result.FirstOrDefault();
        }
    }
}
