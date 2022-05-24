using System;
using System.Collections.Generic;

namespace HL7Serializer.Models
{
    public class Message
    {
        public MessageHeader Header { get; set; }
        public Patient Patient { get; set; }
        public string Response { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();
        public Message(MessageHeader header, Patient patient)
        {
            Header = header;
            Patient = patient;
            Segments.Add(BuildHeader());
            Segments.Add(BuildPatient());
        }
        public Message() { }
        
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

            var response = Response.Split('\n')[1];

            var responseParts = response.Split('|');
            var acknowledgmentCode = responseParts[1];
            var textMessage = responseParts[3];
            var errorCondition = responseParts[6];
            var value = string.Format("acknowledgmentCode:{0}\ntextMessage:{1}\nerrorCondition:{2}",acknowledgmentCode, textMessage, errorCondition);
            return new KeyValuePair<bool, string>(acknowledgmentCode == "AA" ? true : false, value);
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
            });

            return patientSegment;
        }
    }
}