using MongoDB.Bson;

namespace Laundry.API.Entities;

public class WashingMachine
{

    public string _id { get; set; }
    public string ConfigurationId { get; set; }
    public string? Time { get; set; }
    public string? Date { get; set; }
    public bool Reserved { get; set; } 
    public int SpinRate { get; set; }
    public int WashingTemperature { get; set; }
    

    public WashingMachine(string id, string date, string time) 
    {
        _id = ObjectId.GenerateNewId().ToString();
        ConfigurationId = id;
        Date = date;
        Time = time;
        Reserved = false;
        SpinRate = 0;
        WashingTemperature = 0;
    }
}