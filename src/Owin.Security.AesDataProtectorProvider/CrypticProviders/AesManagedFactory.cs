using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Provides factory
	/// </summary>
	public class AesManagedFactory : IAesFactory
	{
		/// <summary>
		/// Creates AES managed implementation instance.
		/// </summary>
		/// <returns></returns>
		public Aes Create()
		{
			return new AesManaged();
		}
	}
}