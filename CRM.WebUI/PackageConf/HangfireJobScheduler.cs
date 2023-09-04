using Hangfire;
using CRM.Business.Abstract; // Adjust this namespace
using CRM.Entity.Concrete;
using Microsoft.Extensions.DependencyInjection; // Add this namespace
using CRM.API.Concrete;

namespace CRM.WebUI.PackageConf
{
	public class HangfireJobScheduler
	{
		private readonly IServiceProvider _serviceProvider;

		public HangfireJobScheduler(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		[Obsolete]
		public void ScheduleJobs()
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var invoiceGenerationService = scope.ServiceProvider.GetRequiredService<IInvoiceGenerationService>();

				RecurringJob.AddOrUpdate<IInvoiceGenerationService>(
					x => x.GenerateInvoices(),
					Cron.Minutely);
			}
		}

		public void AddJob(Order order)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var invoiceGenerationService = scope.ServiceProvider.GetRequiredService<IInvoiceGenerationService>();

				RecurringJob.AddOrUpdate(
					order.Id.ToString(),
					() => invoiceGenerationService.SetMembershipPaymentStatus(order),
					Cron.Minutely);
			}
		}
	}
}
