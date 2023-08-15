namespace CRM.WebUI.Middleware
{
	public class NotAuthorizedException : CodedException
	{
		public NotAuthorizedException(string message) : base(message)
		{
			Code = 403;
		}
	}
	public class NotFoundException : CodedException
	{
		public NotFoundException(string message) : base(message)
		{
			Code = 404;
		}
	}
}
