namespace Payment.API.Entities
{
    public class DebtType
    {
        public static readonly string CREDIT = "Credit";
        public static readonly string RENT = "Rent";
        public static readonly string INTERNET = "Internet";
        public static readonly string AIR_CONDITIONING = "AirConditioning";
        public static readonly string PHONE = "Phone";
        public static readonly string CLEANING = "Cleaning";
        public static readonly List<string> types = [CREDIT, RENT, INTERNET, AIR_CONDITIONING, PHONE, CLEANING];
    }
}
