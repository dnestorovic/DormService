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

        public async Task<UserMeals> GetUserMeals(string username)
        {
            return await _context.Meals.Find(p => p.Username == username).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUserMeals(UserMeals userMeals)
        {
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
