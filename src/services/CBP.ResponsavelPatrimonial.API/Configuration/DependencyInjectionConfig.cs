using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CBP.ResponsavelPatrimonial.API.Application.Commands;
using CBP.ResponsavelPatrimonial.API.Application.Events;
using CBP.ResponsavelPatrimonial.API.Data;
using CBP.ResponsavelPatrimonial.API.Models;
using CBP.Core.Mediator;
using CBP.ResponsavelPatrimonial.API.Data.Repository;

namespace CBP.ResponsavelPatrimonial.API.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IMediatorHandler, MediatorHandler>();

      services.AddScoped<IRequestHandler<RegistrarResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      //services.AddScoped<IRequestHandler<RemoverResponsavelCommand, ValidationResult>, ResponsavelCommandHandler>();
      services.AddScoped<IRequestHandler<AdicionarEnderecoCommand, ValidationResult>, ResponsavelCommandHandler>();

      services.AddScoped<INotificationHandler<ResponsavelRegistradoEvent>, ResponsavelEventHandler>();

      services.AddAutoMapper(typeof(Startup));

      services.AddScoped<IResponsavelRepository, ResponsavelRepository>();
      services.AddScoped<ResponsavelContext>();
    }
  }
}