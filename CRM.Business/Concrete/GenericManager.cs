using CRM.Business.Abstract;
using CRM.DataAccess.Repository;

namespace CRM.Business.Concrete
{
	public class GenericManager<T> : IGenericService<T> where T : class
	{

		GenericRepo<T> repo = new GenericRepo<T>();
		public void Add(T t)
		{
			if (t.Equals(null))
			{
				throw new Exception("This value can't be null.");
			}

			repo.Add(t);
		}
		public void Delete(T t)
		{
			repo.Delete(t);
		}
		public List<T> GetListAll()
		{
			return repo.GetListAll();
		}
		public T GetById(int id)
		{
			return repo.GetById(id);
		}
		public void Update(T t)
		{
			repo.Update(t);
		}
	}
}
