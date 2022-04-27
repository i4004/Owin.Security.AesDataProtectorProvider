using System.Security.Cryptography;

namespace Owin.Security.AesDataProtectorProvider.CrypticProviders
{
	/// <summary>
	/// Represent SHA256 factory
	/// </summary>
	public interface ISha256Factory
	{
		/// <summary>
		/// Creates SHA256 instance.
		/// </summary>
		/// <returns></returns>
		SHA256 Create();
	}
}