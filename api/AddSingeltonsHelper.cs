using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql.Replication;
using repository;

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
    
        }
    }
}