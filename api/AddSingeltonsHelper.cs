using System.Collections.Immutable;
using repository;
using service;

namespace api
{
    public static class AddSingeltonsHelper
    {
        public static void AddSingeltons(this IServiceCollection services, string? conn = null)
        {
            //Adding the data source connection
            services.AddNpgsqlDataSource( conn ??
                Utilities.FormatConnectionString(
                    Environment.GetEnvironmentVariable("pgconn")!)
                , dataSourceBuilder => dataSourceBuilder.EnableParameterLogging()
            );

            //Adding singeltons
                        services.AddSingleton<UserRepository>();
            services.AddSingleton<DeviceRepository>();  services.AddSingleton<SensorRepository>();
            services.AddSingleton<UserToDeviceRepository>();

            services.AddSingleton<UserService>();
            services.AddSingleton<DeviceService>();
            services.AddSingleton<SensorService>();
          
            services.AddSingleton<UserToDeviceService>();            services.AddSingleton<MqttClientService>();

        }
    }
}