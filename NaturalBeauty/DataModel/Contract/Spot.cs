using Data.Common;
using MongoDB.Bson;
using System;
using System.IO;

namespace DataModel.Contract
{
    public class Spot:Entity<string>
    {
        public override string Id { get; set; } = new BsonObjectId(new ObjectId(Guid.NewGuid().ToString().Replace("-", ""))).ToString();
        public string Name { get; set; }
        public string ShortSummery { get; set; }
        public string Description { get; set; } 
        public ObjectId Imageinfo { get; set; }

    }

    //public class ImageData
    //{
    //    public int ContentLength { get; set; }
    //    public string ContentType { get; set; }
    //    public string FileName { get; set; }
    //    public Stream InputStream { get; set; }
    //}
}
