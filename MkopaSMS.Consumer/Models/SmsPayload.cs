using MkopaSMS.Consumer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkopaSMS.Consumer.Models
{
  public  class SmsPayload
    {
        public Guid MessageId { get; set; }
        public string PhoneNumber { get; set; }
        public string SmsText { get; set; }
        public DateTime DateCreated { get; set; }
        public int DeliveryAttempts { get; set; }
        public SmsMessageStatus Status { get; set; }
        [ConcurrencyCheck]
        [Timestamp]
        public virtual byte[] TimeStamp { get; protected set; }
 
        public SmsPayload(Guid id, string body, string toNumber)
        {
            MessageId = id;
            SmsText = body;
            PhoneNumber = toNumber;
            DateCreated = DateTime.UtcNow;
            DeliveryAttempts = 0;
            Status = SmsMessageStatus.Queued;
        }

        public virtual SMSMessageDeliveryReport MarkAsSent(Guid id, string fromNumber,string content, string sender)
    {

        if (this.Status == SmsMessageStatus.Sent)
        {
            //duplicate throw!
        }
        this.Status = SmsMessageStatus.Sent;
        this.DeliveryAttempts++;
        var obj = new SMSMessageDeliveryReport(id, this, fromNumber, content, true, sender);
        return obj;
    }

    public virtual SMSMessageDeliveryReport AddFailedDelivery(Guid id,
        string fromNumber, string content, string sender)
    {
        if (this.Status != SmsMessageStatus.Queued)
            return null;// propblem occured
        this.DeliveryAttempts++;
        var obj = new SMSMessageDeliveryReport(id, this, fromNumber, content, false, sender);
        return obj;
    }
 }

}
