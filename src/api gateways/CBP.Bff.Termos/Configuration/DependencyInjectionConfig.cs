using System;
using CBP.Bff.Termos.Extensions;
using CBP.Bff.Termos.Services;
using CBP.WebAPI.Core.Extensions;
using CBP.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace CBP.Bff.Termos.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IAspNetUser, AspNetUser>();

      services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

      services.AddHttpClient<IPatrimonioService, PatrimonioService>()
        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        .AddPolicyHandler(PollyExtensions.EsperarTentar())
        .AddTransientHttpErrorPolicy(
          p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IGuiaService, GuiaService>()
          .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<ITermoTransferenciaService, PedidoService>()
          .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IClienteService, ClienteService>()
          .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
  }
}