using BasicDemo.Data;
using BasicDemo.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.Configure<EmailSettingsOptions>(builder.Configuration.GetSection("EmailSettings"));
builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

var memCollection = new Dictionary<string, string>
{
    {"MainSetting:SubSetting","sub setting from dictionary" }
};

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

configuration.AddJsonFile("custom.json",optional: true, reloadOnChange: true);
configuration.AddXmlFile("custom.xml", optional: true, reloadOnChange: true);
configuration.AddInMemoryCollection(memCollection);

if (environment.IsDevelopment())
{
    configuration.AddUserSecrets<Program>();
}

//if(environment.IsProduction())
//{
//    var builtConfig = builder.Build();

//    var azureServiceTokenProvider = new AzureServiceTokenProvider();
//    var keyVaultClient = new KeyVaultClient(
//        new KeyVaultClient.AuthenticationCallback(
//            azureServiceTokenProvider.KeyVaultTokenCallback));

//    builder.AddAzureKeyVault(
//        $"https//{builtConfig["KeyVaultName"]}.vault.azure.net/",
//        keyVaultClient,
//        new DefaultKeyVaultSecretManager());
//}

configuration.AddEnvironmentVariables();
configuration.AddCommandLine(args);





var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
