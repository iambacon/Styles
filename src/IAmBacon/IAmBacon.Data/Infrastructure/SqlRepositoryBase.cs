
namespace IAmBacon.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using IAmBacon.Data.Context;

    /// <summary>
    /// The sql repository base.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public abstract class SqlRepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        #region Fields

        /// <summary>
        /// The db set.
        /// </summary>
        private readonly IDbSet<TEntity> dbSet;

        /// <summary>
        /// The context.
        /// </summary>
        private BaconContext context;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        protected SqlRepositoryBase(IDatabaseFactory databaseFactory)
        {
            this.DatabaseFactory = databaseFactory;
            this.dbSet = this.Context.Set<TEntity>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected BaconContext Context
        {
            get
            {
                return this.context ?? (this.context = this.DatabaseFactory.Get());
            }
        }

        /// <summary>
        /// Gets the database factory.
        /// </summary>
        protected IDatabaseFactory DatabaseFactory { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity");
            }

            return this.dbSet.Add(entity);
        }

        /// <summary>
        /// The as queryable.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return this.dbSet.AsQueryable();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot delete a null entity");
            }

            this.dbSet.Remove(entity);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = this.dbSet.Where(where).AsEnumerable();

            foreach (TEntity entity in objects)
            {
                this.Delete(entity);
            }
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            return this.dbSet.Find(keyValues);
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            // accepting ordering and filtering parameters ensures that the work is done
            // on the database rather than in memory on the web server.

            // Could also include a parameter that lets the caller pass in a comma-delimited list
            // of navigation properties for eager loading.
            IQueryable<TEntity> query = this.dbSet;

            if (where != null)
            {
                query = query.Where(where);
            }

            query = includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        /// <summary>
        /// Finds the specified where expression.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> @where)
        {
            return this.dbSet.Where(where);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.dbSet;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot attach a null entity");
            }

            this.dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        #endregion
    }
}