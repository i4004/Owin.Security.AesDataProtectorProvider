using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Provides SHA256CryptoServiceProvider factory
	/// </summary>
	public class Sha256CspFactory : ISha256Factory
	{
		/// <summary>
		/// Creates SHA256 CSP instance.
		/// </summary>
		/// <returns></returns>
		public SHA256 Create()
		{
			return new SHA256CryptoServiceProvider();
		}
	}
}