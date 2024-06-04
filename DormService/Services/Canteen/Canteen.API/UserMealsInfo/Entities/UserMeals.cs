using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Canteen.API.UserMealsInfo.Entities
{
    public class UserMeals
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Username { get; set; }
        public int Breakfast { get; set; }
        public int Lunch { get; set; }
        public int Dinner { get; set; }

        public UserMeals() { }  
        public UserMeals(string username)
        {
            Username = username;
        }
    }
}
