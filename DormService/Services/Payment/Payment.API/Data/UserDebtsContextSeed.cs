using MongoDB.Driver;
using Payment.API.Entities;

namespace Payment.API.Data
{
    public class UserDebtsContextSeed
    {

        public static readonly Dictionary<string, float> momciloDebts = new Dictionary<string, float>()
        {
            {DebtType.CREDIT, 0 },
            {DebtType.INTERNET, 100 },
            {DebtType.CLEANING, 1000 },
            {DebtType.AIR_CONDITIONING, 50 },
            {DebtType.PHONE, 450 },
            {DebtType.RENT, 5000 }
        };

        public static readonly StudentDebts natalija = new StudentDebts("Natalija");
        public static readonly StudentDebts teodora = new StudentDebts("Teodora");
        public static readonly StudentDebts david = new StudentDebts("David");
        public static readonly StudentDebts momcilo = new StudentDebts("Momcilo", momciloDebts);



        public static void SeedData(IMongoCollection<StudentDebts> debtsCollection)
        {
            var existsDebts = debtsCollection.Find(d => true).Any();

            if (!existsDebts)
            {
                debtsCollection.InsertManyAsync(GetPreconfigureDebts());
            }

        }

        private static IEnumerable<StudentDebts> GetPreconfigureDebts()
        {
            List<StudentDebts> students = new List<StudentDebts>();
            students = [natalija, teodora, david, momcilo];
            return students;
        }
    }
}
