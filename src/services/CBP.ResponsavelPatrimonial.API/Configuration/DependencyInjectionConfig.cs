using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CBP.ResponsavelPatrimonial.API.Application.Commands;
using CBP.ResponsavelPatrimonial.API.Application.Events;
using CBP.ResponsavelPatrimonial.API.Data;
using CBP.ResponsavelPatrimonial.API.Models;
using CBP.Core.Mediator;
using CBP.ResponsavelPatrimonial.API.Data.Repository;
using CBP.ResponsavelPatrimonial.API.Services;

namespace CBP.ResponsavelPatrimonial.API.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      services.AddScoped<IMediatorHandler, MediatorHandler>();
      services.AddScoped<IRequestHandler<RegistrarResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();

      services.AddScoped<INotificationHandler<ResponsavelRegistradoEvent>, ResponsavelEventHandler>();

      services.AddScoped<IResponsavelRepository, ResponsavelRepository>();
      services.AddScoped<ResponsavelContext>();
    }
  }
}