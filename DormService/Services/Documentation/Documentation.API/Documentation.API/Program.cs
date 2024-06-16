using Documentation.API.Data;
using Documentation.API.Repositories.Interfaces;
using Mailing;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<IDocumentContext, DocumentContext>();
builder.Services.AddScoped<IDocumentationListRepository, DocumentationListRepository>();
//builder.Services.AddEmailService(builder.Configuration);
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
