using Discus.SDK.MongoDB.Configurations;
using Discus.SDK.MongoDB.Repository;
using MongoDB.Driver.Core.Configuration;

namespace Discus.SDK.MongoDB.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceMongoDB(this IServiceCollection services, IConfigurationSection mongoSection)
        {
            if (services.HasRegistered(nameof(AddServiceMongoDB)))
                return services;

            services.AddSingleton<IMongoClient>(new MongoClient("mongodb://124.70.198.29:27017"));
            services.Configure<MongoConfiguration>(mongoSection)
                 .AddScoped<IMongoRepository, MongoRepository>();
            return services;
        }
    }
}
