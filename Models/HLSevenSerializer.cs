using System.Collections.Generic;
using HL7Serializer.Models.Common;

namespace HL7Serializer.Models
{
    public class HLSevenSerializer
    {
        public string Serialize(MessageHeader header, Patient patient)
        {
            return new Message(header, patient).Serialize();
        }

        public string Serialize(MessageHeader header, Patient patient, Patient mergePatient)
        {
            return new Message(header, patient, mergePatient).Serialize();
        }

        public string Serialize(MessageHeader header, Patient patient, Order order)
        {
            return new Message(header, patient, order).Serialize();
        }

        public KeyValuePair<bool, string> Deserialize(string response)
        {
            return new Message(response).Deserialize();
        }
    }
}