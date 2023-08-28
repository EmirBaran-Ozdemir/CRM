using System.Linq.Expressions;

namespace CRM.DataAccess.Abstract
{
	public interface IGenericDal<T> where T : class
	{
		List<T> GetListAll();
		void Add(T t);
		T AddAndGet(T t);
		void Update(T t);
		void Delete(T t);
		T GetById(int id);
		List<T> GetListAll(Expression<Func<T, bool>> filter);
	}
}
