using BackgroundTask.MediatR.Interfaces;
using BackgroundTask.Persistence.SQLite.Interfaces;
using BackgroundTask.Persistence.SQLite.Persistence;
using BackgroundTask.Persistence.SQLite.Persistence.Intefaces;
using BackgroundTask.Persistence.SQLite.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundTask.Persistence.SQLite {
    public class SqlLitePersistence : IPersistenceExtension {
        private readonly IServiceCollection _services;

        public SqlLitePersistence( IServiceCollection services ) {
            _services = services;
        }

        public void addPersistence( string connectionString ) {
            _services.AddSingleton<IInitializeDataBaseRepository>( provider => new InitializeDataBaseRepository( connectionString ) );

            _services.AddDbContext<ApplicationDBContext>( c => c.UseSqlite( connectionString ) );
            _services.AddScoped<IApplicationDBContext>( provider => provider.GetRequiredService<ApplicationDBContext>() );

        }
    }
}