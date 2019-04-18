using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TelegramS.Models;
using TelegramS.Settings;

namespace TelegramS.Context
{
    public class TelegramContext
    {

        private readonly IMongoDatabase database;

        public TelegramContext(IOptions<Configs> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            database = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Publisher> Publishers => database.GetCollection<Publisher>("Publishers");
    }
}
