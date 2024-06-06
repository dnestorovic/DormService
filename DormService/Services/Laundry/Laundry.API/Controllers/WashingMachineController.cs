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

    [HttpGet("/economic")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WashingMachine>> GetPromotedWashingMachineId()
    {   
        string? washingMachineId = await _managementRepository.GetPromotedWashingMachineId();
        return washingMachineId is null ? NotFound() : Ok(washingMachineId);
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
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> ReserveWashingMachine([FromBody] WashingMachineReservationDTO dto)
    {   
        // TODO: replace hardcoded ID and value with variables
        try {
            await _grpcService.ReduceCredit("ID", 200);
        } catch (RpcException e) {
            return BadRequest(e.Message);
        }

        bool reservationSuccessful = await _reservationRepository.ReserveWashingMachine(dto);
        if (!reservationSuccessful)
        {
            return BadRequest("Cannot reserve this washing machine.");
        }
        
        bool updated = await _managementRepository.UpdateMetrics(dto);
        bool updateMetricsSuccessful = await _managementRepository.UpdateMetrics(dto);
        return updateMetricsSuccessful ? Ok(true) : BadRequest("Cannot update machine metrics.");
    }
}