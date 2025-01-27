using BackgroundTask;
using BackgroundTask.Persistence.SQLite;
using System.Reflection;

namespace Example;
public static class DependencyInjection {
    public static IServiceCollection AddDependencies( this IServiceCollection services ) {

        services.AddBackgroundTasks( opt => {
            opt.RegisterServicesFromAssemblies( Assembly.GetExecutingAssembly() );
            opt.addPersistence( c => c.AddDataBase<SqlLitePersistence>( "Data Source=./BackgroundTask.db" ) );
        } );

        return services;
    }

}
