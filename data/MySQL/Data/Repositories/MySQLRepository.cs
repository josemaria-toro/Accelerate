using Accelerate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Data depository based on MySQL.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of data entity managed in the repository.
    /// </typeparam>
#if NETSTANDARD
    [ExcludeFromCodeCoverage]
#else
    [ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
#endif
    public abstract class MySQLRepository<TEntity> : EntityFrameworkRepository<TEntity, MySQLRepositoryOptions> where TEntity : class, IEntity, new()
    {
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options for data repository.
        /// </param>
        protected MySQLRepository(IOptions<MySQLRepositoryOptions> options) : base(options)
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

            optionsBuilder.UseMySQL(Options.ConnectionString, (options) =>
            {
                options.CommandTimeout(Options.Timeout);
            });
        }
    }
}