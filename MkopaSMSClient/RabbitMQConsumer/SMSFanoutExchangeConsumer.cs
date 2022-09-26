using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkopaSMSClient.RabbitMQConsumer
{
    public class SMSFanoutExchangeConsumer
    {

        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("mkopaInt-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("mkopaInt-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            channel.QueueBind("mkopaInt-queue", "mkopaInt-exchange", string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
              
                Console.WriteLine(message);
            };

            channel.BasicConsume("mkopaInt-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
