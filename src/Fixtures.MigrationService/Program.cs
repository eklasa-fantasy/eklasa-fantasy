using Fixtures.API.Data;
using Fixtures.API.Interfaces;
using Fixtures.API.Services;
using Fixtures.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

// builder.Services.AddScoped<IFootballApiService, FootballApiService>();
// builder.Services.AddScoped<IFixtureService, FixtureService>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<FixturesDbContext>("fixt_sqldata");

var host = builder.Build();
host.Run();
