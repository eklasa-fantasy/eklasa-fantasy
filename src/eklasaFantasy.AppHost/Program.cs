using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Configure SQL Server with existing instance
var id_sql = builder.AddSqlServer("id-sql")
                 .AddDatabase("id-sqldata");

var fixt_sql = builder.AddSqlServer("fixt-sql")
                .AddDatabase("fixt-sqldata");

var res_sql = builder.AddSqlServer("res-sql")
                .AddDatabase("res-sqldata");


var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

// Services
var identityApi = builder.AddProject<Projects.Identity_API>("identity-api", launchProfileName)
    .WithReference(id_sql);
;

var fixturesApi = builder.AddProject<Projects.Fixtures_API>("fixtures-api", launchProfileName)
    .WithReference(fixt_sql)
;

var resultsApi = builder.AddProject<Projects.Results_API>("results-api")
    .WithReference(res_sql)
;

//Migrations
builder.AddProject<Projects.Identity_MigrationService>("identity-migrations")
    .WithReference(id_sql);
builder.AddProject<Projects.Fixtures_MigrationService>("fixtures-migrations")
    .WithReference(fixt_sql);
builder.AddProject<Projects.Results_MigrationService>("results-migrations")
    .WithReference(res_sql);

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);



// Apps
var webApp = builder
    .AddNpmApp("webApp", "../eklasaFantasy.WebApp")
    .WithExternalHttpEndpoints()
    .WithEnvironment("IdentityUrl", identityEndpoint);
    //.WithHttpEndpoint(env: "PORT")
    ;



//identityApi.WithEnvironment("WebAppClient", webApp.GetEndpoint(launchProfileName));




//identityApi.WithEnvironment("WebAppClient", webApp.GetEndpoint(launchProfileName));


builder.Build().Run();


static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "ESHOP_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
