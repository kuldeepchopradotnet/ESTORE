namespace ESTORE.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AllowAccessAttribute: Attribute
    {
        public string? Access = null;
        public AllowAccessAttribute(string access) {
            Access = access;
        }


    }
}
