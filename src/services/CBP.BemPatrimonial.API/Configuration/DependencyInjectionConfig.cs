using CBP.BemPatrimonial.API.Data;
using CBP.BemPatrimonial.API.Data.Repository;
using CBP.BemPatrimonial.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CBP.Catalogo.API.Configuration
{
  public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPatrimonioRepository, PatrimonioRepository>();
            services.AddScoped<PatrimonioContext>();
        }
    }
}