using Canteen.API.GrpcServices;
using Canteen.API.OrderMealsInfo.Repositories;
using Canteen.API.UserMealsInfo.Data;
using Canteen.API.UserMealsInfo.Repositories;
using Payment.GRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddScoped<IOrderMealsRepository, OrderMealsRepository>();

// gRPC
builder.Services.AddGrpcClient<PaymentProtoService.PaymentProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:PaymentUrl"]));
builder.Services.AddScoped<PaymentGrpcService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUserMealsContext, UserMealsContext>();
builder.Services.AddScoped<IUserMealsRepository, UserMealsRepository>();
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
