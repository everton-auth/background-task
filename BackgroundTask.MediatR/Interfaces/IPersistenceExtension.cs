using Microsoft.Extensions.DependencyInjection;

namespace BackgroundTask.MediatR.Interfaces {
    public interface IPersistenceExtension {
        public void addPersistence( string connectionString );
    }
}
