using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Provides SHA512CryptoServiceProvider factory
	/// </summary>
	public class Sha512CspFactory : ISha512Factory
	{
		/// <summary>
		/// Creates SHA512 CSP implementation instance.
		/// </summary>
		/// <returns></returns>
		public SHA512 Create()
		{
			return new SHA512CryptoServiceProvider();
		}
	}
}