using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VismaTechnicalTask.Models
{
    public class AppRec
    {
        [Key]
        public string Id { get; set; }

        public DateTime GenDate { get; set; }

        public string MsgType { get; set; }

        public string MIGversion { get; set; }

        [ForeignKey("Sender")]
        public string SenderID { get; set; }
        public virtual Sender Sender { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverID { get; set; }
        public virtual Receiver Receiver { get; set; }

        public string Status { get; set; }
    }
}
