using Laundry.API.Data;
using Laundry.API.Repositories;
using Laundry.API.GrpcServices;
using Payment.GRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IWashingMachineContext, WashingMachineContext>();
builder.Services.AddScoped<IWashingMachineRepository, WashingMachineRepository>();
builder.Services.AddScoped<IWashingMachineManagementContext, WashingMachineManagementContext>();
builder.Services.AddScoped<IWashingMachineManagementRepository, WashingMachineManagementRepository>();

// gRPC
builder.Services.AddGrpcClient<PaymentProtoService.PaymentProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:PaymentUrl"]));
builder.Services.AddScoped<PaymentGrpcService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();