using System.Collections.Generic;

namespace HL7Serializer.Models
{
    public class Segment 
    {
        public string Name { get; set; }
        public List<Composite> Composites { get; set; } = new List<Composite>();

        public Segment(string name, List<Composite> composites)
        {
            Name = name;
            Composites = composites;
        }

        public string Serialize()
        {
            var compositesJoined = string.Join(Constants.Delimiters.SegmentDelimiter, Composites);
            return string.Format("{0}|{1}|", Name, compositesJoined);
        }
    }
}