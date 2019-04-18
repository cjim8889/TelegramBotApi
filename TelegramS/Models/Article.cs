using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TelegramS.Models
{
    public class Article
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Url")]
        public string Url { get; set; }

        [BsonElement("PublishedDate")]
        public DateTime PublishedDate { get; set; }

        public Article()
        {
            Id = ObjectId.GenerateNewId().ToString();
            PublishedDate = DateTime.Now;
        }

    }
}
