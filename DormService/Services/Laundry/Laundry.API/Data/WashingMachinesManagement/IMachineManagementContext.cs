using Laundry.API.Entities;
using MongoDB.Driver;

namespace Laundry.API.Data;

public interface IWashingMachineManagementContext
{
    public IMongoCollection<WashingMachineConfiguration> ManageableMachines { get; set; }
}