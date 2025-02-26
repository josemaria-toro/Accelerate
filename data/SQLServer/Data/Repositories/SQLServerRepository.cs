using Accelerate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Data depository based on SQLServer.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of data entity managed in the repository.
    /// </typeparam>
#if NETSTANDARD
    [ExcludeFromCodeCoverage]
#else
    [ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
#endif
    public abstract class SQLServerRepository<TEntity> : EntityFrameworkRepository<TEntity, SQLServerRepositoryOptions> where TEntity : class, IEntity, new()
    {
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for data repository.
        /// </param>
        protected SQLServerRepository(IOptions<SQLServerRepositoryOptions> options) : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            base.Dispose(disposing);

            _disposed = true;
        }
        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(Options.ConnectionString, (options) =>
            {
                options.CommandTimeout(Options.Timeout);
            });
        }
    }
}