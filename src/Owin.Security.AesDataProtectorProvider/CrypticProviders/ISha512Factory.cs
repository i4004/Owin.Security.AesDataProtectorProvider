using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Represent SHA512 factory
	/// </summary>
	public interface ISha512Factory
	{
		/// <summary>
		/// Creates SHA512 instance.
		/// </summary>
		/// <returns></returns>
		SHA512 Create();
	}
}