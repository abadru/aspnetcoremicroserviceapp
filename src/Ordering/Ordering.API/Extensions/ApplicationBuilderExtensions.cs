using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Ordering.API.Consumer;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusRabbitMQConsumer Listener { get; set; }
        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMQConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopped.Register(OnStopped);

            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopped()
        {
            Listener.Disconnect();
        }
    }
}
