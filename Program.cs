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

            var serializer = new HLSevenSerializer();
            Console.WriteLine(serializer.Serialize(header, patient));

            Console.WriteLine("----------------------------------------------");

            var falseResponse = @"MSH|^~\&|||MEDFLOW|MEDFLOW|20140108113941+0100||ACK^A01^ACK|0170cd83|P|2.3\n
                            MSA|AR|0170cd83|Combination of patient id and issuer of patient id is not unique, identified by field 'patientId' with fieldValue 'MEDFLOW/76040'!|||205^Duplicate key identifier";
            
            var falseDeserialized = serializer.Deserialize(falseResponse);
            Console.WriteLine(string.Format("Result:\n{0} \n{1}", falseDeserialized.Key, falseDeserialized.Value));

            Console.WriteLine("----------------------------------------------");

            var successResponse = @"MSH|^~\&|||MEDFLOW|MEDFLOW|20140108113857+0100||ACK^A01^ACK|0170cd83|P|2.3\n
                                    MSA|AA|0170cd83||||0^Message accepted";
            
            var successDeserialized = serializer.Deserialize(successResponse);
            Console.WriteLine(string.Format("Result:\n{0} \n{1}", successDeserialized.Key, successDeserialized.Value));
        }
    }
}
