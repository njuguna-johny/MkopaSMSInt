using MkopaSMSClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkopaSMSClient
{
    public interface INotificationAdapter
    {
        string SendSMSMessage(SMSPayLoad smsPayload);
    }
}
