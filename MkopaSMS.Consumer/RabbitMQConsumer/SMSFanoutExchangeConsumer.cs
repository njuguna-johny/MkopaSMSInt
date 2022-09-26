using Microsoft.Extensions.Configuration;
using MkopaSMS.Consumer.Models;
using MkopaSMSInt.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MkopaSMS.Consumer.RabbitMQConsumer
{
    public partial class SMSFanoutExchangeConsumer
    {

        public static void Consume(IModel channel, IWebSmsSender smsSender, IConfiguration config)
        {
            channel.ExchangeDeclare(config.GetValue<string>("amqpexchange"), ExchangeType.Fanout); //Channel configuration can be taken to configurations 
            channel.QueueDeclare(config.GetValue<string>("amqpqueue"), durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(config.GetValue<string>("amqpqueue"), config.GetValue<string>("amqpexchange"), string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //Deserialize the message to SmsPayload and pass to the WebSmsSender
                SmsPayloadFromRB smsPayload = JsonSerializer.Deserialize<SmsPayloadFromRB>(message);
                SmsPayLoad smsPayload1 = JsonSerializer.Deserialize<SmsPayLoad>(message);

                if (smsPayload.Status == "Sent")
                {
                    //Write implementation for update the Db with sent status
                }
                else
                {
                    Console.WriteLine("SMS sent to SMSGateway");
                    Tuple<bool, string> sendfake = smsSender.Send(smsPayload, "https://SMSGateway.com");
                    //Check if sendfake is true fire message sent event
                    smsPayload1.Status = "Sent";
                    Console.WriteLine("SMS sent publishing sent event");
                    Publish(channel, smsPayload1); /// publish message sent event to rabbitMQ
                    Console.WriteLine("Sent event published");
                }


                Console.WriteLine(message);
            };

            channel.BasicConsume(config.GetValue<string>("amqpqueue"), true, consumer);
            Console.WriteLine("Mkopa SMSConsumer started");
            Console.ReadLine();
        }
    


    public static void Publish(IModel channel, SmsPayLoad pLoad)
    {
        var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
        channel.ExchangeDeclare("mkopaInt-exchange", ExchangeType.Fanout, arguments: ttl);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(pLoad));

        var properties = channel.CreateBasicProperties();
        properties.Headers = new Dictionary<string, object> { { "account", "update" } };

        channel.BasicPublish("mkopaInt-exchange", "account.new", properties, body);


    }
    }
}
