using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using System.Linq.Expressions;

namespace CRM.DataAccess.Repository
{
	public class GenericRepo<T> : IGenericDal<T> where T : class
	{

		CRMContext context = new CRMContext();
		public void Add(T t)
		{
			context.Add(t);
			context.SaveChanges();
		}

		public void Delete(T t)
		{
			context.Remove(t);
			context.SaveChanges();
		}

		public List<T> GetListAll()
		{
			return context.Set<T>().ToList();
		}

		public T GetById(int id)
		{
			return context.Set<T>().Find(id);
		}

		public List<T> GetListAll(Expression<Func<T, bool>> filter)
		{
			return context.Set<T>().Where(filter).ToList();
		}

		public void Update(T t)
		{
			context.Update(t);
			context.SaveChanges();
		}
	}
}
