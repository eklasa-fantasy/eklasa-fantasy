using Projects;

var builder = DistributedApplication.CreateBuilder(args);


var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

//Services
var identityApi = builder.AddProject<Projects.Identity_API>("identity-api", launchProfileName)
    .WithExternalHttpEndpoints()
;

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);

var webApp = builder
    .AddNpmApp("webApp", "../WebApp")
    //.WithHttpEndpoint(env: "PORT")
    ;



builder.Build().Run();


static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "ESHOP_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
