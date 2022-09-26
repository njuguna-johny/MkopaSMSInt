using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MkopaSMS.Consumer.RabbitMQConsumer;
using RabbitMQ.Client;
using System;

namespace MkopaSMS.Consumer
{
    public class MkopaSMSService : IMkopaSMSService
    {
        private readonly IWebSmsSender _smsSender;
        private readonly ILogger<MkopaSMSService> _log;
        private readonly IConfiguration _config;

        public MkopaSMSService(ILogger<MkopaSMSService> log, IConfiguration config, IWebSmsSender smsSender)
        {
            _log = log;
            _config = config;
            _smsSender = smsSender;
        }


        public void Run()
        {
            //for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            //{

            //    _log.LogInformation("Run number {runNumber}", i);
            //}

            var factory = new ConnectionFactory
            {
                Uri = new Uri(_config.GetValue<string>("amqp")) // RabbitMQ configs can be taken to configuartion 
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            SMSFanoutExchangeConsumer.Consume(channel, _smsSender,_config);
        }
    }
}
