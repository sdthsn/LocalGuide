using Data.Common;
using System;
using System.IO;

namespace DataModel.Contract
{
    public class Spot:Entity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ShortSummery { get; set; }
        public string Description { get; set; } 
        public ImageData Image { get; set; }

    }

    public class ImageData
    {
        public int ContentLength { get; }
        public string ContentType { get; }
        public string FileName { get; }
        public Stream InputStream { get; }
    }
}
