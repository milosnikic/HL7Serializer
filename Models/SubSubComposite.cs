using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HL7Serializer.Models
{
    public class SubSubComposite
    {
        public string Value { get; set; }
        public SubSubComposite(object value)
        {
            if (value is DateTime)
            {
                Value = ((DateTime)value).ToString("yyyyMMddHHmm");
                return;
            }

            if (value == null)
            {
                Value = string.Empty;
                return;
            }
            
            Value = value.ToString();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}