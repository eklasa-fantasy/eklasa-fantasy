using Identity.API.Data;
using Identity.MigrationService;
using Fixtures.API.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<ApplicationDbContext>("id-sqldata");
//builder.AddSqlServerDbContext<FixturesDbContext>("sqldata");

var host = builder.Build();
host.Run();
