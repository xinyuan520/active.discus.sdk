namespace Discus.SDK.Repository.SqlSugar.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSqlSugar(this IServiceCollection services, IConfigurationSection mysqlSection)
        {
            if (services.HasRegistered(nameof(AddSqlSugar)))
                return services;

            services.Configure<MysqlConfig>(mysqlSection).AddSqlSugar(mysqlSection);
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<ISqlSugarClient>(provider =>
            {
                var db = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = mysqlSection.GetValue<string>("ConnectionString"),
                    DbType = DbType.MySql, // 根据数据库类型选择对应的DbType
                    IsAutoCloseConnection = true, // 自动关闭数据库连接
                    InitKeyType = InitKeyType.Attribute // 使用属性方式进行表和列的映射
                });

                // 输出Sql语句到控制台，方便调试
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n");
                };

                return db;
            });
            return services;
        }
    }
}
