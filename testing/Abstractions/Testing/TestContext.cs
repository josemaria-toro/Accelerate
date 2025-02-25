using Microsoft.Extensions.Configuration;
using System;

namespace Accelerate.Testing
{
    /// <summary>
    /// Base class for tests context.
    /// </summary>
    public abstract class TestContext : IDisposable
    {
        private IConfiguration _configuration;
        private IConfigurationBuilder _configurationBuilder;
        private Boolean _disposed;
        private String _environmentName;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        protected TestContext()
        {
            _configurationBuilder = new ConfigurationBuilder();
            _configurationBuilder.SetBasePath(Environment.CurrentDirectory)
                                 .AddEnvironmentVariables()
                                 .AddInMemoryCollection()
                                 .AddJsonFile("appsettings.json", true, true);

            _environmentName = _configurationBuilder.Build()
                                                    .GetValue<String>("Environment", "Development");

            _configurationBuilder.AddJsonFile($"appsettings.{_environmentName}.json", true, true);
            _configuration = _configurationBuilder.Build();
        }

        /// <summary>
        /// Configuration service instance.
        /// </summary>
        public IConfiguration Configuration => _configuration;
        /// <summary>
        /// Name of environment where test will run.
        /// </summary>
        public String EnvironmenName => _environmentName;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicate if Object is currently freeing, releasing, or resetting unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (disposing)
            {
                _environmentName = null;
                _configuration = null;
                _configurationBuilder = null;
            }

            _disposed = true;
        }
    }
}