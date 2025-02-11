var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithExternalHttpEndpoints()
    .WithPgAdmin()
    .WithImage("ankane/pgvector")
    .WithImageTag("v0.5.1");

if (builder.Environment.IsDevelopment())
{
    var postgresSource = builder.AddParameter("PostgresSource", secret: true);

    postgres = postgres.WithDataBindMount(source: postgresSource.Resource.Value);
}

var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";


var identityDb = postgres.AddDatabase("identitydb");

var identityApi = builder.AddProject<Projects.Identity_API>("identity-api")
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);

var webApp = builder.AddProject<Projects.WebApp>("webapp")
    .WithEnvironment("IdentityUrl", identityEndpoint);

webApp
    .WithEnvironment("CallBackUrl", webApp.GetEndpoint(launchProfileName));

identityApi
    .WithEnvironment("WebAppClient", webApp.GetEndpoint(launchProfileName));

builder.Build().Run();

// For test use only.
// Looks for an environment variable that forces the use of HTTP for all the endpoints. We
// are doing this for ease of running the Playwright tests in CI.
static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "FORUM_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
