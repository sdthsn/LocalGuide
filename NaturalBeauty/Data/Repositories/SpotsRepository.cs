using Data.Interfaces;
using Data.Repositories.AbstractRepository;
using DataModel.Contract;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SpotsRepository :Repository<Spot,string>,ISpotsRepository
    {
        private readonly IMongoDatabase _dB;
        private readonly  IGridFSBucket _bucket;
        public SpotsRepository(MongoClient mongoClient,string databaseName, string collectionName = null): 
            base (mongoClient,databaseName, collectionName)
        {
            _dB = mongoClient.GetDatabase(databaseName);
            _bucket = new GridFSBucket(_dB, new GridFSBucketOptions { BucketName = "images",ChunkSizeBytes = 1048576 });// 1048576= 1MB
        }

        public async Task <IEnumerable<Spot>> GetAsync()
        {
            var result = await _collection.FindAsync(r => true);
            return await result.ToListAsync();
        }
       public async Task<ObjectId> FileUploadAsync(string fileName, Stream source)
        {
            var id = await _bucket.UploadFromStreamAsync(fileName, source);
            return id;
        }
        public async Task<byte[]> FileDownloadAsync(ObjectId fileId)
        {
            var bytes = await _bucket.DownloadAsBytesAsync(fileId);
            return bytes;
        }


    }
}
        // Todo: If we want to provide options 
        //var gridFsOption = new GridFSBucketOptions
        //{
        //    BucketName = "images",
        //    ChunkSizeBytes = 1048576, // 1MB
        //    //WriteConcern = WriteConcern.WMajority,
        //    //ReadPreference = ReadPreference.Secondary
        //};
