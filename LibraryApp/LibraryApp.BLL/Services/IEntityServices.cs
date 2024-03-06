using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.Services
{
	public interface IEntityServices<T> where T : class, new()
	{

		void Insert(T entity);
		T Update(T entity);
		int Save();
		IEnumerable<T> GetList();
		IQueryable<T> GetListQuerable();
		IEnumerable<T> GetList(Expression<Func<T, bool>> _lambda);
		T FirstOrDefault(Expression<Func<T, bool>> _lambda);
		bool Any(Expression<Func<T, bool>> _lambda);
		int Count(Expression<Func<T, bool>> _lambda);


	}
}
