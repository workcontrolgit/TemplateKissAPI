using Microsoft.Extensions.DependencyInjection;
using $ext_projectname$.Application.Interfaces;
using $safeprojectname$.Repository;
using SqlKata.Compilers;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using $ext_projectname$.Application.Constants;
using SqlKata.Execution;

namespace $safeprojectname$
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //SQLKata DI Container https://sqlkata.com/docs/
            services.AddScoped(factory =>
            {
                return new QueryFactory
                {
                    Compiler = new SqlServerCompiler(),
                    Connection = new SqlConnection(configuration[ConfigurationConstants.ConnectionString]),
                    Logger = compiled => Console.WriteLine(compiled)
                };
            });
            //GenFu DI Container https://github.com/MisterJames/GenFu
            services.AddSingleton(typeof(IMockRepository<>), typeof(MockRepository<>));
        }
    }
}
