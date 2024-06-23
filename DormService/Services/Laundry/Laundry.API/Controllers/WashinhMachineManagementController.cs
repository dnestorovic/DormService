using Laundry.API.Entities;
using Laundry.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Laundry.API.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class WashingMachineManagementController: ControllerBase
{
    private IWashingMachineManagementRepository _repository;
    private IWashingMachineRepository _reservationRepository;


    public WashingMachineManagementController(IWashingMachineManagementRepository repository, IWashingMachineRepository reservationRepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }


    [HttpPost("/room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddNewWashingMachineConfiguration([FromBody] WashingMachineConfigurationDTO config)
    {   
        await _repository.AddNewWashingMachineConfiguration(config);
        return Ok();
    }

    [HttpGet("/room")]
    [ProducesResponseType(typeof(WashingMachineConfiguration), StatusCodes.Status200OK)]
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
        bool canDelete = await _reservationRepository.DeleteMachines(id);
        if (!canDelete) {
            return BadRequest(false);
        }

        bool result = await _repository.DeleteWashingMachineConfiguration(id);
        return result ? Ok(true) : BadRequest(false);
    }
}