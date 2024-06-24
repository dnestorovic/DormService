using Grpc.Core;
using Laundry.API.Entities;
using Laundry.API.GrpcServices;
using Laundry.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Mailing.Data;
using Mailing;
using Microsoft.AspNetCore.Authorization;

namespace Laundry.API.Controllers;

[Authorize(Roles = "Student")]
[ApiController]
[Route("[controller]")]
public class WashingMachineController: ControllerBase
{
    private IWashingMachineRepository _reservationRepository;
    private IWashingMachineManagementRepository _managementRepository;
    private PaymentGrpcService _grpcService;
    private readonly IEmailService _emailService;


    public WashingMachineController(IWashingMachineRepository reservationRepository,
        IWashingMachineManagementRepository managementRepository,
        PaymentGrpcService grpcService,
        IEmailService emailService)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _managementRepository = managementRepository ?? throw new ArgumentNullException(nameof(managementRepository)); 
        _grpcService = grpcService ?? throw new ArgumentNullException(nameof(grpcService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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

    [HttpGet("economic")]
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

    [HttpGet("student/{studentId}")]
    [ProducesResponseType(typeof(WashingMachine), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WashingMachine>>> GetWashingMachinesByStudentId(string studentId)
    {   
        IEnumerable<WashingMachine> washingMachines = await _reservationRepository.GetWashingMachinesByStudentId(studentId);
        return Ok(washingMachines);
    }


    [HttpPut()]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> ReserveWashingMachine([FromBody] WashingMachineReservationDTO dto)
    {   
        try {
            var response = await _grpcService.ReduceCredit(dto.StudentId, dto.Price);
            if (!response.SuccessfulTransaction) {
                return BadRequest("Check credit");
            }
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
        if (!updateMetricsSuccessful) {
            BadRequest("Cannot update machine metrics.");
        }

        Email email = new(dto.EmailAddress, 
                        "Hi " + dto.StudentId + "\n you have successfully reserved washing machine. \nRegards \nDormServcie", 
                        "Washing machine reservation");
        var emailSent = await _emailService.SendEmail(email);
        if (!emailSent) {
            await _emailService.SendEmail(email);
        }

        return Ok(true);
    }
}