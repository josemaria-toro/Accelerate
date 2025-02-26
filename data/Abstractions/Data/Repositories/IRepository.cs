using Accelerate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Accelerate.Data.Repositories
{
    /// <summary>
    /// Data repository.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of data entity managed in the repository.
    /// </typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Persist pending repository changes.
        /// </summary>
        Int32 Commit();
        /// <summary>
        /// Remove a data entity.
        /// </summary>
        /// <param name="entity">
        /// Data entity information.
        /// </param>
        void Delete(TEntity entity);
        /// <summary>
        /// Remove the list of data entities.
        /// </summary>
        /// <param name="entities">
        /// Data entities information.
        /// </param>
        void Delete(IEnumerable<TEntity> entities);
        /// <summary>
        /// Remove the list of data entities retrieved by the expression.
        /// </summary>
        /// <param name="expression">
        /// Expression to determine the data entities to delete.
        /// </param>
        void Delete(Expression<Func<TEntity, Boolean>> expression);
        /// <summary>
        /// Insert a data entity.
        /// </summary>
        /// <param name="entity">
        /// Data entity information.
        /// </param>
        void Insert(TEntity entity);
        /// <summary>
        /// Insert a list of data entities.
        /// </summary>
        /// <param name="entities">
        /// Data entities information.
        /// </param>
        void Insert(IEnumerable<TEntity> entities);
        /// <summary>
        /// Undo pending repository changes.
        /// </summary>
        void Rollback();
        /// <summary>
        /// Retrieve all data entities in repository.
        /// </summary>
        IEnumerable<TEntity> Select();
        /// <summary>
        /// Retrieve a list of data entities by expression.
        /// </summary>
        /// <param name="expression">
        /// Expression to determine the data entities to retrieve.
        /// </param>
        IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression);
        /// <summary>
        /// Retrieve a list of data entities by expression.
        /// </summary>
        /// <param name="expression">
        /// Expression to determine the data entities to retrieve.
        /// </param>
        /// <param name="skip">
        /// Number of data entities to skip.
        /// </param>
        IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip);
        /// <summary>
        /// Select a list of data entities by expression.
        /// </summary>
        /// <param name="expression">
        /// Expression to determine the data entities to select.
        /// </param>
        /// <param name="skip">
        /// Number of data entities to skip.
        /// </param>
        /// <param name="take">
        /// Number of data entities to take.
        /// </param>
        IEnumerable<TEntity> Select(Expression<Func<TEntity, Boolean>> expression, Int32 skip, Int32 take);
        /// <summary>
        /// Update a data entity.
        /// </summary>
        /// <param name="entity">
        /// Data entity information.
        /// </param>
        void Update(TEntity entity);
        /// <summary>
        /// Update a list of data entities.
        /// </summary>
        /// <param name="entities">
        /// Data entities information.
        /// </param>
        void Update(IEnumerable<TEntity> entities);
    }
}