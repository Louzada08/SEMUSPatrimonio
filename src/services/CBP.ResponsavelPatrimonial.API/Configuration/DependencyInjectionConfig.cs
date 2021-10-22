using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using CBP.Core.Mediator;
using CBP.ResponsavelPatrimonial.API.Application.Commands;
using CBP.ResponsavelPatrimonial.API.Application.Events;
using CBP.ResponsavelPatrimonial.API.Data;
using CBP.ResponsavelPatrimonial.API.Models;
using CBP.ResponsavelPatrimonial.API.Data.Repository;
using CBP.ResponsavelPatrimonial.API.Application.Queries;
using CBP.WebAPI.Core.Usuario;

namespace CBP.ResponsavelPatrimonial.API.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      // API
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IAspNetUser, AspNetUser>();
      services.AddAutoMapper(typeof(Startup));

      // Commands
      services.AddScoped<IRequestHandler<RegistrarResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      //services.AddScoped<IRequestHandler<AtualizarResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      //services.AddScoped<IRequestHandler<RemoverResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      services.AddScoped<IRequestHandler<ObterResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();

      // Events
      services.AddScoped<INotificationHandler<ResponsavelRegistradoEvent>, ResponsavelEventHandler>();

      // Application
      services.AddScoped<IMediatorHandler, MediatorHandler>();
      services.AddScoped<IResponsavelQueries, ResponsavelQueries>();

      // Data
      services.AddScoped<IResponsavelRepository, ResponsavelRepository>();
      services.AddScoped<ResponsavelContext>();
    }
  }
}