using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Databases
/*var postgres = builder.AddPostgres("postgres")
    .WithImage("ankane/pgvector")
    .WithImageTag("latest");*/

var sql = builder.AddSqlServer("sql");


var identityDb = sql.AddDatabase("identitydb");



var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

// Services
var identityApi = builder.AddProject<Projects.Identity_API>("identity-api", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);
;

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);


// Apps
var webApp = builder
    .AddNpmApp("webApp", "../WebApp")
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
