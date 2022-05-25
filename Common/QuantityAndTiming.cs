using System;

namespace HL7Serializer.Common
{
    public class QuantityAndTiming
    {
        public string Quantity { get; set; }
        public string Interval { get; set; }
        public string Duration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Priority { get; set; }
    }
}