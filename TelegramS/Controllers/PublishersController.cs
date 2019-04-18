using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TelegramS.Models;
using TelegramS.Services;

namespace TelegramS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {

        private readonly PublisherService publisherService;
        private readonly MqService mqService;
        public PublishersController(PublisherService publisherService, MqService mqService)
        {
            this.publisherService = publisherService;
            this.mqService = mqService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Publisher>>> GetPublishers()
        {
            return await publisherService.GetPublishers();
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher(Publisher publisher)
        {
            await publisherService.InsertPublisher(publisher);
            return publisher;
        }

        

        [HttpPost("Article")]
        public async Task<ActionResult<UpdateResult>> InsertArticle(string publisherId, Article article)
        {
            return await publisherService.InsertArticle(publisherId, article);
        }

        [HttpGet("Article/{articleId}/Push")]
        public async Task<ActionResult> PushArticle(string articleId, string publisherId)
        {
            await publisherService.PushArticle(articleId, publisherId);
            return StatusCode(201);
        }


    }
}