using CBP.WebAPI.Core.Identidade;
using CBP.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CBP.Identidade.API.Configuration
{
  public static class ApiConfig
  {
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
      services.AddControllers();
      services.AddScoped<IAspNetUser, AspNetUser>();

      return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthConfiguration();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      return app;
    }
  }
}