using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CRM.Business.ValidationRules
{
	public abstract class GenericVal<T> : AbstractValidator<T> where T : class
	{
		public GenericVal() { }
		public void EntityNullCheck()
		{
			foreach (var property in typeof(T).GetProperties())
			{
				if (property.GetCustomAttribute<ForeignKeyAttribute>() != null)
					continue;

				bool isCollection = property.PropertyType.IsGenericType;
				isCollection = isCollection && property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>);
				if (isCollection)
					continue;

				RuleFor(x => property.GetValue(x))
					.NotNull()
					.WithMessage($"{property.Name} can't be null.").OverridePropertyName(typeof(T).Name + "." + property.Name); // For returning something like User.Name
			}
		}
		public bool HasNumbers(string val)
		{
			bool containsNumbers = Regex.IsMatch(val, @"\d");
			return containsNumbers;
		}
		public bool HasNoNumbers(string val)
		{
			bool containsNumbers = Regex.IsMatch(val, @"\d");
			return !containsNumbers;
		}
		public bool HasSmallLetters(string val)
		{
			bool containsSmallLetters = Regex.IsMatch(val, @"[a-z]");
			return containsSmallLetters;
		}
		public bool HasCapitalLetters(string val)
		{
			bool containsCapitalLetters = Regex.IsMatch(val, @"[A-Z]");
			return containsCapitalLetters;
		}
		public bool HasNoSpecialCharacter(string val)
		{
			bool containsSpecialCharacter = Regex.IsMatch(val, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
			return !containsSpecialCharacter;
		}
		public bool HasSpecialCharacter(string val)
		{
			bool containsSpecialCharacter = Regex.IsMatch(val, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
			return containsSpecialCharacter;
		}
	}
}
