namespace HL7Serializer.Models.Common
{
    public class Device
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string NameOfCodingSystem { get; set; }
        public string AlternateId { get; set; }
        public string AlternateText { get; set; }
        public string NameOfAlternateCodingSystem { get; set; }
    }
}