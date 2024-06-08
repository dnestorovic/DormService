
using Payment.GRPC.Protos;

namespace Canteen.API.GrpcServices
{
    public class PaymentGrpcService
    {
        private readonly PaymentProtoService.PaymentProtoServiceClient _paymentProtoServiceClient;

        public PaymentGrpcService(PaymentProtoService.PaymentProtoServiceClient paymentProtoServiceClient)
        {
            _paymentProtoServiceClient = paymentProtoServiceClient ?? throw new ArgumentNullException(nameof(paymentProtoServiceClient));
        }

        public async Task<ReduceCreditResponse> ReduceCredit(string username, int amount)
        {
            var reduceCreditRequest = new ReduceCreditRequest();
            reduceCreditRequest.Id = username;
            reduceCreditRequest.Amount = amount;
            return await _paymentProtoServiceClient.ReduceCreditAsync(reduceCreditRequest);
        }
    }
}
