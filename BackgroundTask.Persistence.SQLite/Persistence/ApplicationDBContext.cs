using BackgroundTask.Persistence.SQLite.Domain.Entity;
using BackgroundTask.Persistence.SQLite.Persistence.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Persistence.SQLite.Persistence {
    public class ApplicationDBContext : DbContext, IApplicationDBContext {

        public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options ) : base( options ) { }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            optionsBuilder.EnableSensitiveDataLogging( true );
        }

        public DbSet<CommandEntity> Commands { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            base.OnModelCreating( modelBuilder );
        }

        public override Task<int> SaveChangesAsync( CancellationToken cancellationToken = default ) {
            return base.SaveChangesAsync( cancellationToken );
        }
    }
}
