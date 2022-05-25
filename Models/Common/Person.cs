namespace HL7Serializer.Models.Common
{
    public class Person
    {
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string MiddleInitialOrName { get; set; }
        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public string PointOfCare { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
        public string Facility { get; set; }
        public string LocationStatus { get; set; }
        public string PersonalLocationType { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string LocationType { get; set; }
    }
}