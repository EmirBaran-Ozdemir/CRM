using AutoMapper;

namespace CRM.WebUI.PackageConf
{
	public class AutoMapperConfiguration
	{
		public static IMapper Configure()
		{
			var config = new MapperConfiguration(cfg =>
			{
				//cfg.CreateMap<TSource, TDestination>();
			});

			return config.CreateMapper();
		}
	}
}
