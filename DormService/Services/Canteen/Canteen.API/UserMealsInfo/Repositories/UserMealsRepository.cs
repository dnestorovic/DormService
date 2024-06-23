using Canteen.API.UserMealsInfo.Data;
using Canteen.API.UserMealsInfo.Entities;
using MongoDB.Driver;

namespace Canteen.API.UserMealsInfo.Repositories
{
    public class UserMealsRepository : IUserMealsRepository
    {
        private readonly IUserMealsContext _context;

        public UserMealsRepository(IUserMealsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserMeals> CreateNewUserMeals(string username)
        {
            var newUserMeals = new UserMeals(username);
            await _context.Meals.InsertOneAsync(newUserMeals);

            return await _context.Meals.Find(p => p.Username == username).FirstOrDefaultAsync();
        }

        public async Task<UserMeals> GetUserMeals(string username)
        {
            var userMeals = await _context.Meals.Find(p => p.Username == username).FirstOrDefaultAsync();
            if (userMeals == null)
            {
                // If there is no record for given user, create it
                return await CreateNewUserMeals(username);
            }
            return userMeals;
        }

        public async Task<bool> UpdateUserMeals(UserMeals userMeals)
        {
            // Increase number of meals for each meal type
            var oldMeals = await GetUserMeals(userMeals.Username);
            userMeals.Breakfast += oldMeals.Breakfast;
            userMeals.Lunch += oldMeals.Lunch;
            userMeals.Dinner += oldMeals.Dinner;
            userMeals._id = oldMeals._id;

            var updateResult = await _context.Meals.ReplaceOneAsync(p => p.Username == userMeals.Username, userMeals);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
