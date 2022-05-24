namespace HL7Serializer.Models
{
    public static class Constants
    {
        public const string EncodingChars = "^~\\&";
        public const string Version = "2.3";
        public const string MessageControlId = "0170cd83";
        public const char ProcessingId = 'P';
        public static class Delimiters
        {
            public const char SegmentDelimiter = '|';
            public const char SubCompositeDelimiter = '^';
        }
    }
}