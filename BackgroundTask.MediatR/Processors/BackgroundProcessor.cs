using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using BackgroundTask.Interfaces;
using BackgroundTask.MediatR.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundTask.Processors {


    public class BackgroundCommandProcessor : IBackgroundCommandProcessor {
        private readonly Channel<IBackgroundCommand> _commandChannel;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPersistenceExtension _persistence;

        public BackgroundCommandProcessor( IServiceProvider serviceProvider , IPersistenceExtension persistence ) {
            _commandChannel = Channel.CreateUnbounded<IBackgroundCommand>();
            _serviceProvider = serviceProvider;
            _persistence = persistence;
        }

        public async Task SendAsync( IBackgroundCommand command , CancellationToken cancellationToken ) {
            if( command == null ) throw new ArgumentNullException( nameof( command ) );

            await _commandChannel.Writer.WriteAsync( command );
            _ = Task.Run( () => ProcessCommandsAsync( CancellationToken.None ) );
        }

        public async Task ProcessCommandsAsync( CancellationToken cancellationToken ) {
            while( await _commandChannel.Reader.WaitToReadAsync( cancellationToken ) ) {
                while( _commandChannel.Reader.TryRead( out var command ) ) {
                    var commandType = command.GetType();
                    var handlerType = typeof( IBackgroundCommandHandler<> ).MakeGenericType( commandType );

                    using var scope = _serviceProvider.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService( handlerType );

                    var handleMethod = handlerType.GetMethod( "HandleAsync" );
                    if( handleMethod != null ) {
                        try {
                            await ( Task ) handleMethod.Invoke( handler , new object[] { command , CancellationToken.None } )!;
                        } catch( Exception ex ) {
                            Console.WriteLine( $"Error processing command: {ex.Message}" );
                        }
                    }

                }
            }
        }
    }
}