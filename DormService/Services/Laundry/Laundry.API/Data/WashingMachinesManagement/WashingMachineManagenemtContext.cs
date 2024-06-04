using Laundry.API.Entities;
using MongoDB.Driver;

namespace Laundry.API.Data;

public class WashingMachineManagementContext: IWashingMachineManagementContext
{
    public IMongoCollection<WashingMachineConfiguration> ManageableMachines { get; set; }


    public WashingMachineManagementContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
        var database = client.GetDatabase("LaundryDB");

        ManageableMachines = database.GetCollection<WashingMachineConfiguration>("MachineManagement");
    }
}