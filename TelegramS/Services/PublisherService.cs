using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TelegramS.Context;
using Newtonsoft.Json;
using TelegramS.Models;

namespace TelegramS.Services
{
    public class PublisherService
    {
        private readonly IMongoCollection<Publisher> _publishers;
        private readonly MqService mqService;

        public PublisherService(TelegramContext context, MqService mqService)
        {
            _publishers = context.Publishers;
            this.mqService = mqService;
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _publishers.Find(publisher => true).ToListAsync();
        }

        public async Task<Publisher> GetPublisher(string id)
        {
            return await _publishers.Find(publisher => publisher.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertPublisher(Publisher publisher)
        {
            await _publishers.InsertOneAsync(publisher);
        }

        public async Task<UpdateResult> InsertArticle(String publisherId, Article article)
        {

            var filter = Builders<Publisher>.Filter.Eq("Id", publisherId);
            var update = Builders<Publisher>.Update.Push<Article>("Articles", article);

            return await _publishers.UpdateOneAsync(filter, update);
        }

        public async Task PushArticle(String articleId, String publisherId)
        {
            var publisher = await GetPublisher(publisherId);
            var article = publisher.Articles.FirstOrDefault<Article>(a => a.Id == articleId);

            if (article != null)
            {

                var message = JsonConvert.SerializeObject(new { article });
                mqService.PublishMessage(message);

            }
        }

    }
}
