using Laundry.API.Entities;
using Laundry.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WashingMachineController: ControllerBase
{
    private IWashingMachineRepository _repository;

    public WashingMachineController(IWashingMachineRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WashingMachine>> GetWashingMachine(string id)
    {
        
        var washingMachine = await _repository.GetWashingMachine(id);
        if (washingMachine is null)
        {
            return NotFound(null);
        }

        return Ok(washingMachine);
    }

    [HttpGet("all/{date}")]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WashingMachine>>> GetWashingMachinesByDate(string date)
    {   
        IEnumerable<WashingMachine> washingMachines = await _repository.GetWashingMachinesByDate(date);
        return Ok(washingMachines);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    
    public async Task<ActionResult<bool>> ReserveWashingMachine(string id)
    {   
        // TODO: add payment transaction
        bool updated = await _repository.ReserveWashingMachine(id);
        return updated ? Ok(updated) : BadRequest(updated);
    }
    

}