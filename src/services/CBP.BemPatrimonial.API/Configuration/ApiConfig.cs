using CBP.BemPatrimonial.API.Data;
using CBP.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NSE.Catalogo.API.Configuration
{
  public static class ApiConfig
  {
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<PatrimonioContext>(options =>
          options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

      services.AddControllers();

      services.AddCors(options =>
      {
        options.AddPolicy("Total",
                  builder =>
                      builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
      });
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors("Total");

      app.UseAuthConfiguration(); // essa configuração tem que estar aqui entre UseRouting() e UseEndpoints

      app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllers();
            });
    }
  }
}