using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Laundry.API.Entities;

public class WashingMachine
{

    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public string date { get; set; }
    public TimeSlot time { get; set; }
    public bool reserved { get; set; } 
}