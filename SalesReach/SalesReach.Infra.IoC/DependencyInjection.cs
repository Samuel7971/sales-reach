using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesReach.Domain.Repositories.Interface;
using SalesReach.Infra.Data;
using SalesReach.Infra.Data.Repositories;
using SalesReach.Infra.Data.Settings;
using SalesReach.Infra.Data.Unit_Of_Work;

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

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IDocumentoRepository, DocumentoRepository>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
        }
    }
}
