using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Interfaces {

    public interface IBackgroundCommandHandler<TCommand> where TCommand : IBackgroundCommand {
        Task HandleAsync( TCommand command , CancellationToken cancellationToken );
    }
}