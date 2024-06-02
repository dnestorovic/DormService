using Canteen.API.UserMealsInfo.Entities;
using MongoDB.Driver;

namespace Canteen.API.UserMealsInfo.Data
{
    public class UserMealsContextSeed
    {
        public static void SeedData(IMongoCollection<UserMeals> mealsCollection)
        {
            var existMeals = mealsCollection.Find(p => true).Any();
            if (!existMeals)
            {
                mealsCollection.InsertManyAsync(GetPrecongifuredMeals());
            }
        }

        private static IEnumerable<UserMeals> GetPrecongifuredMeals()
        {
            return new List<UserMeals>()
            {
                new UserMeals()
                {
                    Username = "Natalija",
                    Breakfast = 10,
                    Lunch = 20,
                    Dinner = 15
                },
                new UserMeals()
                {
                    Username = "Momcilo",
                    Breakfast = 15,
                    Lunch = 15,
                    Dinner = 15
                },
                new UserMeals()
                {
                    Username = "David",
                    Breakfast = 10,
                    Lunch = 20,
                    Dinner = 10
                }
            };
        }
    }
}
