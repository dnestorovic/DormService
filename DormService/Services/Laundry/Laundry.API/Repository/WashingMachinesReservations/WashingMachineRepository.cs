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