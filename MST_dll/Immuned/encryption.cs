using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Immuned
{
	// Token: 0x0200001C RID: 28
	public static class encryption
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00008E20 File Offset: 0x00007220
		public static string byte_arr_to_str(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008E68 File Offset: 0x00007268
		public static byte[] str_to_byte_arr(string hex)
		{
			byte[] array2;
			try
			{
				int length = hex.Length;
				byte[] array = new byte[length / 2];
				for (int i = 0; i < length; i += 2)
				{
					array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
				}
				array2 = array;
			}
			catch
			{
				Debug.LogError("Session has ended. Please reconnect.");
				Application.Quit();
				array2 = null;
			}
			return array2;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008ED4 File Offset: 0x000072D4
		public static string encrypt_string(string plain_text, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						byte[] bytes = Encoding.Default.GetBytes(plain_text);
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.FlushFinalBlock();
						byte[] array = memoryStream.ToArray();
						text = encryption.byte_arr_to_str(array);
					}
				}
			}
			return text;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008F90 File Offset: 0x00007390
		public static string decrypt_string(string cipher_text, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ICryptoTransform cryptoTransform = aes.CreateDecryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						byte[] array = encryption.str_to_byte_arr(cipher_text);
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.FlushFinalBlock();
						byte[] array2 = memoryStream.ToArray();
						@string = Encoding.Default.GetString(array2, 0, array2.Length);
					}
				}
			}
			return @string;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009050 File Offset: 0x00007450
		public static string iv_key()
		{
			return Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-", StringComparison.Ordinal));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002EA9 File Offset: 0x000012A9
		public static string sha256(string r)
		{
			return encryption.byte_arr_to_str(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(r)));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009094 File Offset: 0x00007494
		public static string encrypt(string message, string enc_key, string iv)
		{
			byte[] bytes = Encoding.Default.GetBytes(encryption.sha256(enc_key).Substring(0, 32));
			byte[] bytes2 = Encoding.Default.GetBytes(encryption.sha256(iv).Substring(0, 16));
			return encryption.encrypt_string(message, bytes, bytes2);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000090E0 File Offset: 0x000074E0
		public static string decrypt(string message, string enc_key, string iv)
		{
			byte[] bytes = Encoding.Default.GetBytes(encryption.sha256(enc_key).Substring(0, 32));
			byte[] bytes2 = Encoding.Default.GetBytes(encryption.sha256(iv).Substring(0, 16));
			return encryption.decrypt_string(message, bytes, bytes2);
		}
	}
}
