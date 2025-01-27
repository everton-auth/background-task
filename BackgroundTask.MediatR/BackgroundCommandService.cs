using BackgroundTask.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask {

    public class BackgroundCommandService : BackgroundService {
        private readonly IBackgroundCommandProcessor _processor;

        public BackgroundCommandService( IBackgroundCommandProcessor processor ) {
            _processor = processor;
        }

        protected override async Task ExecuteAsync( CancellationToken stoppingToken ) {
            await _processor.ProcessCommandsAsync( stoppingToken );
        }
    }
}