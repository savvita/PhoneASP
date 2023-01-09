namespace Phone.Web.Exceptions
{
    public class PhoneNotFoundException : Exception
    {
        public int PhoneId { get; }
        public PhoneNotFoundException(int phoneId) : base($"Phone with Id {phoneId} not founds")
        {
            PhoneId = phoneId;
        }
    }
}
