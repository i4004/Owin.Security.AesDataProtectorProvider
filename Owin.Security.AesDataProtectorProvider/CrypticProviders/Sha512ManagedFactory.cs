using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Provides SHA512 managed factory
	/// </summary>
	public class Sha512ManagedFactory : ISha512Factory
	{
		/// <summary>
		/// Creates SHA512 managed implementation instance.
		/// </summary>
		/// <returns></returns>
		public SHA512 Create()
		{
			return SHA512.Create();
		}
	}
}