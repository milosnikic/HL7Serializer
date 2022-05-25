using System;
using System.Collections.Generic;

namespace HL7Serializer.Models
{
    public class SubComposite
    {
        public string Value { get; set; }
        public List<SubSubComposite> SubSubComposites { get; set; }
        public static SubComposite Empty = new SubComposite(string.Empty);

        public SubComposite(object value)
        {
            if (value is DateTime)
            {
                Value = ((DateTime)value).ToString("yyyyMMddHHmm");
                return;
            }

            if (value is List<SubSubComposite>)
            {
                SubSubComposites = (List<SubSubComposite>)value;
                Value = string.Join(Constants.Delimiters.SubSubCompositeDelimiter, SubSubComposites);
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