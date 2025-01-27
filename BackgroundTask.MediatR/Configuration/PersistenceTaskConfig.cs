using BackgroundTask.MediatR.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BackgroundTask.MediatR.Configuration {
    public class PersistenceTaskConfig {
        private readonly IServiceProvider _serviceProvider;

        public PersistenceTaskConfig( IServiceProvider provider ) {
            _serviceProvider = provider;
        }

        public void AddDataBase<TPersistence>( string connectionString ) where TPersistence : IPersistenceExtension {
            var percistence = _serviceProvider.GetService<TPersistence>();
            percistence?.addPersistence( connectionString );
        }
    }
}
