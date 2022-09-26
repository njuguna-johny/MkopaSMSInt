using MkopaSMS.Consumer.Models;
using MkopaSMS.Consumer.RabbitMQConsumer;
using System;

namespace MkopaSMS.Consumer
{
    public interface IWebSmsSender
    {
        Tuple<bool, string> Send(SmsPayloadFromRB message, string rawUrlString);
    }
}