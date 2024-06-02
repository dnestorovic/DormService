using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Laundry.API.Entities;

public class WashingMachine
{

    public string _id { get; set; }
    public string? Time { get; set; }
    public string? Date { get; set; }
    public bool Reserved { get; set; } 

    public WashingMachine(string date, string time) 
    {
        _id = ObjectId.GenerateNewId().ToString();
        Date = date;
        Time = time;
        Reserved = false;
    }
}