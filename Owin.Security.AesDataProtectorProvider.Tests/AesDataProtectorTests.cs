using Microsoft.Owin.Security.DataProtection;
using NUnit.Framework;
using Owin.Security.AesDataProtectorProvider.CrypticProviders;
using Simplify.Extensions;

namespace Owin.Security.AesDataProtectorProvider.Tests
{
	[TestFixture]
	public class AesDataProtectorTests
	{
		private const string Key = "Test";

		private AesDataProtectorProvider _protectorProvider;
		private IDataProtector _protector;
		
		[SetUp]
		public void Initialize()
		{
			_protectorProvider = new AesDataProtectorProvider(new Sha512ManagedFactory(), new Sha256ManagedFactory(),
				new AesManagedFactory(), Key);

			_protector = _protectorProvider.Create();
		}

		[Test]
		public void ProtectUnprotect_TestString_SourceStringEqualProtectedThenUnprotected()
		{
			// Assign

			const string source = "Test data";
			var data = source.ToBytesArray();

			// Act

			var protectedData = _protector.Protect(data);
			var unprotectedData = _protector.Unprotect(protectedData);

			// Assert
			Assert.AreEqual(data, unprotectedData);
		}
	}
}