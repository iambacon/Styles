namespace IAmBacon.Domain.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Model.Common;

    /// <summary>
    /// The Service interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IService<TEntity>
        where TEntity : class
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        IResult Delete(TEntity entity);

        /// <summary>
        /// The get method.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Get(int id);

        /// <summary>
        /// The get method.
        /// </summary>
        /// <param name="where">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// The save method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        IResult Save(TEntity entity);

        #endregion
    }
}