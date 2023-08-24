using CRM.DataTypeObjects.Models;

namespace CRM.Business.ValidationRules
{
	public class LoginVal : GenericVal<LoginModel>
	{
		public LoginVal()
		{
			EntityNullCheck();
		}

	}
}
