using Laundry.API.Entities;
using Laundry.API.Data;

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
}