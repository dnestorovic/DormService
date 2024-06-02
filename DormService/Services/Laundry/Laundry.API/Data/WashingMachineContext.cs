using Laundry.API.Entities;
using MongoDB.Driver;

namespace Laundry.API.Data;

public class WashingMachineContext: IWashingMachineContext
{
    public IMongoCollection<WashingMachine> WashingMachines { get; }

    public WashingMachineContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
        var database = client.GetDatabase("LaundryDB");

        WashingMachines = database.GetCollection<WashingMachine>("WashingMachines");
    }
}