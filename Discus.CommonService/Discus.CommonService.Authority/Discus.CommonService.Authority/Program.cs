
var webApiAssembly = System.Reflection.Assembly.GetExecutingAssembly();
var applicationFullName = UserApplicationDependencyRegistrar.ApplicationFullName;
var serviceInfo = ServiceInfo.CreateInstance(webApiAssembly, applicationFullName);
var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureDefault(serviceInfo).Build();

app.UseCustomMiddleware();

await app.ChangeThreadPoolSettings().UseConsul(serviceInfo.Id).RunAsync();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
