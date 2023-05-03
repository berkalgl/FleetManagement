using Microsoft.EntityFrameworkCore;
using Shipping.API.Infrastructure.Middlewares;
using Shipping.API.Infrastructure.Modules;
using Shipping.Infrastructure;
using Shipping.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.RegisterServices();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<ShipmentDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "ShipmentDb"));
builder.Services.AddDbContext<LogDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "LogDb"));

var app = builder.Build();

var dbInstance = app.Services.CreateScope().ServiceProvider.GetRequiredService<ShipmentDbContext>();
SeedData.Initialize(dbInstance);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global error handler
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
