using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Represent AES factory
	/// </summary>
	public interface IAesFactory
	{
		/// <summary>
		/// Creates AES instance.
		/// </summary>
		/// <returns></returns>
		Aes Create();
	}
}