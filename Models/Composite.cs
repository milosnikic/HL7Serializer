using System;
using System.Collections.Generic;

namespace HL7Serializer.Models
{
    public class Composite
    {
        public string Value { get; set; }
        public List<SubComposite> SubComposites { get; set; }
        public static Composite Empty = new Composite(string.Empty);

        public Composite(object value)
        {
            if (value is DateTime)
            {
                Value = ((DateTime)value).ToString("yyyyMMddHHmm");
                return;
            }

            if (value is List<SubComposite>)
            {
                SubComposites = (List<SubComposite>) value;
                Value = string.Join(Constants.Delimiters.SubCompositeDelimiter, SubComposites);
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