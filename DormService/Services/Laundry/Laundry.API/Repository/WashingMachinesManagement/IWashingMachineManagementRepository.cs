using Laundry.API.Entities;

namespace Laundry.API.Repositories;

public interface IWashingMachineManagementRepository
{
    
    Task AddNewWashingMachineConfiguration(WashingMachineConfigurationDTO config);

    Task<IEnumerable<WashingMachineConfiguration>> GetWashingMachinesConfigurations();

}