﻿using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Owin.Security.DataProtection;

namespace AcspNet.Owin.Security.AesDataProtectorProvider
{

	internal class AesDataProtector : IDataProtector
	{
		private readonly byte[] _key;

		public AesDataProtector(string key)
		{
			using (var sha1 = new SHA256CryptoServiceProvider())
			{
				_key = sha1.ComputeHash(Encoding.UTF8.GetBytes(key));
			}
		}

		public byte[] Protect(byte[] data)
		{
			byte[] dataHash;
			using (var sha = new SHA256CryptoServiceProvider())
			{
				dataHash = sha.ComputeHash(data);
			}
			using (var aesAlg = new AesCryptoServiceProvider())
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
						bwEncrypt.Write(data.Length);
						bwEncrypt.Write(data);
					}
					var protectedData = msEncrypt.ToArray();
					return protectedData;
				}
			}
		}

		public byte[] Unprotect(byte[] protectedData)
		{
			using (var aesAlg = new AesCryptoServiceProvider())
			{
				aesAlg.Key = _key;
				using (var msDecrypt = new MemoryStream(protectedData))
				{
					var iv = new byte[16];
					msDecrypt.Read(iv, 0, 16);
					aesAlg.IV = iv;
					using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					using (var brDecrypt = new BinaryReader(csDecrypt))
					{
						var signature = brDecrypt.ReadBytes(32);
						var len = brDecrypt.ReadInt32();
						var data = brDecrypt.ReadBytes(len);
						byte[] dataHash;
						using (var sha = new SHA256CryptoServiceProvider())
						{
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