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
    
    [HttpDelete("/room/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> DeleteWashingMachineConfiguration(string id)
    {   
        bool result = await _repository.DeleteWashingMachineConfiguration(id);
        return result ? Ok(true) : BadRequest(false);
    }
}