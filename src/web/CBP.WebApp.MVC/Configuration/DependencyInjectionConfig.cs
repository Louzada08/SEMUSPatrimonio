using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Services;
using CBP.WebApp.MVC.Services.Handlers;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using Polly;
using Polly.Retry;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using CBP.WebAPI.Core.Usuario;

namespace CBP.WebApp.MVC.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSingleton<IValidationAttributeAdapterProvider, EmailValidationAttributeAdapterProvider>();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IAspNetUser, AspNetUser>();

      //services.AddAutoMapper(typeof(ViewModelToDomainMappingProfile), typeof(DomainToViewModelMappingProfile));
      services.AddAutoMapper(typeof(Startup));

      #region HttpServices
      services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

      services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IResponsavelService, ResponsavelService>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IPatrimonioService, PatrimonioService>()
        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<ITermoTransferenciaService, TermoTransferenciaService>()
          .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IUsuarioService, UsuarioService>()
          .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
      #endregion
    }
  }

  public class PollyExtensions
  {
    public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
    {
      var retry = HttpPolicyExtensions
          .HandleTransientHttpError()
          .WaitAndRetryAsync(new[]
          {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
          }, (outcome, timespan, retryCount, context) =>
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Tentando pela {retryCount} vez!");
            Console.ForegroundColor = ConsoleColor.White;
          });

      return retry;
    }
  }
}