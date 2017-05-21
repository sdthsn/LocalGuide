namespace Data.Common
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity<TEntityId>
    {

        protected Entity()
        {
        }

        [BsonRepresentation(BsonType.ObjectId)]
        [Key]
        [BsonElement("id")]
        public virtual TEntityId Id { get; set; }
        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }
        [BsonElement("updatedOn")]
        public DateTime UpdatedOn { get; set; }
    }
}
