using Payment.GRPC.Protos;

namespace Laundry.API.GrpcServices
{
    public class PaymentGrpcService
    {
        private readonly PaymentProtoService.PaymentProtoServiceClient _paymentProtoServiceClient;

        public PaymentGrpcService(PaymentProtoService.PaymentProtoServiceClient paymentProtoServiceClient)
        {
            _paymentProtoServiceClient = paymentProtoServiceClient ?? throw new ArgumentNullException(nameof(paymentProtoServiceClient));

        }

        public async Task<ReduceCreditResponse> ReduceCredit(string studentID, decimal amount)
        {
            var request = new ReduceCreditRequest();
            request.Id = studentID;
            request.Amount = Convert.ToInt32(amount);

            return await _paymentProtoServiceClient.ReduceCreditAsync(request);
        }
    }


}