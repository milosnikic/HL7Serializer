using System;
using HL7Serializer.Models;
using static HL7Serializer.Models.Patient;

namespace HL7Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var header = new MessageHeader()
            {
                SendingApplication = "MEDFLOW",
                SendingFacility = "MEDFLOW",
                ReceivingFacility = "MEDFLOWDCM",
                ReceivingApplication = "MEDFLOW",
                MessageType = "ADT^A01",
                SentDateTime = DateTime.Now
            };

            var patient = new Patient()
            {
                Id = 76040,
                AssigningAuthority = "MEDFLOW",
                BirthDate = new DateOfBirth(new DateTime(1960, 5, 19)),
                City = "MALIBU BEACH",
                Firstname = "Daniel",
                Lastname = "Craig",
                Gender = 'M',
                Street = "6548 SECRET WAY",
            };

            var hl7Message = new Message(header);
            hl7Message.AddPatient(patient);
            Console.WriteLine(hl7Message.Serialize());
        }
    }
}
