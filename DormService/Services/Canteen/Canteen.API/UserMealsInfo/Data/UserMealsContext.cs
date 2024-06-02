using Canteen.API.UserMealsInfo.Entities;
using MongoDB.Driver;

namespace Canteen.API.UserMealsInfo.Data
{
    public class UserMealsContext : IUserMealsContext
    {
        public UserMealsContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
            var database = client.GetDatabase("MealsDB");

            Meals = database.GetCollection<UserMeals>("Meals");
            UserMealsContextSeed.SeedData(Meals);
        }

        public IMongoCollection<UserMeals> Meals { get; }
    }
}
