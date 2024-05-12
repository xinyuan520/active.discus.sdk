using Discus.SDK.Consul.Configurations;
using Discus.SDK.Consul.Extensions;
using Discus.SDK.Core.Configuration;
using Discus.SDK.Core.Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace Discus.Gateway.Ocelot;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile($"{AppContext.BaseDirectory}/ocelot.direct.json", true, true);

        #region 获取appsetting.json配置文件
        var tokenConfig = builder.Configuration.GetSection("JWT").Get<JWTConfig>();
        var threadPoolConfig = builder.Configuration.GetSection("ThreadPoolSettings");
        var consulOption = builder.Configuration.GetSection("Consul").Get<ConsulConfiguration>();
        #endregion
        builder.Configuration.AddConsulConfiguration(consulOption, true);

        builder.Services.Configure<ThreadPoolSettings>(threadPoolConfig)
            .AddAuthentication()
            .AddJwtBearer("mgmt", options =>
            {
                var tokenConfig = builder.Configuration.GetSection("JWT").Get<JWTConfig>();
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = tokenConfig.ValidateIssuer,
                    ValidIssuer = tokenConfig.ValidIssuer,
                    ValidateIssuerSigningKey = tokenConfig.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenConfig.Encoding.GetBytes(tokenConfig.SymmetricSecurityKey)),
                    ValidateAudience = tokenConfig.ValidateAudience,
                    ValidAudience = tokenConfig.ValidAudience,
                    ValidateLifetime = tokenConfig.ValidateLifetime,
                    RequireExpirationTime = tokenConfig.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromSeconds(tokenConfig.ClockSkew),
                };
            });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("default", policy =>
            {
                var corsHosts = builder.Configuration.GetValue<string>("CorsHosts");
                var corsHostsArray = corsHosts.Split(',');
                policy.WithOrigins(corsHostsArray)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        })
            .AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            })
            .AddOcelot(builder.Configuration)
            .AddConsul()
            .AddPolly();

        var app = builder.Build();

        app.UseCors("default")
            .UseHttpLogging()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello Ocelot!");
                });
            })
            .UseOcelot().Wait();

        app.Run();
    }
}