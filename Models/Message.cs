using System;
using System.Collections.Generic;
using HL7Serializer.Models.Common;

namespace HL7Serializer.Models
{
    public class Message
    {
        public MessageHeader Header { get; set; }
        public Patient Patient { get; set; }
        public Patient MergePatient { get; set; }
        public Order Order { get; set; }

        public string Response { get; set; }

        public List<Segment> Segments { get; set; } = new List<Segment>();
        
        /// <summary>
        /// Used for message patient manipulations
        /// </summary>
        /// <param name="header">Header used for message</param>
        /// <param name="patient">Patient</param>
        public Message(MessageHeader header, Patient patient)
        {
            Header = header;
            Patient = patient;
            Segments.Add(BuildHeader());
            Segments.Add(BuildEvn());
            Segments.Add(BuildPatient());
        }

        /// <summary>
        /// Used for merging matients
        /// </summary>
        /// <param name="header">Header used for message</param>
        /// <param name="patient">Original patient</param>
        /// <param name="mergePatient">Patient to be merged</param>
        public Message(MessageHeader header, Patient patient, Patient mergePatient)
        {
            Header = header;
            Patient = patient;
            MergePatient = mergePatient;
            Segments.Add(BuildHeader());
            Segments.Add(BuildEvn());
            Segments.Add(BuildPatient());
            Segments.Add(BuildMergePatient());
        }

        /// <summary>
        /// Used for creating order
        /// </summary>
        /// <param name="header">Header used for message</param>
        /// <param name="patient">Patient</param>
        /// <param name="order">Order details</param>
        public Message(MessageHeader header, Patient patient, Order order)
        {
            Header = header;
            Patient = patient;
            Order = order;
            Segments.Add(BuildHeader());
            Segments.Add(BuildEvn());
            Segments.Add(BuildPatient());
            Segments.Add(BuildOrder());
            Segments.Add(BuildObservationRequest());
        }

        public Message() { }

        /// <summary>
        /// Used for deserialization purposes
        /// </summary>
        /// <param name="response">String representation of response message to be deserialized</param>
        public Message(string response)
        {
            Response = response;
        }
        public string Serialize()
        {
            var result = "";
            foreach (var segment in Segments)
            {
                result += segment.Serialize() + "\n";
            }

            return result;
        }

        public KeyValuePair<bool, string> Deserialize()
        {
            if (Response == null)
            {
                return new KeyValuePair<bool, string>(false, "Error. No response has been set!");
            }

            try
            {
                var response = Response.Split('\n')[1];

                var responseParts = response.Split('|');
                var acknowledgmentCode = responseParts[1];
                var textMessage = responseParts[3];
                var errorCondition = responseParts[6];
                var value = string.Format("acknowledgmentCode:{0}\ntextMessage:{1}\nerrorCondition:{2}", acknowledgmentCode, textMessage, errorCondition);
                return new KeyValuePair<bool, string>(acknowledgmentCode == "AA" ? true : false, value);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, string.Format("Error. {0}!", ex.Message));
            }

        }
        public Segment BuildHeader()
        {
            return new Segment("MSH", new List<Composite>()
            {
                new Composite(Constants.EncodingChars),
                new Composite(Header.SendingApplication),
                new Composite(Header.SendingFacility),
                new Composite(Header.ReceivingApplication),
                new Composite(Header.ReceivingFacility),
                new Composite(Header.SentDateTime),
                Composite.Empty, // Security code
                new Composite(Header.MessageType),
                new Composite(Constants.MessageControlId),
                new Composite(Constants.ProcessingId),
                new Composite(Constants.Version)
            });
        }

        public Segment BuildEvn()
        {
            return new Segment("EVN", new List<Composite>()
            {
                new Composite(Header.MessageType.Split('^')[1])
            });
        }

        public Segment BuildPatient()
        {
            List<SubComposite> identifierList = Patient.GetIdentifierList(Patient);
            List<SubComposite> nameList = Patient.GetNameList(Patient);
            List<SubComposite> addressList = Patient.GetAddressList(Patient);

            var patientSegment = new Segment("PID", new List<Composite>()
            {
                Composite.Empty,
                new Composite(Patient.Id),
                new Composite(identifierList),
                new Composite(Patient.Id),
                new Composite(nameList),
                Composite.Empty,
                new Composite(Patient.BirthDate),
                new Composite(Patient.Gender),
                Composite.Empty,
                Composite.Empty,
                new Composite(addressList),
                Composite.Empty,
                // new Composite(phoneList)
            });

            return patientSegment;
        }

        private Segment BuildMergePatient()
        {
            List<SubComposite> identifierList = Patient.GetIdentifierList(MergePatient);

            var patientSegment = new Segment("MRG", new List<Composite>()
            {
                new Composite(identifierList),
            });

            return patientSegment;
        }

        private Segment BuildOrder()
        {
            List<SubComposite> number = Order.GetNumberList(Order);
            List<SubComposite> timing = Order.GetTimingList(Order);
            List<SubComposite> enterer = Order.GetEntererList(Order);
            List<SubComposite> provider = Order.GetProviderList(Order);
            List<SubComposite> entererLocation = Order.GetEntererLocationList(Order);
            List<SubComposite> phoneNumber = Order.GetPhoneNumberList(Order);
            List<SubComposite> enteringDevice = Order.GetEnteringDeviceList(Order);

            var orderSegment = new Segment("ORC", new List<Composite>()
            {
                new Composite(Order.Code),
                new Composite(number),
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                new Composite(timing),
                Composite.Empty,
                new Composite(Order.TransactionDate),
                new Composite(enterer),
                Composite.Empty,
                new Composite(provider),
                new Composite(entererLocation),
                new Composite(phoneNumber), // Callback phone number
                Composite.Empty, // Order effective date/time
                Composite.Empty, // Order control code reason
                Composite.Empty, // Entering organization
                new Composite(enteringDevice), // Entering device
            });

            return orderSegment;
        }

        private Segment BuildObservationRequest()
        {
            List<SubComposite> orderNumber = ObservationRequest.GetOrderNumberList(Order.ObservationRequest);
            List<SubComposite> universalService = ObservationRequest.GetUniversalServiceList(Order.ObservationRequest);
            List<SubComposite> orderingProvider = ObservationRequest.GetOrderingProviderList(Order.ObservationRequest);
            List<SubComposite> phoneNumber = ObservationRequest.GetPhoneNumberList(Order.ObservationRequest);
            List<SubComposite> quantityAndTiming = ObservationRequest.GetQuantityAndTimingList(Order.ObservationRequest);
            List<SubComposite> tehnician = ObservationRequest.GetTehnicianList(Order.ObservationRequest);

            var observationRequestSegment = new Segment("OBR", new List<Composite>()
            {
                new Composite(Order.ObservationRequest.Id),
                new Composite(orderNumber),
                Composite.Empty,
                new Composite(universalService),
                Composite.Empty,
                new Composite(Order.ObservationRequest.RequestedDate),
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                new Composite(orderingProvider),//16
                new Composite(phoneNumber),//17
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                new Composite(quantityAndTiming),//27
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                Composite.Empty,
                new Composite(tehnician), //34
            });

            return observationRequestSegment;
        }
    }
}