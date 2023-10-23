using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Webjob.Template;

var builder = new HostBuilder().ConfigureWebJobs(b =>
{
    b.AddTimers();
});

builder.ConfigureAppConfiguration((hostContext, configApp) =>
{
    configApp.AddJsonFile("appsettings.json", true, true);
    configApp.AddEnvironmentVariables();
    if (hostContext.HostingEnvironment.IsDevelopment())
    {
        configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
        configApp.AddUserSecrets<Program>();
    }
}).ConfigureHostConfiguration(configurationBuilder => {
    configurationBuilder.AddCommandLine(args);
}).ConfigureServices((host, services) =>
{
    services.AddScoped<Functions, Functions>();
    

}).UseSerilog((hostContext, services, lg) =>
{
    lg.ReadFrom.Configuration(hostContext.Configuration, "Serilog")
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .Enrich.WithCorrelationIdHeader("correlationId");
});
var tokenSource = new CancellationTokenSource();
CancellationToken ct = tokenSource.Token;
var host = builder.Build();

using (host)
{
    await host.RunAsync(ct);
    tokenSource.Dispose();
}