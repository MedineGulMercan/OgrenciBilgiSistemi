using Microsoft.EntityFrameworkCore;
using OgrenciBilgiSistemi.Domain.DataBaseContext;

namespace OgrenciBilgiSistemi.Configration
{
    public static class DataBaseStartup
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services)
        {
            string connectionString = "Server=DESKTOP-F7K2U1C\\SQLEXPRESS;database=OgrenciBilgiSistemi3;Integrated Security = true; TrustServerCertificate=True;";
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            return services;
        }
    }
}
