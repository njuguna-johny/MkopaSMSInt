using MassTransit;
using MkopaSMSClient.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MkopaSMSClient
{
    public class SmsPayloadConsumer : IConsumer<SmsPayLoad>
    {
        IPublishEndpoint _publishEndpoint;
       private readonly INotificationAdapter _smsNotifications;

        public SmsPayloadConsumer(IPublishEndpoint publishEndpoint, INotificationAdapter smsNotifications)
        {
            _publishEndpoint = publishEndpoint;
            _smsNotifications = smsNotifications;
        }
        public async Task Consume(ConsumeContext<SmsPayLoad> context)
        {
            var apiCallback = "";
            var msg = context.Message;
            //Call the SMS gateway
         

            if (msg.Status=="New")
            {
                var smsPayload = new SMSPayLoad { PhoneNumber = msg.PhoneNumber, SmsText = msg.MessabgeBody.ToString() };
         
                apiCallback = _smsNotifications.SendSMSMessage(smsPayload);//Post to SMS gateway via http client

            }
            else
            {
                //SMS already sent likely update the DB so that next time it's not fetched into the queue
                apiCallback = "Ok";
            }
        
            if (apiCallback == "Ok")
            {
                msg.Status = "Sent";
                //Publish SMS sent event
                await _publishEndpoint.Publish<SmsPayLoad>(msg);

            }
            await Console.Out.WriteLineAsync(msg.MessabgeBody);
        }
    }
}
