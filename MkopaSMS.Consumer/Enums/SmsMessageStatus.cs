using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkopaSMS.Consumer.Enums
{
    public enum SmsMessageStatus
    {

        Queued = 1,

        Sent = 2,

        Cancelled = 3,

        Failed = 4
    }
}
