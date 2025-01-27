using System;
using System.Linq;
using BackgroundTask.Interfaces;
using BackgroundTask.MediatR.Configuration;
using BackgroundTask.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundTask {
    public static class DependencyInjection {
        public static IServiceCollection AddBackgroundTasks( this IServiceCollection services , Action<BackgroundTaskConfig> Configoptions ) {
            var options = new BackgroundTaskConfig();
            Configoptions( options );

            var handlerInterfaceType = typeof( IBackgroundCommandHandler<> );

            var handlerTypes = options.Assemblies
                .SelectMany( a => a.GetTypes() )
                .Where( t => t.GetInterfaces()
                    .Any( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType ) );

            foreach( var handlerType in handlerTypes ) {
                var interfaceType = handlerType.GetInterfaces()
                    .First( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType );

                services.AddTransient( interfaceType , handlerType );
            }

            services.AddSingleton<IBackgroundCommandProcessor , BackgroundCommandProcessor>();

            // ignora as exceptions em background (caso em config padrão, ele termina o app ao estourar uma exception)
            services.Configure<HostOptions>( hostOptions =>
                  hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore );

            services.AddHostedService<BackgroundCommandService>();
            return services;
        }
    }

}