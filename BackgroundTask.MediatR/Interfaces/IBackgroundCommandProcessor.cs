using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Interfaces {

    public interface IBackgroundCommandProcessor {
        Task ProcessCommandsAsync( CancellationToken cancellationToken );
        Task SendAsync( IBackgroundCommand command , CancellationToken cancellationToken );
    }
}
