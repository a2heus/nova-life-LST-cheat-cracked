using System;
using System.IO;
using Immuned;
using UnityEngine;

namespace MST_dll
{
	// Token: 0x0200000C RID: 12
	public class KeyAuthManager : MonoBehaviour
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00005694 File Offset: 0x00003A94
		public static DateTime UnixTimeToDateTime(long unixtime)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
			dateTime = dateTime.AddSeconds((double)unixtime).ToLocalTime();
			return dateTime;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A7B File Offset: 0x00000E7B
		private void method_0()
		{
			KeyAuthManager.KeyAuthApp.webhook("WebhookID", "param", "", "");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005700 File Offset: 0x00003B00
		private void method_1()
		{
			byte[] array = KeyAuthManager.KeyAuthApp.download("fileID");
			File.WriteAllBytes("PathOfYourChoosing", array);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005730 File Offset: 0x00003B30
		private void method_2()
		{
			if (KeyAuthManager.KeyAuthApp.checkblack())
			{
				Debug.Log("User is blacklisted");
				Application.Quit();
			}
			else
			{
				Debug.Log("User is not blacklisted");
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005764 File Offset: 0x00003B64
		private void method_3()
		{
			KeyAuthManager.KeyAuthApp.check();
			if (KeyAuthManager.KeyAuthApp.response.success)
			{
				Debug.Log("Session is valid");
			}
			else
			{
				Debug.Log("Session is not valid");
			}
		}

		// Token: 0x04000010 RID: 16
		[Header("Login Section")]
		public static bool loggedIn;

		// Token: 0x04000011 RID: 17
		public static api KeyAuthApp = new api("cracked", "3ncZdOCvhK", "ab1c6b1944e30baf36d9bb05e0f0bb3a021fe0231197c403bdea7cf9603cd9e9", "1.0");
	}
}
