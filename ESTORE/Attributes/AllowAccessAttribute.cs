namespace ESTORE.Attributes
{
    public class AllowAccessAttribute: Attribute
    {
        public string? Access = null;
        public AllowAccessAttribute(string access) {
            Access = access;
        }
    }
}
