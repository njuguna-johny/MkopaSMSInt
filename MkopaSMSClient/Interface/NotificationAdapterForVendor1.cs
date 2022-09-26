using MkopaSMSClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkopaSMSClient.Interface
{
    public class NotificationAdapterForVendor1 : INotificationAdapter
    {

        //sms gateway vendor specific configuration
        //Note the Send message can be implement differently for each vendor individual vendir
        public NotificationAdapterForVendor1()
        {
            
        }
        public string SendSMSMessage(SMSPayLoad smsPayload)
        {
     
            try
            {

                //Send payload to vendor 
                return "Ok";
            }
            catch (Exception ex)
            {
                return "Fail";
            }

        }
    }
}
