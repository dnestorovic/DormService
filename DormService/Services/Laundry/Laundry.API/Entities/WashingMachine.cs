using MongoDB.Bson;

namespace Laundry.API.Entities;

public class WashingMachine
{

    public string _id { get; set; }
    public string configurationId { get; set; }
    public string? Time { get; set; }
    public string? Date { get; set; }
    public bool Reserved { get; set; } 

    public WashingMachine(string id, string date, string time) 
    {
        _id = ObjectId.GenerateNewId().ToString();
        configurationId = id;
        Date = date;
        Time = time;
        Reserved = false;
    }
}