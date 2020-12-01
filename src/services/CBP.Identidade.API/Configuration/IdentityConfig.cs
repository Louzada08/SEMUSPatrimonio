using CBP.Identidade.API.Data;
using CBP.Identidade.API.Extensions;
using CBP.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CBP.Identidade.API.Configuration
{
  public static class IdentityConfig
  {
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

      services.AddDefaultIdentity<IdentityUser>()
          .AddRoles<IdentityRole>()
          .AddErrorDescriber<IdentityMensagensPortugues>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      services.AddJwtConfiguration(configuration);

      return services;
    }
  }
}