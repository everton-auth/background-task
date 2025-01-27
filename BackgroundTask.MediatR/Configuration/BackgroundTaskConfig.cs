using System;
using System.Collections.Generic;
using System.Reflection;

namespace BackgroundTask.MediatR.Configuration {
    public class BackgroundTaskConfig {

        public List<Assembly> Assemblies { get; } = new List<Assembly>();

        public void RegisterServicesFromAssemblies( params Assembly[] assemblies ) {
            Assemblies.AddRange( assemblies );
        }

        public void addPersistence( Action<PersistenceTaskConfig> ConfigOptions ) {

        }
    }
}
