using System.Collections.Generic;

namespace HL7Serializer.Models
{
    public class HLSevenSerializer
    {
        public string Serialize(MessageHeader header, Patient patient)
        {
            return new Message(header, patient).Serialize();
        }

        public KeyValuePair<bool, string> Deserialize(string response)
        {
            return new Message(response).Deserialize();
        }
    }
}