using System;

namespace Accelerate.Application.Services
{
    /// <summary>
    /// Base class for application services.
    /// </summary>
    public abstract class ApplicationService : IApplicationService
    {
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        protected ApplicationService()
        {
        }

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
        /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            _disposed = true;
        }
    }
}