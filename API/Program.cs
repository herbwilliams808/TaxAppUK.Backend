using Application.Services;
using Shared.Models;
using Shared.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddAzureWebAppDiagnostics();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

// Load configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Register services
builder.Services.Configure<AzureBlobSettings>(builder.Configuration.GetSection("AzureBlobSettings"));
builder.Services.AddSingleton<AzureBlobTaxRatesService>();
builder.Services.AddSingleton<TaxEstimationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application startup begins...");

try
{
    var blobService = app.Services.GetRequiredService<AzureBlobTaxRatesService>();
    var taxRates = await blobService.LoadTaxRatesAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "Error during application startup.");
    throw;
}

// Only enable Swagger in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();