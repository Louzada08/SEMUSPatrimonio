using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CBP.Catalogo.API.Configuration
{
  public static class SwaggerConfig
  {
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo()
        {
          Title = "NerdStore Enterprise Patrimônio API",
          Description = "Esta API faz parte da Unidade de Controle de Patrimônio/SEMUS made ASP.NET Core 3.1.",
          Contact = new OpenApiContact() { Name = "Anderson Luiz Louzada", Email = "valuz.anderson.to@gmail.com" },
          License = new OpenApiLicense() { Name = "PRIVATE", Url = new Uri("http://saude.palmas.to.gov.br") }
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "Insira o token JWT desta maneira: Bearer {seu token}",
          Name = "Authorization",
          Scheme = "Bearer",
          BearerFormat = "JWT",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                    {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                    }
                  },
                  new string[] { }
                }
        });

      });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
      });
    }
  }
}