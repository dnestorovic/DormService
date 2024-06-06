using MongoDB.Driver;
using Payment.Common.Entities;

namespace Payment.Common.Data
{
    public class StudentDebtsContextSeed
    {
        private static decimal credit = 1000;
        private static decimal internet = 1000;
        private static decimal cleaning = 1500;
        private static decimal airConditioning = 500;
        private static decimal phone = 2000;
        private static decimal rent = 5000;


        private static readonly StudentDebts natalija = new StudentDebts("Natalija", credit, rent, internet, airConditioning, phone, cleaning);
        private static readonly StudentDebts teodora = new StudentDebts("Teodora", credit, rent, internet, airConditioning, phone, cleaning);
        private static readonly StudentDebts david = new StudentDebts("David", credit, rent, internet, airConditioning, phone, cleaning);
        private static readonly StudentDebts momcilo = new StudentDebts("Momcilo", credit, rent, internet, airConditioning, phone, cleaning);

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
            return [natalija, teodora, david, momcilo];
        }
    }
}
