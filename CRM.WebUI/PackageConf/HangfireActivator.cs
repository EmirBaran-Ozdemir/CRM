using Hangfire;

namespace CRM.WebUI.PackageConf
{
	public class HangfireActivator : JobActivator
	{
		private readonly IServiceProvider _serviceProvider;

		public HangfireActivator(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public override object ActivateJob(Type jobType)
		{
			return _serviceProvider.GetRequiredService(jobType);
		}
	}
}
