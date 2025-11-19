using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// 1. Add Health Checks
// ----------------------------
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "sqlserver"
    );

// ----------------------------
// 2. Add HealthCheck UI
// ----------------------------
builder.Services.AddHealthChecksUI(options =>
    {
        options.SetEvaluationTimeInSeconds(10); // refresh every 10s
        options.MaximumHistoryEntriesPerEndpoint(60);
    })
    .AddInMemoryStorage();

var app = builder.Build();

// API endpoint
app.MapGet("/", () => "API Running");

// ----------------------------
// 3. Expose health endpoints
// ----------------------------

// Raw health check JSON output
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// HealthCheck UI Dashboard
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";        // UI dashboard route
    options.ApiPath = "/health-ui-api";   // backend API route
});

app.Run();