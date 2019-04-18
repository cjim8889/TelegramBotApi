using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TelegramS.Settings;

namespace TelegramS.Context
{
    public class MqContext
    {
        private readonly IModel channel;
        private readonly IConnection connection;
        public MqContext(IOptions<Configs> options)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.Uri = new Uri(options.Value.MqConnectionString);

            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            Channel.QueueDeclare(queue: "test",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

        }

        ~MqContext()
        {
            CloseChannel();
            CloseConnection();
        }

        public IModel Channel => channel;
        
        public void CloseChannel()
        {
            Channel.Close();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
