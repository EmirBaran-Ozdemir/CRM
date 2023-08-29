using AutoMapper;
using CRM.DataTypeObjects.Models;
using CRM.Entity.Concrete;

namespace CRM.WebUI.PackageConf
{
	public class AutoMapperConfiguration
	{
		public static IMapper Configure()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<User, LoginModel>()
					.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
					.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

			});

			return config.CreateMapper();
		}
	}
}
