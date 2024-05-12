using Discus.SDK.Consul.Configurations;
using Discus.SDK.Consul.Extensions;
using Discus.Shared.Webapi;
using Discus.Shared.WebApi.Extensions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Extensions.Hosting;

public static class WebApplicationBuilderExtension
{
    /// <summary>
    /// Configure Configuration/ServiceCollection/Logging
    /// <param name="builder"></param>
    /// <param name="serviceInfo"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static WebApplicationBuilder ConfigureDefault(this WebApplicationBuilder builder, IServiceInfo serviceInfo)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));
        if (serviceInfo is null)
            throw new ArgumentNullException(nameof(serviceInfo));

        // Configuration
        var initialData = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("ServiceName", serviceInfo.ServiceName) };
        builder.Configuration.AddInMemoryCollection(initialData);
        //builder.Configuration.AddJsonFile($"{AppContext.BaseDirectory}/appsettings.shared.json", true, true);
        builder.Configuration.AddJsonFile($"{AppContext.BaseDirectory}/appsettings.json", true, true);
        builder.Configuration.AddJsonFile($"{AppContext.BaseDirectory}/hosting.json", true, true);
        var consulOption = builder.Configuration.GetSection(NodeConsts.Consul).Get<ConsulConfiguration>();
        builder.Configuration.AddConsulConfiguration(consulOption, true);
        var logOption = builder.Configuration.GetSection(NodeConsts.LogConfig).Get<LogConfig>();

        builder.UseDefaultSerilog(logOption);
        OnSettingConfigurationChanged(builder.Configuration);

        builder.Services.ReplaceConfiguration(builder.Configuration);
        builder.Services.AddSingleton(typeof(IServiceInfo), serviceInfo);
        builder.Services.AddService(serviceInfo);
        return builder;
    }

    /// <summary>
    /// Register Cofiguration ChangeCallback
    /// </summary>
    /// <param name="state"></param>
    private static IDisposable _callbackRegistration;
    private static void OnSettingConfigurationChanged(object state)
    {
        _callbackRegistration?.Dispose();
        var configuration = state as IConfiguration;
        var changedChildren = configuration.GetChildren();
        var reloadToken = configuration.GetReloadToken();

        ReplacePlaceholder(changedChildren);

        _callbackRegistration = reloadToken.RegisterChangeCallback(OnSettingConfigurationChanged, state);
    }

    /// <summary>
    /// replace placeholder
    /// </summary>
    /// <param name="sections"></param>
    private static void ReplacePlaceholder(IEnumerable<IConfigurationSection> sections)
    {
        var serviceInfo = ServiceInfo.GetInstance();
        foreach (var section in sections)
        {
            var childrenSections = section.GetChildren();
            if (childrenSections != null && childrenSections.Any())
                ReplacePlaceholder(childrenSections);

            if (string.IsNullOrWhiteSpace(section.Value))
                continue;

            var sectionValue = section.Value;
            if (sectionValue.Contains("$SERVICENAME"))
                section.Value = sectionValue.Replace("$SERVICENAME", serviceInfo.ServiceName);

            if (sectionValue.Contains("$SHORTNAME"))
                section.Value = sectionValue.Replace("$SHORTNAME", serviceInfo.ShortName);
        }
    }
}
