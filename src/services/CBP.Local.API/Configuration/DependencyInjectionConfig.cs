using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using CBP.Core.Mediator;
using CBP.Local.API.Application.Commands;
using CBP.Local.API.Application.Events;
using CBP.Local.API.Data;
using CBP.Local.API.Models;
using CBP.Local.API.Data.Repository;
using CBP.Local.API.Application.Queries;

namespace CBP.Local.API.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      // API
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddAutoMapper(typeof(Startup));

      // Commands
      services.AddScoped<IRequestHandler<RegistrarLocalCommand, ValidationResult>, LocalCommandHandler>();
      //services.AddScoped<IRequestHandler<AtualizarResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      //services.AddScoped<IRequestHandler<RemoverResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      services.AddScoped<IRequestHandler<ObterLocalCommand, ValidationResult>, LocalCommandHandler>();

      // Events
      services.AddScoped<INotificationHandler<LocalRegistradoEvent>, LocalEventHandler>();

      // Application
      services.AddScoped<IMediatorHandler, MediatorHandler>();
      services.AddScoped<ILocalQueries, LocalQueries>();

      // Data
      services.AddScoped<ILocalRepository, LocalRepository>();
      services.AddScoped<LocalContext>();
    }
  }
}