using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkopaSMSClient.Models
{
    public class SMSPayLoad
    {
        public string PhoneNumber { get; set; }
        public string SmsText { get; set; }
    }
}
