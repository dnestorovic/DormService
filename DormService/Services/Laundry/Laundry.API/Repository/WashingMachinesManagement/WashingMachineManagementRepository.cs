using Laundry.API.Entities;
using Laundry.API.Data;
using MongoDB.Driver;

namespace Laundry.API.Repositories;

public class WashingMachineManagementRepository : IWashingMachineManagementRepository
{

    private readonly IWashingMachineManagementContext _context;
    
    public WashingMachineManagementRepository(IWashingMachineManagementContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task AddNewWashingMachineConfiguration(WashingMachineConfigurationDTO config)
    {
        await _context.ManageableMachines.InsertOneAsync(new WashingMachineConfiguration(config));
    }

    public async Task<IEnumerable<WashingMachineConfiguration>> GetWashingMachinesConfigurations()
    {
        return await _context.ManageableMachines.Find(wm => true).ToListAsync();
    }
}