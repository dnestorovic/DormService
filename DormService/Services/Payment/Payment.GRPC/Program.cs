using Payment.GRPC.Services;
using Payment.GRPC.Protos;

using Payment.Common.Extensions;
using Payment.Common.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddPaymentCommonServices();
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<bool, ReduceCreditResponse>().ReverseMap();     // TODO
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
