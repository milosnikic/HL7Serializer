using System;

namespace HL7Serializer.Models.Common
{
    public class ShortDate
        {
            public DateTime? Date { get; set; }

            public ShortDate() {}

            public ShortDate(DateTime? date)
            {
                Date = date;
            }

            public ShortDate(DateTime date)
            {
                Date = date;
            }

            public override string ToString()
            {
                return Date != null ? ((DateTime)Date).ToString("yyyyMMdd") : "";
            }
        }
}