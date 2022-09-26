using System;

namespace MkopaSMS.Consumer.RabbitMQConsumer
{
    public class SmsPayloadFromRB
    {

        public Guid id { get; set; }
        public string toNumber { get; set; }
        public string body { get; set; }
        public string Status { get; set; }
    }
}
