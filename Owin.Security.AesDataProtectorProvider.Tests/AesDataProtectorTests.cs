using System;
using Microsoft.Owin.Security.DataProtection;
using NUnit.Framework;
using Owin.Security.AesDataProtectorProvider.CrypticProviders;
using Simplify.Extensions;

namespace Owin.Security.AesDataProtectorProvider.Tests
{
	[TestFixture]
	public class AesDataProtectorTests
	{
		private const string Key = "abcdefg-keyabcx";

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

		[Test]
		public void Unprotect_CookieWhichCausesPaddingError_NoExceptionsThrown()
		{
			// Assign

			var authCookie = "eLnczc+4i2AwIDknRD90lwEg/qVRgh9yi2oGd9QF8mO/UpgYJyQPQN885qx3tshalkwF5UGRqyHgO6e2t0UHxLBqdO/QWzSNFrbxMDV7mlRfCKF9tfv+Oi+iwNwWAQe/AAWzpZMU/jt6aKFEvDlu5lTx9NMQPANZsTIaRAHD8guje8ltGFJeSDQ8FgyMnzZjBMw8FyiiKYbX5ToFSVoPgsf/6bxlev2QiuYnSkqVsIwMwiCFdUC2fFuj2MouFmipi3dNQDxt+Ihjxff1aBNfI0H2AzDRFtpEyxDSdq/kcMXipkZWSHsfJQaPL9HbJivksQnvg2IUiZWOVn8FOABx0IbjJFzWCQWF1KrdYfCjsLPv/N4LZVJqvyolxHiBBkmM6OrM4WI1+yPndLhoVgUPsg==";
			var protectedData = Convert.FromBase64String(authCookie);

			// Act

			_protector.Unprotect(protectedData);
		}
	}
}