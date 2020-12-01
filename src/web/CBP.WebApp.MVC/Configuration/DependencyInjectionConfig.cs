using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CBP.WebApp.MVC.Extensions;
using CBP.WebApp.MVC.Services;
using CBP.WebApp.MVC.Services.Handlers;
using Microsoft.Extensions.Configuration;
using System;

namespace CBP.WebApp.MVC.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

      services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

      services.AddHttpClient<IPatrimonioService, PatrimonioService>()
        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

      //services.AddHttpClient("Refit",options =>
      //{
      //  options.BaseAddress = new Uri(configuration.GetSection("PatrimonioUrl").Value);
      //})
      //  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
      //  .AddTypedClient(Refit.RestService.For<IPatrimonioServiceRefit>);

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddScoped<IUser, AspNetUser>();
    }
  }
}