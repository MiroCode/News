using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NewsApp.Infrastructure.Models
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string IsActive { get; set; }
    }
}