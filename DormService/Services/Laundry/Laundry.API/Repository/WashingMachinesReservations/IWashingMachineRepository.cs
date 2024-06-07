using Laundry.API.Entities;

namespace Laundry.API.Repositories;

public interface IWashingMachineRepository
{
    
    Task<WashingMachine> GetWashingMachine(string id);

    Task<IEnumerable<WashingMachine>> GetWashingMachinesByDate(string date);

    Task<bool> ReserveWashingMachine(WashingMachineReservationDTO dto);
}