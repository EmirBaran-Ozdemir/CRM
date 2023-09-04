using CRM.DataAccess.Abstract;
using CRM.DataAccess.Concrete;
using System.Linq.Expressions;

namespace CRM.DataAccess.Repository
{
	public class GenericRepo<T> : IGenericDal<T> where T : class
	{

		private readonly CRMContext _context;
		public GenericRepo(CRMContext context)
		{
			_context = context;
		}
		public void Add(T t)
		{
			_context.Add(t);
			_context.SaveChanges();
		}

		public void Delete(T t)
		{
			_context.Remove(t);
			_context.SaveChanges();
		}

		public List<T> GetListAll()
		{
			return _context.Set<T>().ToList();
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id)!;
		}

		public List<T> GetListAll(Expression<Func<T, bool>> filter)
		{
			return _context.Set<T>().Where(filter).ToList();
		}

		public void Update(T t)
		{
			_context.Update(t);
			_context.SaveChanges();
		}

		public T AddAndGet(T t)
		{
			Add(t);
			return t;
		}
	}
}
