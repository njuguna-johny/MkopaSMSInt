using MkopaSMSInt.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MkopaSMSInt.RabbitMQPublisher
{
    static class SMSFanoutExchangePublisher
    {
        public static void Publish(IModel channel, SmsPayLoad pLoad)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("mkopaInt-exchange", ExchangeType.Fanout, arguments: ttl);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pLoad));

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { { "account", "update" } };

            channel.BasicPublish("mkopaInt-exchange", "account.new", properties, body);


        }
    }
}
