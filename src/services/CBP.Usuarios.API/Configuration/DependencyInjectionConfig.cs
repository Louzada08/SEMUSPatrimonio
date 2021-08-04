using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CBP.Usuarios.API.Application.Commands;
using CBP.Usuarios.API.Application.Events;
using CBP.Usuarios.API.Data;
using CBP.Usuarios.API.Models;
using CBP.Core.Mediator;
using CBP.Usuarios.API.Data.Repository;

namespace CBP.Usuarios.API.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IMediatorHandler, MediatorHandler>();

      services.AddScoped<IRequestHandler<RegistrarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();
      //services.AddScoped<IRequestHandler<RemoverUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();

      services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();

      services.AddAutoMapper(typeof(Startup));

      services.AddScoped<IUsuarioRepository, UsuarioRepository>();
      services.AddScoped<UsuarioContext>();
    }
  }
}