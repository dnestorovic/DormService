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
    public async Task<bool> DeleteWashingMachineConfiguration(string id)
    {
        var result = await _context.ManageableMachines.DeleteOneAsync(wm => wm._id == id);
        return result.DeletedCount == 1;
    }

    public async Task<bool> UpdateMetrics(WashingMachineReservationDTO reservation)
    {
        WashingMachineConfiguration machine = await _context.ManageableMachines.Find(wm => wm._id == reservation.ConfigurationId).FirstAsync();
        machine.UpdateUtilizationFactor(reservation.SpinRate, reservation.WashingTemperature);
        var result = await _context.ManageableMachines.ReplaceOneAsync(wm => wm._id == reservation.ConfigurationId, machine);

        return result.IsAcknowledged && result.ModifiedCount == 1;
    }
}