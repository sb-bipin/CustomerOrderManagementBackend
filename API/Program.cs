using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog first
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console() // ✅ This writes logs to terminal
    .CreateLogger();

builder.Host.UseSerilog(); // ✅ Hook Serilog into the host

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Optional: Use built-in logging in addition to Serilog (not required if you're only using Serilog)
builder.Logging.ClearProviders(); // Clear default loggers
builder.Logging.AddConsole();     // Console logger writes to Kestrel terminal
builder.Logging.SetMinimumLevel(LogLevel.Debug); // Ensure all logs are shown

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedInitialData.SeedAsync(services);
}

// Enable Swagger only in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // ✅ Log HTTP requests

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
