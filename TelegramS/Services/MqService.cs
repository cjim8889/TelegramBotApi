using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramS.Context;

namespace TelegramS.Services
{
    public class MqService
    {

        private readonly MqContext context;
        public MqService(MqContext context)
        {
            this.context = context;
        }

        public void PublishMessage(String message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            context.Channel.BasicPublish(exchange: "",
                                 routingKey: "test",
                                 basicProperties: null,
                                 body: body);


            Console.WriteLine($"Message Sent :{message}");
        }

    }
}
