using CRM.API.Concrete;
using CRM.Entity.Concrete;
using Hangfire;
using static System.Formats.Asn1.AsnWriter;

namespace CRM.WebUI.PackageConf
{
    public class HangfireJobScheduler
    {
        private static HangfireJobScheduler _instance;

        private readonly WebApplication _webApplication;

        private HangfireJobScheduler(WebApplication webApplication)
        {
            _webApplication = webApplication;
			_webApplication.UseHangfireDashboard("/hangfire", new DashboardOptions{});
		}

        public static HangfireJobScheduler GetInstance(WebApplication webApplication)
        {
            if (_instance == null)
            {
                _instance = new HangfireJobScheduler(webApplication);
            }
            return _instance;
        }

        [Obsolete]
        public void ScheduleJobs()
        {
            RecurringJob.AddOrUpdate<IInvoiceGenerationService>(
            x => x.GenerateInvoices(),
            Cron.Minutely);
        }
    }
}
