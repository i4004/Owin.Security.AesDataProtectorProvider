using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Provides AesCryptoServiceProvider factory
	/// </summary>
	public class AesCspFactory : IAesFactory
	{
		/// <summary>
		/// Creates AES CSP implementation instance.
		/// </summary>
		/// <returns></returns>
		public Aes Create()
		{
			return new AesCryptoServiceProvider();
		}
	}
}