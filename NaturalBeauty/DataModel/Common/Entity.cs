namespace Data.Common
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity<TIdentifier>
    {

        protected Entity()
        {
        }


        [BsonRepresentation(BsonType.ObjectId)]
        [Key]
        [BsonElement("id")]
        public TIdentifier Id { get; set; }
        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }
        [BsonElement("updatedOn")]
        public DateTime UpdatedOn { get; set; }
    }
}
