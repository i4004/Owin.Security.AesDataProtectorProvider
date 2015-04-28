using System;
using System.Text;
using Microsoft.Owin.Security.DataProtection;
using Owin.Security.AesDataProtectorProvider.CrypticProviders;

namespace Owin.Security.AesDataProtectorProvider
{
	/// <summary>
	/// Provider AES DataProtectorProvider
	/// </summary>
	public class AesDataProtectorProvider : IDataProtectionProvider
	{
		private readonly ISha512Factory _sha512Factory;
		private readonly ISha256Factory _sha256Factory;
		private readonly IAesFactory _aesFactory;

		private string _key;

		/// <summary>
		/// Initializes a new instance of the <see cref="AesDataProtectorProvider" /> class.
		/// </summary>
		/// <param name="sha512Factory">The SHA512 factory.</param>
		/// <param name="sha256Factory">The SHA256 factory.</param>
		/// <param name="aesFactory">The AES factory.</param>
		/// <param name="key">The key.</param>
		public AesDataProtectorProvider(ISha512Factory sha512Factory, ISha256Factory sha256Factory, IAesFactory aesFactory, string key = null)
		{
			_sha512Factory = sha512Factory;
			_sha256Factory = sha256Factory;
			_aesFactory = aesFactory;
			_key = key;
		}

		private string SeedHash
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_key))
					_key = HashString(Environment.MachineName);

				return _key;
			}
		}
		private string HashString(string value)
		{
			return HexStringFromBytes(_sha512Factory.Create().ComputeHash(Encoding.ASCII.GetBytes(value))).ToUpper();
		}

		/// <summary>
		/// Returns a new instance of IDataProtection for the provider.
		/// </summary>
		/// <param name="purposes">Additional entropy used to ensure protected data may only be unprotected for the correct purposes.</param>
		/// <returns>
		/// An instance of a data protection service
		/// </returns>
		public IDataProtector Create(params string[] purposes)
		{
			return new AesDataProtector(_sha256Factory, _aesFactory, SeedHash);
		}

		/// <summary>
		/// Convert an array of bytes to a string of hex digits
		/// </summary>
		/// <param name="bytes">array of bytes</param>
		/// <returns>String of hex digits</returns>
		public static string HexStringFromBytes(byte[] bytes)
		{
			var sb = new StringBuilder();

			foreach (var b in bytes)
				sb.Append(b.ToString("x2"));

			return sb.ToString();
		}
	}
}