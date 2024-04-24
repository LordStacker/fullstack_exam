using repository;
using service;

namespace api
{
    public static class AddSingeltonsHelper
    {
        public static void AddSingeltons(this IServiceCollection services)
        {
            //Adding the data source connection
            services.AddNpgsqlDataSource(
                Utilities.FormatConnectionString(
                    Environment.GetEnvironmentVariable("pgconn")!)
                    , dataSourceBuilder => dataSourceBuilder.EnableParameterLogging()
            );

            //Adding singeltons
            services.AddSingleton<UserService>();
            services.AddSingleton<UserRepository>();
            services.AddSingleton<DeviceService>();
            services.AddSingleton<DeviceRepository>();
        }
    }
}