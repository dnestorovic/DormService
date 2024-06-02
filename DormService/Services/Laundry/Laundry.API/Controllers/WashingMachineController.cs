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

}