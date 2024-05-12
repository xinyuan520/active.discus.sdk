namespace Discus.SDK.File.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceMinio(this IServiceCollection services, IConfigurationSection minioSection)
        {
            if (services.HasRegistered(nameof(AddServiceMinio)))
                return services;

            services.Configure<MinioConfig>(minioSection)
                .AddSingleton<IStorageClient, StorageClient>();
            return services;
        }
    }
}
