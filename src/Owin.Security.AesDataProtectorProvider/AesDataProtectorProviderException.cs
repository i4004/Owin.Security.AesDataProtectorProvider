using System;

namespace Owin.Security.AesDataProtectorProvider
{
	/// <summary>
	/// Provides AesDataProtectorProviderException exception
	/// </summary>
	public class AesDataProtectorProviderException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AesDataProtectorProviderException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AesDataProtectorProviderException(string message)
			: base(message)
		{
		}
	}
}