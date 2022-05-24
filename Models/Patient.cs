using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HL7Serializer.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string AssigningAuthority { get; set; }
        public List<int> OtherIds { get; set; } = new List<int>();
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public DateOfBirth BirthDate { get; set; }
        public char Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public static List<SubComposite> GetAddressList(Patient patient)
        {
            return new List<SubComposite>()
            {
                new SubComposite(patient.Street),
                SubComposite.Empty,
                new SubComposite(patient.City),
                SubComposite.Empty,
                SubComposite.Empty,
            };
        }

        public static List<SubComposite> GetNameList(Patient patient)
        {
            return new List<SubComposite>()
            {
                new SubComposite(patient.Lastname),
                new SubComposite(patient.Firstname),
                SubComposite.Empty,
                SubComposite.Empty,
                SubComposite.Empty,
            };
        }

        public static List<SubComposite> GetIdentifierList(Patient patient)
        {
            return new List<SubComposite>()
            {
                new SubComposite(patient.Id),
                SubComposite.Empty,
                SubComposite.Empty,
                new SubComposite(patient.AssigningAuthority)
            };
        }

        public class DateOfBirth
        {
            public DateTime? Date { get; set; }

            public DateOfBirth() {}

            public DateOfBirth(DateTime? date)
            {
                Date = date;
            }

            public DateOfBirth(DateTime date)
            {
                Date = date;
            }

            public override string ToString()
            {
                return Date != null ? ((DateTime)Date).ToString("yyyyMMdd") : "";
            }
        }
    }
}