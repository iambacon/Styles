using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IAmBacon.Data.Infrastructure;
using IAmBacon.Domain.Services.Interfaces;
using IAmBacon.Model.Common;

namespace IAmBacon.Domain.Services
{
    /// <summary>
    /// The service base.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity.
    /// </typeparam>
    public abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit Of Work.</param>
        protected ServiceBase(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.Repository = repository;
            this.UnitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Gets the repository.
        /// </summary>
        protected IRepository<TEntity> Repository { get; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }
        
        /// <summary>
        /// The create method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public virtual TEntity Create(TEntity entity)
        {
            IResult result = this.Save(entity);

            // Todo: This needs sorting out.
            return entity;
        }

        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public IResult Delete(TEntity entity)
        {
            this.Repository.Delete(entity);
            this.UnitOfWork.Commit();

            return new Result(true);
        }

        /// <summary>
        /// The get method.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public TEntity Get(int id)
        {
            return this.Repository.Find(id);
        }

        /// <summary>
        /// The get method.
        /// </summary>
        /// <param name="where">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> @where)
        {
            return this.Repository.Find(where);
        }
        
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<TEntity> GetAll()
        {
            return this.Repository.GetAll();
        }

        /// <summary>
        /// The save method.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public abstract IResult Save(TEntity entity);
    }
}