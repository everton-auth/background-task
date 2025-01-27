using BackgroundTask.Interfaces;
using BackgroundTask.Persistence.SQLite.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Persistence.SQLite.Persistence.Intefaces {
    public interface ICommandRepository {
        public Task<CommandEntity> RegisterCommand( IBackgroundCommand command , CancellationToken cancellationToken );
        public Task<CommandEntity> FinalizeCommand( CommandEntity command , CancellationToken cancellationToken );
        public Task<List<CommandEntity>> GetCommands( Expression<Func<CommandEntity , bool>> predicate , CancellationToken cancellationToken , bool untrack = false );
    }
}
