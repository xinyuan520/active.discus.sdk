using Discus.SDK.Core.Configuration;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
var jsonfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
var configuration = builder.AddJsonFile(jsonfile).Build();
var consulSection = configuration.GetSection("Consul");
var consulUrl = configuration.GetValue<ConsulConfig>("Consul");
Console.WriteLine(consulUrl);
//IServiceCollection services = new ServiceCollection();

//services.AddServiceConsul(consulSection);