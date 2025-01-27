using BackgroundTask.Interfaces;

namespace Example.Examples.BackgroundTasks; 
public class SumNumbersTask : IBackgroundCommand {
    public float Number1 { get; set; }
    public float Number2 { get; set; }
}

public class SumNumbersTaskHandler : IBackgroundCommandHandler<SumNumbersTask> {

    public Task HandleAsync( SumNumbersTask command , CancellationToken cancellationToken ) {
        Console.WriteLine( $"Sum ={command.Number1 + command.Number2}" );
        return Task.CompletedTask;
    }
}