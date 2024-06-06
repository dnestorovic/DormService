using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Payment.Common.Repository;
using Payment.GRPC.Protos;

namespace Payment.GRPC.Services
{
    public class PaymentService : PaymentProtoService.PaymentProtoServiceBase
    {
        private readonly IDebtsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IDebtsRepository repository, IMapper mapper, ILogger<PaymentService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ReduceCreditResponse> ReduceCredit(ReduceCreditRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Server: Reduction of credit in progress...");
 
            var response = new ReduceCreditResponse();
            response.SuccessfulTransaction = await _repository.ReduceCredit(request.Id, request.Amount);

            return response;
        }
    }
}
