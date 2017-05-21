using Data.Interfaces;
using Data.Repositories.AbstractRepository;
using DataModel.Contract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SpotsRepository :Repository<Spot,string>,ISpotsRepository
    {
        public SpotsRepository(MongoClient mongoClient,string databaseName, string collectionName = null): base (mongoClient,databaseName, collectionName) { }
    }
}
