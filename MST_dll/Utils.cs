using System;
using System.Net;
using System.Reflection;
using System.Text;
using Life;
using MST_dll;
using UnityEngine;

// Token: 0x0200000B RID: 11
public static class Utils
{
	// Token: 0x06000037 RID: 55 RVA: 0x00002A3A File Offset: 0x00000E3A
	public static void Notify(string message)
	{
		Nova.Notify("LST-Cheat", message, 0, 2f);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002A54 File Offset: 0x00000E54
	internal static void smethod_0(string string_0, object object_0, params object[] args)
	{
		object_0.GetType().GetMethod(string_0, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(object_0, args);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000054D4 File Offset: 0x000038D4
	internal static void smethod_1(string string_0, string string_1)
	{
		KeyAuthManager.KeyAuthApp.login(string_0, string_1);
		if (KeyAuthManager.KeyAuthApp.response.success)
		{
			Utils.bool_0 = true;
			Main.bool_11 = false;
			Utils.Notify("Login successful !");
		}
		else
		{
			Utils.Notify(KeyAuthManager.KeyAuthApp.response.message ?? "");
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00005544 File Offset: 0x00003944
	internal static void smethod_2(string string_0, string string_1)
	{
		WebClient webClient = new WebClient();
		webClient.Headers.Add("Content-Type", "application/json");
		string text = "{\"content\": \"" + string_1 + "\"}";
		webClient.UploadData(string_0, Encoding.UTF8.GetBytes(text));
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000055A8 File Offset: 0x000039A8
	internal static bool smethod_3(Camera camera_0, GameObject gameObject_0)
	{
		Vector3 vector = camera_0.WorldToViewportPoint(gameObject_0.transform.position);
		RaycastHit raycastHit;
		return vector.z > 0f && vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f && Physics.Raycast(camera_0.transform.position, gameObject_0.transform.position - camera_0.transform.position, ref raycastHit) && raycastHit.transform == gameObject_0.transform;
	}

	// Token: 0x0400000F RID: 15
	internal static bool bool_0;
}
