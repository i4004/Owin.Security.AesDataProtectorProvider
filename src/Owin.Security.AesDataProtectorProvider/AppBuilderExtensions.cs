using System;
using Microsoft.Owin.Security.DataProtection;
using Owin.Security.AesDataProtectorProvider.CrypticProviders;

namespace Owin.Security.AesDataProtectorProvider
{
	/// <summary>
	/// AesDataProtectorProvider IAppBuilder extensions
	/// </summary>
	public static class AppBuilderExtensions
	{
		private static ISha512Factory _sha512Factory;
		private static ISha256Factory _sha256Factory;
		private static IAesFactory _aesFactory;

		/// <summary>
		/// Gets or sets the SHA512 factory instance.
		/// </summary>
		/// <value>
		/// The SHA512 factory instance.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static ISha512Factory Sha512Factory
		{
			get => _sha512Factory ?? (_sha512Factory = new Sha512ManagedFactory());

			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_sha512Factory = value;
			}
		}

		/// <summary>
		/// Gets or sets the SHA256 factory instance.
		/// </summary>
		/// <value>
		/// The SHA256 factory instance.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static ISha256Factory Sha256Factory
		{
			get => _sha256Factory ?? (_sha256Factory = new Sha256ManagedFactory());

			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_sha256Factory = value;
			}
		}

		/// <summary>
		/// Gets or sets the AES factory instance.
		/// </summary>
		/// <value>
		/// The AES factory instance.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static IAesFactory AesFactory
		{
			get => _aesFactory ?? (_aesFactory = new AesManagedFactory());

			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_aesFactory = value;
			}
		}

		/// <summary>
		/// Uses the AES data protector provider as data provider.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="key">The key for encryption/decryption, to use Environment.MachineName as a key should be null.</param>
		/// <param name="cspProvilder">If set to <c>true</c> then CSP FIPS-compliant providers will be used instead of managed providers.</param>
		public static void UseAesDataProtectorProvider(this IAppBuilder builder, string key = null, bool cspProvilder = false)
		{
			if (cspProvilder)
			{
				Sha256Factory = new Sha256CspFactory();
				Sha512Factory = new Sha512CspFactory();
				AesFactory = new AesCspFactory();
			}

			builder.SetDataProtectionProvider(new AesDataProtectorProvider(Sha512Factory, Sha256Factory, AesFactory, key));
		}
	}
}