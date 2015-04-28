using Microsoft.Owin.Security.DataProtection;

namespace Owin.Security.AesDataProtectorProvider
{
	/// <summary>
	/// AesDataProtectorProvider IAppBuilder extensions
	/// </summary>
	public static class AppBuilderExtensions
	{
		/// <summary>
		/// Uses the AES data protector provider as data provider.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="key">The key for encryption/decryption, to use Environment.MachineName as a key should be null.</param>
		public static void UseAesDataProtectorProvider(this IAppBuilder builder, string key = null)
		{
			builder.SetDataProtectionProvider(new AesDataProtectorProvider(key));
		}
	}
}