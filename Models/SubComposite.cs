namespace HL7Serializer.Models
{
    public class SubComposite
    {
        public string Value { get; set; }
        public static SubComposite Empty = new SubComposite(string.Empty);

        public SubComposite(object value)
        {
            Value = value.ToString();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}