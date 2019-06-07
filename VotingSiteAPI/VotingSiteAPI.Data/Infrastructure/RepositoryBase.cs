using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace VotingSiteAPI.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
	{
		private VotingSiteAPIDbCtx _dataContext;
		private readonly IDbSet<T> _dbSet;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="databaseFactory">An instance of any concrete class
		/// that implements the IDatabaseFactory interface.
		/// </param>
		protected RepositoryBase(IDatabaseFactory databaseFactory)
		{
			DatabaseFactory = databaseFactory;
			_dbSet = DataContext.Set<T>();
		}

		protected IDatabaseFactory DatabaseFactory
		{
			get;
			private set;
		}

		protected VotingSiteAPIDbCtx DataContext => _dataContext ?? (_dataContext = DatabaseFactory.Get());

        public virtual void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public virtual void Update(T entity)
		{
			_dbSet.Attach(entity);
			_dataContext.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public virtual void Delete(Expression<Func<T, bool>> where)
		{
			IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
			foreach (T obj in objects)
			{
				_dbSet.Remove(obj);
			}
		}

		public virtual T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		//public virtual T GetById(string id)
		//{
		//	return _dbSet.Find(id);
		//}

		public virtual IEnumerable<T> GetAll()
		{
			return _dbSet.ToList();
		}

		public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
		{
			return _dbSet.Where(where).ToList();
		}

		public T Get(Expression<Func<T, bool>> where)
		{
			return _dbSet.Where(where).FirstOrDefault<T>();
		}

		/// <summary>
		/// Calls the DbContext method SaveChanges()
		/// </summary>
		/// <history>
		/// <historyItem Initials="SKF" Date="19-Apr-2019" Desc="Created." />
		/// </history>
		public void Commit()
		{
			_dataContext.SaveChanges();
		}
	}
}
