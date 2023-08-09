using CRM.Entity.Concrete;
using FluentValidation;


namespace CRM.Business.ValidationRules
{
	public class RegisterVal : GenericVal<User>
	{
		public RegisterVal()
		{
			EntityNullCheck();
			RuleFor(x => x.Name).MinimumLength(8).WithMessage("Please enter at least eight character")
									.Must(HasNoSpecialCharacter).WithMessage("Name cannot contain special characters.")
									.Must(HasNoNumbers).WithMessage("Name cannot contain numbers.").OverridePropertyName("User.Name");

			RuleFor(x => x.Password).MinimumLength(8).WithMessage("Please enter at least eight character")
										.Must(HasNumbers).WithMessage("Password should contain at least one number")
										.Must(HasSpecialCharacter).WithMessage("Password should contain at least one special character")
										.Must(HasSmallLetters).WithMessage("Password should contain at least one small letter")
										.Must(HasCapitalLetters).WithMessage("Password should contain at least one capital letter").OverridePropertyName("User.Password");
		}

	}
}

