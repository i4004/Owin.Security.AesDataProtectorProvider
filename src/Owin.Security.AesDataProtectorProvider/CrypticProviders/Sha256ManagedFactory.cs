using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Provides SHA256Managed factory
	/// </summary>
	public class Sha256ManagedFactory : ISha256Factory
	{
		/// <summary>
		/// Creates SHA256 managed implementation instance.
		/// </summary>
		/// <returns></returns>
		public SHA256 Create()
		{
			return new SHA256Managed();
		}
	}
}