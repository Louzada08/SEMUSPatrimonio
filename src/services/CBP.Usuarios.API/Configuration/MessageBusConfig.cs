using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CBP.MessageBus;
using CBP.Usuarios.API.Services;
using CBP.Core.Utils;

namespace CBP.Usuarios.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistroUsuarioIntegrationHandler>();
        }
    }
}