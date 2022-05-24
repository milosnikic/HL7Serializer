using System;

namespace HL7Serializer.Models
{
    public class MessageHeader
    {
        public string SendingApplication { get; set; }
        public string SendingFacility { get; set; }
        public string ReceivingApplication { get; set; }
        public string ReceivingFacility { get; set; }
        public DateTime? SentDateTime { get; set; }
        public string MessageType { get; set; }
    }
}