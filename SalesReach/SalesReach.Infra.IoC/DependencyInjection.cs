using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesReach.Infra.Data;
using SalesReach.Infra.Data.Settings;

namespace SalesReach.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void InitInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            #region .: Configuração Connection :.
            services.Configure<SettingsDataBase>(x => configuration.GetSection("DefaultConnection").Bind(x));
            services.AddScoped<DbSession>();
            #endregion
        }
    }
}
