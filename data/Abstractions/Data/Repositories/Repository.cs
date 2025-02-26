using Accelerate.Data.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Base class for data repositories.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of data entity managed in the repository.
    /// </typeparam>
    /// <typeparam name="TOptions">
    /// Type of configuration options.
    /// </typeparam>
    public abstract class Repository<TEntity, TOptions> : IRepository<TEntity> where TEntity : class, IEntity, new()
                                                                               where TOptions : RepositoryOptions
    {
        private Boolean _disposed;
        private TOptions _options;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for data repository.
        /// </param>
        protected Repository(IOptions<TOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentException("Options for data repository cannot be null", nameof(options));
        }

        /// <summary>
        /// Configuration options of repository.
        /// </summary>
        protected internal TOptions Options => _options;

        /// <inheritdoc />
        public abstract Int32 Commit();
        /// <inheritdoc />
        public abstract void Delete(TEntity entity);
        /// <inheritdoc />
        public abstract void Delete(IEnumerable<TEntity> entities);
        /// <inheritdoc />
        public abstract void Delete(Expression<Func<TEntity, Boolean>> expression);
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

            if (disposing)
            {
                _options = null;
            }

            _disposed = true;
        }
        /// <inheritdoc />
        public abstract void Insert(TEntity entity);
        /// <inheritdoc />
        public abstract void Insert(IEnumerable<TEntity> entities);
        /// <inheritdoc />
        public abstract void Rollback();
        /// <inheritdoc />
        public abstract IEnumerable<TEntity> Select();
        /// <inheritdoc />
        public abstract IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
        /// <inheritdoc />
        public abstract IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
        /// <inheritdoc />
        public abstract IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
        /// <inheritdoc />
        public abstract void Update(TEntity entity);
        /// <inheritdoc />
        public abstract void Update(IEnumerable<TEntity> entities);
    }
}