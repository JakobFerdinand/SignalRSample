using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheServer.Models
{
    public class MongoRunner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string RunningTime { get; set; }
    }
}
