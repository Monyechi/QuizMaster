using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        
    }
}
