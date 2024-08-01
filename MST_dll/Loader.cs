using System;
using UnityEngine;

namespace MST_dll
{
	// Token: 0x0200000D RID: 13
	public class Loader
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002ABC File Offset: 0x00000EBC
		public static void Init()
		{
			Loader.gameObject_0 = new GameObject();
			Loader.gameObject_0.AddComponent<Main>();
			global::UnityEngine.Object.DontDestroyOnLoad(Loader.gameObject_0);
		}

		// Token: 0x04000012 RID: 18
		private static GameObject gameObject_0;
	}
}
