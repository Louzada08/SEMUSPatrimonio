using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CBP.Transferencia.API.Data;
using CBP.WebAPI.Core.Usuario;

namespace CBP.Transferencia.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<TermoTransferenciaContext>();
        }
    }
}