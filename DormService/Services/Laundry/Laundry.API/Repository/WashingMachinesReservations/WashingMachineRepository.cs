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

    public async Task<bool> DeleteMachines(string id)
    {
        IEnumerable<WashingMachine> allMachines = await _context.WashingMachines.Find(wm => wm.ConfigurationId == id).ToListAsync();
        if (allMachines == null || !allMachines.Any()) {
            return true; // machine is not currently in use. Can be deleted
        }

        WashingMachine alreadyReserved = await _context.WashingMachines.Find(wm => (wm.ConfigurationId == id) && wm.Reserved == true).FirstOrDefaultAsync();
        if (alreadyReserved != null) {
            return false; // somebody reserved this machine at some point
        }

        var result = await _context.WashingMachines.DeleteManyAsync(wm => wm.ConfigurationId == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
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

    public async Task<IEnumerable<WashingMachine>> GetWashingMachinesByStudentId(string studentId)
    {
        return await _context.WashingMachines.Find(wm => wm.StudentId == studentId).ToListAsync();
    }

    public async Task<bool> ReserveWashingMachine(WashingMachineReservationDTO dto) {
        WashingMachine machine = await GetWashingMachine(dto.Id);
        if (machine.Reserved)
        {
            return false;
        }
        machine.Reserved = true;
        machine.SpinRate = dto.SpinRate;
        machine.WashingTemperature = dto.WashingTemperature;
        machine.StudentId = dto.StudentId;
        var updateResult = await _context.WashingMachines.ReplaceOneAsync(wm => wm._id == dto.Id, machine);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
}