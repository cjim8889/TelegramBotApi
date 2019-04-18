using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramS.Models
{
    public class Publisher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("DisplayName")]
        public string DisplayName { get; set; }

        [BsonElement("Articles")]
        public List<Article> Articles { get; set; }

    }
}
