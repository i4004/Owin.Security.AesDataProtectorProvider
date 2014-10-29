using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace AcspNet.Owin.Security.AesDataProtectorProvider
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
		/// <param name="key">The key.</param>
		public static void UseAesDataProtectorProvider(this IAppBuilder builder, string key = null)
		{
			builder.SetDataProtectionProvider(new AesDataProtectorProvider(key));
		}	
	}
}