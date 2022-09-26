using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkopaSMSClient.Models
{
    public class SmsPayLoad
    {

        public Guid MessageId { get; set; }
        public string PhoneNumber { get; set; }
        public string MessabgeBody { get; set; }
        public string Status { get; set; }
    }
}
