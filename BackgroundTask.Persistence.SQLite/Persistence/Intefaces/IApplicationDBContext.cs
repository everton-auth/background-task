using BackgroundTask.Persistence.SQLite.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Persistence.SQLite.Persistence.Intefaces {
    public interface IApplicationDBContext {
        Task<int> SaveChangesAsync( CancellationToken cancellationToken );
        DbSet<CommandEntity> Commands { get; }
    }
}
