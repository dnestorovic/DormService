using Laundry.API.Entities;
using Laundry.API.Data;
using MongoDB.Driver;

namespace Laundry.API.Repositories;

public class WashingMachineRepository: IWashingMachineRepository
{
    private readonly IWashingMachineContext _context;
    
    public WashingMachineRepository(IWashingMachineContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
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
            machines = LaundryDayGenerator.CreateNewDefaultDay(date);
            _context.WashingMachines.InsertMany(machines);
        }

        return machines;
    }
}