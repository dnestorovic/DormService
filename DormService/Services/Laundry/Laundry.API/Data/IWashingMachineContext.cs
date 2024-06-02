using Laundry.API.Entities;
using MongoDB.Driver;

namespace Laundry.API.Data;

public interface IWashingMachineContext
{
    public IMongoCollection<WashingMachine> WashingMachines { get; set; }
}