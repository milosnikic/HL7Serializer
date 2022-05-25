using System;
using System.Collections.Generic;
using HL7Serializer.Models;

namespace HL7Serializer.Common
{
    public class Patient : Person
    {
        public string AssigningAuthority { get; set; }
        public List<int> OtherIds { get; set; } = new List<int>();
        public ShortDate BirthDate { get; set; }
        public char Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public static List<SubComposite> GetAddressList(Patient patient)
        {
            return new List<SubComposite>()
            {
                new SubComposite(patient.Street),
                SubComposite.Empty,
                new SubComposite(patient.City),
                new SubComposite(patient.State),
                new SubComposite(patient.Zip),
                new SubComposite(patient.Country),
                SubComposite.Empty,
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
                new SubComposite(patient.MiddleInitialOrName),
                new SubComposite(patient.Suffix),
                new SubComposite(patient.Prefix),
            };
        }

        public static List<SubComposite> GetIdentifierList(Patient patient)
        {
            return new List<SubComposite>()
            {
                new SubComposite(patient.Id),
                SubComposite.Empty,
                SubComposite.Empty,
                new SubComposite(patient.AssigningAuthority),
                SubComposite.Empty
            };
        }
    }
}