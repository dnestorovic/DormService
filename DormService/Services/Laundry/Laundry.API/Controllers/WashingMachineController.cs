using Grpc.Core;
using Laundry.API.Entities;
using Laundry.API.GrpcServices;
using Laundry.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WashingMachineController: ControllerBase
{
    private IWashingMachineRepository _reservationRepository;
    private IWashingMachineManagementRepository _managementRepository;
    private PaymentGrpcService _grpcService;


    public WashingMachineController(IWashingMachineRepository reservationRepository, IWashingMachineManagementRepository managementRepository, PaymentGrpcService grpcService)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _managementRepository = managementRepository ?? throw new ArgumentNullException(nameof(managementRepository)); 
        _grpcService = grpcService ?? throw new ArgumentNullException(nameof(grpcService));
    } 


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WashingMachine>> GetWashingMachine(string id)
    {
        
        var washingMachine = await _reservationRepository.GetWashingMachine(id);
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
        IEnumerable<WashingMachine> washingMachines = await _reservationRepository.GetWashingMachinesByDate(date);
        return Ok(washingMachines);
    }


    [HttpPut()]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> ReserveWashingMachine([FromBody] WashingMachineReservationDTO dto)
    {   
        // TODO: replace hardcoded ID and value with variables and process exceptions 
        await _grpcService.ReduceCredit("ID", 200);

        bool reservationSuccessful = await _reservationRepository.ReserveWashingMachine(dto);
        if (!reservationSuccessful)
        {
            return BadRequest(false);
        }
        
        bool updated = await _managementRepository.UpdateMetrics(dto);
        bool updateMetricsSuccessful = await _managementRepository.UpdateMetrics(dto);
        return updateMetricsSuccessful ? Ok(true) : BadRequest(false);
    }
}