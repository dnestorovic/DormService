using MongoDB.Driver;
using Canteen.API.UserMealsInfo.Entities;

namespace Canteen.API.UserMealsInfo.Data
{
    public interface IUserMealsContext
    {
        IMongoCollection<UserMeals> Meals { get; }
    }
}
