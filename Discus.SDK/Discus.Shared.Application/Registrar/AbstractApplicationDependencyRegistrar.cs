namespace Discus.Shared.Application.Registrar
{
    public abstract class AbstractApplicationDependencyRegistrar : IDependencyRegistrar
    {
        public string Name => "application";

        public abstract Assembly ApplicationLayerAssembly { get; }
        public abstract Assembly ContractsLayerAssembly { get; }
        protected IConfiguration Configuration { get; init; }

        protected IServiceCollection Services { get; init; }
        protected IServiceInfo ServiceInfo { get; init; }
        protected IConfigurationSection MysqlSection { get; init; }
        protected IConfigurationSection ConsulSection { get; init; }
        protected IConfigurationSection RabbitMqSection { get; init; }

        protected IConfigurationSection RedisSection { get; init; }

        protected IConfigurationSection MinioSection { get; init; }

        protected IConfigurationSection SqliteSection { get; init; }

        public IConfigurationSection MongoSection { get; set; }


        protected static List<Type> DefaultInterceptorTypes => new() { };

        public AbstractApplicationDependencyRegistrar(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentException("IServiceCollection is null.");
            Configuration = services.GetConfiguration() ?? throw new ArgumentException("Configuration is null.");
            ServiceInfo = services.GetServiceInfo() ?? throw new ArgumentException("ServiceInfo is null.");
            MysqlSection = Configuration.GetSection(NodeConsts.Mysql);
            ConsulSection = Configuration.GetSection(NodeConsts.Consul);
            RabbitMqSection = Configuration.GetSection(NodeConsts.RabbitMq);
            RedisSection = Configuration.GetSection(NodeConsts.Redis);
            MinioSection = Configuration.GetSection(NodeConsts.Minio);
            SqliteSection = Configuration.GetSection(NodeConsts.Sqlite);
            MongoSection = Configuration.GetSection(NodeConsts.MongoDb);
        }

        #region 注册所有服务
        /// <summary>
        /// 注册所有服务
        /// </summary>
        public abstract void AddService();

        protected virtual void AddApplicaitonDefault()
        {
            AddApplicationSharedServices();
            AddAppliactionSerivcesWithInterceptors();
            //AddEfCoreContextWithRepositories();
        }

        /// <summary>
        /// 注册application.shared层服务
        /// </summary>
        protected void AddApplicationSharedServices()
        {
            Services.AddSingleton(typeof(Lazy<>));
            Services.AddServiceConsul(ConsulSection);
            Services.AddServiceRedis(RedisSection);
            Services.AddIdGenerater(RedisSection);
            Services.AddSqlSugar(MysqlSection);
            Services.AddServiceMinio(MinioSection);
            //Services.AddSqlite(SqliteSection);
            Services.AddServiceMongoDB(MongoSection);
        }
        #endregion

        #region 注册Application服务
        /// <summary>
        /// 注册Application服务
        /// </summary>
        protected virtual void AddAppliactionSerivcesWithInterceptors(Action<IServiceCollection> action = null)
        {
            action?.Invoke(Services);
            var appServiceType = typeof(IService);

            var serviceTypes = ContractsLayerAssembly.GetExportedTypes().Where(type => type.IsInterface && type.IsAssignableTo(appServiceType)).ToList();
            serviceTypes.ForEach(serviceType =>
            {
                var implType = ApplicationLayerAssembly.ExportedTypes.FirstOrDefault(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true));
                if (implType is null)
                    return;

                Services.AddScoped(implType);
                Services.TryAddSingleton(new ProxyGenerator());
                Services.AddScoped(serviceType, provider =>
                {
                    var interfaceToProxy = serviceType;
                    var target = provider.GetService(implType);
                    var interceptors = DefaultInterceptorTypes.ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor).ToArray();
                    var proxyGenerator = provider.GetService<ProxyGenerator>();
                    var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(interfaceToProxy, target, interceptors);
                    return proxy;
                });
            });
        }
        #endregion

        #region 注册CAP-Rabbit组件
        protected virtual void AddEventBusCap()
        {
            Services.AddEventBusCap(option =>
            {
                var mysqlConfig = MysqlSection.Get<MySqlOptions>();
                option.UseMySql(config =>
                {
                    config.ConnectionString = mysqlConfig.ConnectionString;
                    config.TableNamePrefix = "cap";
                });

                var rabbitMqConfig = RabbitMqSection.Get<RabbitMqConfiguration>();
                option.UseRabbitMQ(option =>
                {
                    option.HostName = rabbitMqConfig.HostName;
                    option.VirtualHost = rabbitMqConfig.VirtualHost;
                    option.Port = rabbitMqConfig.Port;
                    option.UserName = rabbitMqConfig.UserName;
                    option.Password = rabbitMqConfig.Password;
                    option.ConnectionFactoryOptions = (facotry) =>
                    {
                        facotry.ClientProvidedName = ServiceInfo.Id;
                    };
                });

                option.Version = ServiceInfo.Version;
                //默认值：cap.queue.{程序集名称},在 RabbitMQ 中映射到 Queue Names。
                option.DefaultGroupName = $"cap.{ServiceInfo.ServiceName}";
                //默认值：60 秒,重试 & 间隔
                //在默认情况下，重试将在发送和消费消息失败的 4分钟后 开始，这是为了避免设置消息状态延迟导致可能出现的问题。
                //发送和消费消息的过程中失败会立即重试 3 次，在 3 次以后将进入重试轮询，此时 FailedRetryInterval 配置才会生效。
                option.FailedRetryInterval = 60;
                //默认值：50,重试的最大次数。当达到此设置值时，将不会再继续重试，通过改变此参数来设置重试的最大次数。
                option.FailedRetryCount = 50;
                //默认值：NULL,重试阈值的失败回调。当重试达到 FailedRetryCount 设置的值的时候，将调用此 Action 回调
                //，你可以通过指定此回调来接收失败达到最大的通知，以做出人工介入。例如发送邮件或者短信。
                option.FailedThresholdCallback = (failed) =>
                {
                    //todo
                };
                //默认值：24*3600 秒（1天后),成功消息的过期时间（秒）。
                //当消息发送或者消费成功时候，在时间达到 SucceedMessageExpiredAfter 秒时候将会从 Persistent 中删除，你可以通过指定此值来设置过期的时间。
                option.SucceedMessageExpiredAfter = 24 * 3600;
                //默认值：1,消费者线程并行处理消息的线程数，当这个值大于1时，将不能保证消息执行的顺序。
                option.ConsumerThreadCount = 1;
                //Dashboard
                option.UseDashboard(x =>
                {
                    x.PathMatch = $"/{ServiceInfo.ServiceName}/cap";
                });
            });
        }
        #endregion
    }
}
