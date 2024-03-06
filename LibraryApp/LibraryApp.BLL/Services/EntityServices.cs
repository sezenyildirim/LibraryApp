using LibraryApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.Services
{
	public class EntityServices<T> : IEntityServices<T> where T : class, new()
	{
		public LibraryDBContext _context;

		public EntityServices(LibraryDBContext context)
		{
			_context = context;
		}

		public virtual void Insert(T entity)
		{
			_context.Set<T>().Add(entity);

		}

		public virtual T Update(T entity)
		{
			//_context.ChangeTracker.Clear();

			_context.Set<T>().Attach(entity);
			_context.Entry<T>(entity).State = EntityState.Modified;
			return entity;
		}

		public int Save()
		{
			return _context.SaveChanges();
			//return 1;
		}

		public virtual IEnumerable<T> GetList()
		{
			//   return _context.Set<T>().AsEnumerable();
			return _context.Set<T>().AsEnumerable();
		}

		public virtual IQueryable<T> GetListQuerable()
		{
			return _context.Set<T>().AsQueryable();
		}

		public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> _lambda)
		{
			return _context.Set<T>().Where(_lambda).AsEnumerable();
		}

		public virtual T FirstOrDefault(Expression<Func<T, bool>> _lambda)
		{
			var returnValue = _context.Set<T>().FirstOrDefault(_lambda);
			if (returnValue != null)
			{
				return returnValue;
			}
			else
			{
				return null;
			}

		}

		public virtual T First(Expression<Func<T, bool>> _lambda)
		{
			return _context.Set<T>().First(_lambda);
		}

		public virtual bool Any(Expression<Func<T, bool>> _lambda)
		{
			return _context.Set<T>().Any(_lambda);
		}

		public virtual int Count(Expression<Func<T, bool>> _lambda)
		{
			return _context.Set<T>().Count(_lambda);
		}
	}
}
