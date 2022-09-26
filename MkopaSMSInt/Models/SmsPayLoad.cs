using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkopaSMSInt.Models
{
    public class SmsPayLoad
    {

        public Guid id { get; set; }
        public string toNumber { get; set; }
        public string body { get; set; }
        public string Status { get; set; }
    }
}
