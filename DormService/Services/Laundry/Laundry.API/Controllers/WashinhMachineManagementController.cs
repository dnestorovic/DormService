using Laundry.API.Entities;
using Laundry.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WashingMachineManagementController: ControllerBase
{
    private IWashingMachineManagementRepository _repository;

    public WashingMachineManagementController(IWashingMachineManagementRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpPost("/room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddNewWashingMachineConfiguration([FromBody] WashingMachineConfigurationDTO config)
    {   
        await _repository.AddNewWashingMachineConfiguration(config);
        return Ok();
    }

    [HttpGet("/room")]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WashingMachineConfiguration>>> GetWashingMachinesConfigurations()
    {   
        IEnumerable<WashingMachineConfiguration> washingMachines = await _repository.GetWashingMachinesConfigurations();
        return Ok(washingMachines);
    }
    
}