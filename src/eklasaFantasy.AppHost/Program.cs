using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Configure SQL Server with existing instance
var identityDb = builder.AddSqlServer("eklasaFantasy-sqlserver")
    .WithDataVolume()
    .WithHttpEndpoint(port: 5000, targetPort: 1433) // Assuming 1433 is the default SQL Server port
    .AddDatabase("identityDb"); // Use existing database


var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

// Services
var identityApi = builder.AddProject<Projects.Identity_API>("identity-api", launchProfileName)
    .WithHttpEndpoint(port: 5001, name: "identity-http")
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);
;

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);


// Apps
var webApp = builder
    .AddNpmApp("webApp", "../eklasaFantasy.WebApp")
    .WithExternalHttpEndpoints()
    .WithEnvironment("IdentityUrl", identityEndpoint);
    //.WithHttpEndpoint(env: "PORT")
    ;



//identityApi.WithEnvironment("WebAppClient", webApp.GetEndpoint(launchProfileName));


builder.Build().Run();


static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "ESHOP_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
