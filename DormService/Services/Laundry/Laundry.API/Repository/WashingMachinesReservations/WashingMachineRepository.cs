using Laundry.API.Entities;
using Laundry.API.Data;
using MongoDB.Driver;

namespace Laundry.API.Repositories;

public class WashingMachineRepository: IWashingMachineRepository
{
    private readonly IWashingMachineContext _context;
    private readonly IWashingMachineManagementContext _managementContext;
    
    public WashingMachineRepository(IWashingMachineContext context, IWashingMachineManagementContext managementContext)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _managementContext = managementContext ?? throw new ArgumentNullException(nameof(managementContext));
    }

    public async Task<WashingMachine> GetWashingMachine(string id)
    {
        return await _context.WashingMachines.Find(wm => wm._id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<WashingMachine>> GetWashingMachinesByDate(string date)
    {
        IEnumerable<WashingMachine> machines = await _context.WashingMachines.Find(wm => wm.Date == date).ToListAsync();
        if (machines == null || !machines.Any())
        {
            IEnumerable<WashingMachineConfiguration> availableMachines = await _managementContext.ManageableMachines.Find(wm => true).ToListAsync();
            machines = LaundryDayGenerator.CreateNewDefaultDay(date, availableMachines);
            _context.WashingMachines.InsertMany(machines);
        }

        return machines;
    }


    public async Task<bool> ReserveWashingMachine(string id) {
        WashingMachine machine = await GetWashingMachine(id);
        if (machine.Reserved)
        {
            return false;
        }
        machine.Reserved = true;
        var updateResult = await _context.WashingMachines.ReplaceOneAsync(wm => wm._id == id, machine);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
}