namespace CRM.Business.Abstract
{
	public interface IGenericService<T> where T : class
	{
		List<T> GetListAll();
		void Add(T t);
		void Update(T t);
		void Delete(T t);
		T GetById(int id);
	}
}
