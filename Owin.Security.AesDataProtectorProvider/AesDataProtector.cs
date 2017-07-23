﻿using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Owin.Security.DataProtection;
using Owin.Security.AesDataProtectorProvider.CrypticProviders;

namespace Owin.Security.AesDataProtectorProvider
{
	internal class AesDataProtector : IDataProtector
	{
		private readonly ISha256Factory _sha256Factory;
		private readonly IAesFactory _aesFactory;

		private readonly byte[] _key;

		public AesDataProtector(ISha256Factory sha256Factory, IAesFactory aesFactory, string key)
		{
			_sha256Factory = sha256Factory;
			_aesFactory = aesFactory;

			using (var sha = _sha256Factory.Create())
				_key = sha.ComputeHash(Encoding.UTF8.GetBytes(key));
		}

		public byte[] Protect(byte[] data)
		{
			byte[] dataHash;
			byte[] dataHashLen;

			using (var sha = _sha256Factory.Create())
			{
				dataHash = sha.ComputeHash(data);
				dataHashLen = sha.ComputeHash(Encoding.UTF8.GetBytes(data.Length.ToString()));
			}

			using (var aesAlg = _aesFactory.Create())
			{
				aesAlg.Key = _key;
				aesAlg.GenerateIV();

				using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
				using (var msEncrypt = new MemoryStream())
				{
					msEncrypt.Write(aesAlg.IV, 0, 16);

					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					using (var bwEncrypt = new BinaryWriter(csEncrypt))
					{
						bwEncrypt.Write(dataHash);
						bwEncrypt.Write(dataHashLen);
						bwEncrypt.Write(data.Length);
						bwEncrypt.Write(data);
					}

					return msEncrypt.ToArray();
				}
			}
		}

		public byte[] Unprotect(byte[] protectedData)
		{
			using (var aesAlg = _aesFactory.Create())
			{
				aesAlg.Key = _key;

				using (var msDecrypt = new MemoryStream(protectedData))
				{
					var iv = new byte[16];
					msDecrypt.Read(iv, 0, 16);
					aesAlg.IV = iv;

					if (aesAlg.Key == null)
						throw new AesDataProtectorProviderException("Key is null");

					using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					using (var brDecrypt = new BinaryReader(csDecrypt))
					{
						var signature = brDecrypt.ReadBytes(32);
						var signatureLen = brDecrypt.ReadBytes(32);
						var len = brDecrypt.ReadInt32();
						byte[] data;
						byte[] dataHash;
						using (var sha = _sha256Factory.Create())
						{
							var lenHash = sha.ComputeHash(Encoding.UTF8.GetBytes(len.ToString()));
							if (len < 0 || !signatureLen.SequenceEqual(lenHash))
								throw new SecurityException("Data length integrity check failed");

							data = brDecrypt.ReadBytes(len);
							dataHash = sha.ComputeHash(data);
						}

						if (!dataHash.SequenceEqual(signature))
							throw new SecurityException("Signature does not match the computed hash");

						return data;
					}
				}
			}
		}
	}
}