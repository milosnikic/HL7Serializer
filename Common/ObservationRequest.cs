using System.Collections.Generic;
using HL7Serializer.Models;

namespace HL7Serializer.Common
{
    public class ObservationRequest
    {
        public string Id { get; set; }
        public PlacerOrderNumber OrderNumber { get; set; }
        public Device UniversalServiceId { get; set; }
        public ShortDate RequestedDate { get; set; }
        public Person OrderingProvider { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public QuantityAndTiming QuantityAndTiming { get; set; }
        public Person Tehnician { get; set; }

        public static List<SubComposite> GetOrderNumberList(ObservationRequest observationRequest)
        {
            return new List<SubComposite>()
            {
                new SubComposite(observationRequest.OrderNumber.Id),
                new SubComposite(observationRequest.OrderNumber.Application),
            };
        }

        public static List<SubComposite> GetUniversalServiceList(ObservationRequest observationRequest)
        {
            return new List<SubComposite>()
            {
                new SubComposite(observationRequest.UniversalServiceId.Id),
                new SubComposite(observationRequest.UniversalServiceId.Text),
                new SubComposite(observationRequest.UniversalServiceId.NameOfCodingSystem),
                new SubComposite(observationRequest.UniversalServiceId.AlternateId),
                new SubComposite(observationRequest.UniversalServiceId.AlternateText),
            };
        }

        public static List<SubComposite> GetOrderingProviderList(ObservationRequest observationRequest)
        {
            return new List<SubComposite>()
            {
                new SubComposite(observationRequest.OrderingProvider.Id),
                new SubComposite(observationRequest.OrderingProvider.Lastname),
                new SubComposite(observationRequest.OrderingProvider.Firstname),
                new SubComposite(observationRequest.OrderingProvider.MiddleInitialOrName),
                new SubComposite(observationRequest.OrderingProvider.Suffix),
                new SubComposite(observationRequest.OrderingProvider.Prefix),
            };
        }

        public static List<SubComposite> GetPhoneNumberList(ObservationRequest observationRequest)
        {
            return new List<SubComposite>()
            {
                new SubComposite(observationRequest.PhoneNumber.PrimaryNumber),
                new SubComposite(observationRequest.PhoneNumber.TelecommunicationUseCode),
                new SubComposite(observationRequest.PhoneNumber.TelecommunicationEquipmentType),
                new SubComposite(observationRequest.PhoneNumber.Email),
                new SubComposite(observationRequest.PhoneNumber.CountryCode),
                new SubComposite(observationRequest.PhoneNumber.City),
                new SubComposite(observationRequest.PhoneNumber.SecondaryNumber),
            };
        }

        public static List<SubComposite> GetQuantityAndTimingList(ObservationRequest observationRequest)
        {
            return new List<SubComposite>()
            {
                new SubComposite(observationRequest.QuantityAndTiming.Quantity),
                new SubComposite(observationRequest.QuantityAndTiming.Interval),
                new SubComposite(observationRequest.QuantityAndTiming.Duration),
                new SubComposite(observationRequest.QuantityAndTiming.StartDate),
                new SubComposite(observationRequest.QuantityAndTiming.EndDate),
                new SubComposite(observationRequest.QuantityAndTiming.Priority),
            };
        }

        public static List<SubComposite> GetTehnicianList(ObservationRequest observationRequest)
        {
            var tehnicianSubSubCompositeList = new List<SubSubComposite>()
            {
                new SubSubComposite(observationRequest.Tehnician.Id),
                new SubSubComposite(observationRequest.Tehnician.Lastname),
                new SubSubComposite(observationRequest.Tehnician.Firstname),
                // new SubSubComposite(observationRequest.Tehnician.MiddleInitialOrName),
                // new SubSubComposite(observationRequest.Tehnician.Suffix),
                // new SubSubComposite(observationRequest.Tehnician.Prefix),
            };
            
            return new List<SubComposite>()
            {
                new SubComposite(tehnicianSubSubCompositeList),
            };
        }
    }

    public class PlacerOrderNumber
    {
        public string Id { get; set; }
        public string Application { get; set; }
    }
}