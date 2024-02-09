using Microsoft.AspNetCore.Hosting;

namespace OgrenciBilgiSistemi.Configration
{
    public static class AutoMapperStartup
    {
        /// <summary>
        /// Add auto mapper module.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            return services;
        }
    }
}
