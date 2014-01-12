namespace IAmBacon.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity.
    /// </typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// The as queryable.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Delete(TEntity entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        void Delete(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> where, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, 
            string includeProperties);

        /// <summary>
        /// Finds the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Update(TEntity entity);

        #endregion
    }
}