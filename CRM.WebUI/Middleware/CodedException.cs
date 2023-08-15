namespace CRM.WebUI.Middleware
{
	public class CodedException : Exception
	{
		public int Code { get; set; }
		public CodedException(string message) : base(message)
		{
		}
	}
}
