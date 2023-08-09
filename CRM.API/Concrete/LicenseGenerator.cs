using System.Security.Cryptography;

namespace CRM.API.Concrete
{
	public static class LicenseGenerator
	{
		private const string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		private const int LicenseCodeLength = 12;

		public static string GenerateLicense()
		{
			using (var rng = RandomNumberGenerator.Create())
			{
				byte[] data = new byte[LicenseCodeLength];
				char[] result = new char[LicenseCodeLength];

				rng.GetBytes(data);

				for (int i = 0; i < LicenseCodeLength; i++)
				{
					int charIndex = data[i] % AllowedCharacters.Length;

					result[i] = AllowedCharacters[charIndex];
				}

				return new string(result);
			}
		}
	}
}