using System;

namespace Accelerate.Testing
{
    /// <summary>
    /// Base class for tests.
    /// </summary>
    /// <typeparam name="TContext">
    /// Type of context use on the test.
    /// </typeparam>
    public abstract class Test<TContext> : IDisposable where TContext : TestContext
    {
        private TContext _context;
        private Boolean _disposed;

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="context">
        /// Test context.
        /// </param>
        protected Test(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Context used on the test.
        /// </summary>
        protected TContext Context => _context;

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
                _context = null;
            }

            _disposed = true;
        }
    }
}