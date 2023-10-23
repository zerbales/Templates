
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Console.Template;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder().ConfigureAppConfiguration((hostContext, configApp) => {
    configApp.AddJsonFile("appsettings.json", true, true);
    configApp.AddEnvironmentVariables();

    if (hostContext.HostingEnvironment.IsDevelopment())
    {
        configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
        configApp.AddUserSecrets<Program>();
    }
    var cf = configApp.Build();
    string? keyVaultUrl = cf.GetSection("KeyVaultUri").Value;
    if (!string.IsNullOrEmpty(keyVaultUrl))
    {
        Uri keyVaultUri = new Uri(uriString: keyVaultUrl);
        //DefaultAzureCredential presa da utente loggato in az
        //var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential(includeInteractiveCredentials: true));
        string? managedIdentity = cf.GetSection("ManagedIdentity").Value;
        var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = managedIdentity }));
        configApp.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
    }
}).ConfigureHostConfiguration(configurationBuilder => {
    configurationBuilder.AddCommandLine(args);
}).ConfigureServices((host, services) =>
{
    services.AddHostedService<TemplateHostedService>();
    //services.AddSingleton<IKeyVaultManager, KeyVaultManager>();
}).ConfigureLogging((cl) => { 
    cl.ClearProviders();
    cl.AddConsole(); 
});
var host = builder.Build();
await host.RunAsync();
