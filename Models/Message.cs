using System.Collections.Generic;

namespace HL7Serializer.Models
{
    public class Message
    {
        public MessageHeader Header { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();

        public Message(MessageHeader header)
        {
            Header = header;
            Segments.Add(BuildHeader());
        }
        public Message()
        {
            Header = new MessageHeader();
            Segments.Add(BuildHeader());
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

        public void AddPatient(Patient patient)
        {
            List<SubComposite> identifierList = Patient.GetIdentifierList(patient);
            List<SubComposite> nameList = Patient.GetNameList(patient);
            List<SubComposite> addressList = Patient.GetAddressList(patient);

            var patientSegment = new Segment("PID", new List<Composite>()
            {
                Composite.Empty,
                new Composite(patient.Id),
                new Composite(identifierList),
                new Composite(patient.Id),
                new Composite(nameList),
                Composite.Empty,
                new Composite(patient.BirthDate),
                new Composite(patient.Gender),
                Composite.Empty,
                Composite.Empty,
                new Composite(addressList),
                Composite.Empty,
            });

            Segments.Add(patientSegment);
        }
    }
}