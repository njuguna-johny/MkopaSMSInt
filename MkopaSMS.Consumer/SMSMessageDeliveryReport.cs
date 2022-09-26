using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MkopaSMSInt.Models;
using MkopaSMS.Consumer.Models;

namespace MkopaSMS.Consumer
{
   public class SMSMessageDeliveryReport
    {
        private SmsPayload smsPayload;
        private bool v;

        [Key]
        public Guid Id { get; private set; }

        public string Content { get; private set; }

        [Required]
        [StringLength(30)]
        public string FromNumber { get; private set; }

        public bool IsSuccessfulDelivery { get; private set; }

        public virtual SmsPayLoad Message { get; private set; }

        public DateTime DateCreated { get; private set; }

        [Required]
        [StringLength(100)]
        public string Sender { get; private set; }

        protected SMSMessageDeliveryReport(Guid id)
        {
        }

        internal SMSMessageDeliveryReport(Guid id,
            SmsPayLoad message, string fromNumber,
            string content, bool isSuccessfulDelivery, string sender)
        {
            this.Id = id;
            FromNumber = fromNumber;
            Content = content;
            IsSuccessfulDelivery = isSuccessfulDelivery;
            Message = message;
            this.Sender = sender;
            DateCreated = DateTime.UtcNow;
        }

        public SMSMessageDeliveryReport(Guid id, SmsPayload smsPayload, string fromNumber, string content, bool v, string sender) : this(id)
        {
            this.smsPayload = smsPayload;
            FromNumber = fromNumber;
            Content = content;
            this.v = v;
            Sender = sender;
        }
    }
}
