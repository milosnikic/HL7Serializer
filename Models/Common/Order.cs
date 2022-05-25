using System;
using System.Collections.Generic;

namespace HL7Serializer.Models.Common
{
    public class Order
    {
        public string Code { get; set; }
        public string Number { get; set; }
        public string NamespaceId { get; set; }
        public ShortDate StartDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public Person Enterer { get; set; }
        public Person Provider { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Device EnteringDevice { get; set; }
        public ObservationRequest ObservationRequest { get; set; }

        public static List<SubComposite> GetNumberList(Order order)
        {
            return new List<SubComposite>()
            {
                new SubComposite(order.Number),
                new SubComposite(order.NamespaceId),
            };
        }

        public static List<SubComposite> GetTimingList(Order order)
        {
            return new List<SubComposite>()
            {
                SubComposite.Empty,
                SubComposite.Empty,
                SubComposite.Empty,
                new SubComposite(order.StartDate),
                SubComposite.Empty,
                SubComposite.Empty,
            };
        }

        public static List<SubComposite> GetEntererList(Order order)
        {
            return new List<SubComposite>()
            {
                new SubComposite(order.Enterer.Id),
                new SubComposite(order.Enterer.Lastname),
                new SubComposite(order.Enterer.Firstname),
                new SubComposite(order.Enterer.MiddleInitialOrName),
                new SubComposite(order.Enterer.Suffix),
                new SubComposite(order.Enterer.Prefix),
            };
        }

        public static List<SubComposite> GetProviderList(Order order)
        {
            return new List<SubComposite>()
            {
                new SubComposite(order.Provider.Id),
                new SubComposite(order.Provider.Lastname),
                new SubComposite(order.Provider.Firstname),
                new SubComposite(order.Provider.MiddleInitialOrName),
                new SubComposite(order.Provider.Suffix),
                new SubComposite(order.Provider.Prefix),
            };
        }

        public static List<SubComposite> GetEntererLocationList(Order order)
        {
            return new List<SubComposite>()
            {
                new SubComposite(order.Enterer.Location.PointOfCare),
                new SubComposite(order.Enterer.Location.Room),
                new SubComposite(order.Enterer.Location.Bed),
                new SubComposite(order.Enterer.Location.Facility),
                new SubComposite(order.Enterer.Location.LocationStatus),
                new SubComposite(order.Enterer.Location.PersonalLocationType),
                new SubComposite(order.Enterer.Location.Building),
                new SubComposite(order.Enterer.Location.Floor),
                new SubComposite(order.Enterer.Location.LocationType),
            };
        }

        public static List<SubComposite> GetPhoneNumberList(Order order)
        {
            return new List<SubComposite>()
            {
                new SubComposite(order.PhoneNumber.PrimaryNumber),
                new SubComposite(order.PhoneNumber.TelecommunicationUseCode),
                new SubComposite(order.PhoneNumber.TelecommunicationEquipmentType),
                new SubComposite(order.PhoneNumber.Email),
                new SubComposite(order.PhoneNumber.CountryCode),
                new SubComposite(order.PhoneNumber.City),
                new SubComposite(order.PhoneNumber.SecondaryNumber),
            };
        }

        public static List<SubComposite> GetEnteringDeviceList(Order order)
        {
            return new List<SubComposite>()
            {
                new SubComposite(order.EnteringDevice.Id),
                new SubComposite(order.EnteringDevice.Text),
                new SubComposite(order.EnteringDevice.NameOfCodingSystem),
                new SubComposite(order.EnteringDevice.AlternateId),
                new SubComposite(order.EnteringDevice.AlternateText),
            };
        }
    }
}