using Accelerate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Base class for data repository based on entity framework.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of data entity managed in the repository.
    /// </typeparam>
    /// <typeparam name="TOptions">
    /// Type of configuration options.
    /// </typeparam>
#if NETSTANDARD
    [ExcludeFromCodeCoverage]
#else
    [ExcludeFromCodeCoverage(Justification = "Unit tests for entity framework are unavailable.")]
#endif
    public abstract class EntityFrameworkRepository<TEntity, TOptions> : Repository<TEntity, TOptions> where TEntity : class, IEntity, new()
                                                                                                       where TOptions : EntityFrameworkRepositoryOptions
    {
        private EntityFrameworkRepositoryContext<TEntity, TOptions> _context;
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of class.
        /// </summary>
        /// <param name="options">
        /// Configuration options of data repository.
        /// </param>
        protected EntityFrameworkRepository(IOptions<TOptions> options) : base(options)
        {
            _context = new EntityFrameworkRepositoryContext<TEntity, TOptions>(this);
        }

        /// <inheritdoc/>
        public override Int32 Commit()
        {
            try
            {
                return _context.SaveChanges(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DataException("A concurrency violation is encountered while saving data to the database", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DataException("An error is encountered while saving data to the database", ex);
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while commiting changes", ex);
            }
        }
        /// <inheritdoc/>
        public override void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The entity to delete cannot be null", nameof(entity));
            }

            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while deleting the entity", ex);
            }
        }
        /// <inheritdoc/>
        public override void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("The collection of entities to delete cannot be null", nameof(entities));
            }

            try
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while deleting the collection of entities", ex);
            }
        }
        /// <inheritdoc/>
        public override void Delete(Expression<Func<TEntity, Boolean>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression to determine the collection of entities to delete cannot be null", nameof(expression));
            }

            try
            {
                var entities = _context.Set<TEntity>()
                                       .Where(expression)
                                       .ToList();

                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while deleting entities using a expression", ex);
            }
        }
        /// <inheritdoc/>
        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            base.Dispose(disposing);

            if (disposing)
            {
                _context.Dispose();
                _context = null;
            }

            _disposed = true;
        }
        /// <inheritdoc/>
        public override void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The entity to insert cannot be null", nameof(entity));
            }

            try
            {
                _context.Entry(entity).State = EntityState.Added;
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while inserting the entity", ex);
            }
        }
        /// <inheritdoc/>
        public override void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("The collection of entities to delete cannot be null", nameof(entities));
            }

            try
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while inserting the collection of entities", ex);
            }
        }
        /// <summary>
        /// Configure the database (and other options) to be used for the context.
        /// </summary>
        /// <param name="optionsBuilder">
        /// A builder used to create or modify options for this context.
        /// </param>
        protected internal virtual void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        /// <summary>
        /// Configure the model that was discovered by convention from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        /// <inheritdoc/>
        public override void Rollback()
        {
            try
            {
                if (Options.TrackChanges)
                {
                    var entityEntries = _context.ChangeTracker.Entries()
                                                              .Where(entityEntry => entityEntry.State != EntityState.Unchanged);

                    foreach (var entityEntry in entityEntries)
                    {
                        switch (entityEntry.State)
                        {
                            case EntityState.Added:
                                entityEntry.State = EntityState.Detached;
                                break;
                            case EntityState.Deleted:
                                entityEntry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Modified:
                                entityEntry.State = EntityState.Unchanged;
                                break;
                        }
                    }

                    _context.SaveChanges(true);
                }
                else
                {
                    _context = new EntityFrameworkRepositoryContext<TEntity, TOptions>(this);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DataException("A concurrency violation is encountered while undoing changes", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DataException("An error is encountered while undoing changes", ex);
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while undoing changes", ex);
            }
        }
        /// <inheritdoc/>
        public override IEnumerable<TEntity> Select()
        {
            try
            {
                return _context.Set<TEntity>()
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while selecting all entities", ex);
            }
        }
        /// <inheritdoc/>
        public override IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression to determine the collection of entities to select cannot be null", nameof(expression));
            }

            try
            {
                return _context.Set<TEntity>()
                               .Where(expression)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression", ex);
            }
        }
        /// <inheritdoc/>
        public override IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression to determine the collection of entities to delete cannot be null", nameof(expression));
            }

            if (skip < 0)
            {
                throw new ArgumentException("The number of entities to skip must be major or equals than 0", nameof(skip));
            }

            try
            {
                return _context.Set<TEntity>()
                               .Where(expression)
                               .Skip<TEntity>(skip)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression", ex);
            }
        }
        /// <inheritdoc/>
        public override IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression to determine the collection of entities to delete cannot be null", nameof(expression));
            }

            if (skip < 0)
            {
                throw new ArgumentException("The number of entities to skip must be major or equals than 0", nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentException("The number of entities to take must be major than 0", nameof(take));
            }

            try
            {
                return _context.Set<TEntity>()
                               .Where(expression)
                               .Skip<TEntity>(skip)
                               .Take<TEntity>(take)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while selecting a collection of entities using a expression", ex);
            }
        }
        /// <inheritdoc/>
        public override void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The entity to update cannot be null", nameof(entity));
            }

            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while updating the entity", ex);
            }
        }
        /// <inheritdoc/>
        public override void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("The collection of entities to update cannot be null", nameof(entities));
            }

            try
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Unexpected error is encountered while updating the collection of entities", ex);
            }
        }
    }
}