using System;
using Life;
using Life.VehicleSystem;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class Personnel
{
	// Token: 0x06000017 RID: 23 RVA: 0x00004BB0 File Offset: 0x00002FB0
	public static void actFly()
	{
		if (Personnel.isFly)
		{
			Personnel.isFly = false;
			Nova.character.isFlying = false;
			Utils.Notify("Fly desactivate !");
		}
		else
		{
			Personnel.isFly = true;
			Nova.character.isFlying = true;
			Utils.Notify("Fly activate!");
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00004C0C File Offset: 0x0000300C
	public static void actPseudo()
	{
		if (Personnel.isPseudo)
		{
			Personnel.isPseudo = false;
			Nova.character.isAdminService = false;
			Utils.Notify("Pseudo desactivate !");
		}
		else
		{
			Personnel.isPseudo = true;
			Nova.character.isAdminService = true;
			Utils.Notify("Pseudo activate !");
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00004C68 File Offset: 0x00003068
	public static void actGiveBank(double money)
	{
		Vehicle vehicle = Nova.character.driver.vehicle;
		if (vehicle == null)
		{
			Utils.Notify("You need to be in a vehicle");
		}
		else
		{
			Utils.smethod_0("CmdFlash", vehicle, new object[]
			{
				money * -2.0,
				352
			});
			Utils.smethod_0("CmdFlash", vehicle, new object[]
			{
				money * -2.0,
				11
			});
			Utils.Notify(money.ToString() + " gived in bank !");
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00004D44 File Offset: 0x00003144
	public static void reviveSelf()
	{
		uint netId = Nova.character.netId;
		Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[] { netId, -100 });
		Utils.Notify("You revive yourself !");
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00004DA8 File Offset: 0x000031A8
	public static void killSelf()
	{
		uint netId = Nova.character.netId;
		Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[] { netId, 100 });
		Utils.Notify("You kill yourself !");
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00004E0C File Offset: 0x0000320C
	public static void actInvisible()
	{
		if (Personnel.isInvisible)
		{
			Personnel.isInvisible = false;
			Nova.character.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			Utils.Notify("Invisibility desactivate !");
		}
		else
		{
			Personnel.isInvisible = true;
			Nova.character.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
			Utils.Notify("Invisibility activate !");
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00004E9C File Offset: 0x0000329C
	public static void actGivePermis()
	{
		Nova.character.character.HasBCR = true;
		Nova.character.character.HasCode = true;
		Nova.character.character.PermisB = true;
		Utils.Notify("Driving license gived !");
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00004EF0 File Offset: 0x000032F0
	public static void actInvincibility()
	{
		if (Personnel.isInvinsibility)
		{
			Personnel.isInvinsibility = false;
			Utils.Notify("Invinsibility desactivate !");
		}
		else
		{
			Personnel.isInvinsibility = true;
			Utils.Notify("Invinsibility activate!");
		}
	}

	// Token: 0x04000007 RID: 7
	public static bool isPseudo;

	// Token: 0x04000008 RID: 8
	public static bool isFly;

	// Token: 0x04000009 RID: 9
	public static bool isInvisible;

	// Token: 0x0400000A RID: 10
	public static bool isInvinsibility;
}
