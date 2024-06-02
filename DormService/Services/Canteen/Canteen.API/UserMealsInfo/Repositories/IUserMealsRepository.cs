using Canteen.API.UserMealsInfo.Entities;

namespace Canteen.API.UserMealsInfo.Repositories
{
    public interface IUserMealsRepository
    {
        Task<UserMeals> GetUserMeals(string username);
        Task<bool> UpdateUserMeals(UserMeals userMeals);
    }
}
