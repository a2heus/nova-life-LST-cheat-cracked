using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Life;
using Life.CharacterSystem;
using Life.InventorySystem;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class Player
{
	// Token: 0x06000020 RID: 32 RVA: 0x00002981 File Offset: 0x00000D81
	public static void openSelectedPlayerInventory(Inventory inventory)
	{
		Nova.ui.OpenInventory(inventory);
		Utils.Notify("Inventory of the selected player opened !");
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00004F30 File Offset: 0x00003330
	public static void reviveSelectedPlayer(CharacterSetup selectedPlayer)
	{
		Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[] { selectedPlayer.netId, -100 });
		Utils.Notify("Selected player revived !");
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00004F8C File Offset: 0x0000338C
	public static void killSelectedPlayer(CharacterSetup selectedPlayer)
	{
		Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[] { selectedPlayer.netId, 100 });
		Utils.Notify("Selected player killed !");
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00004FE8 File Offset: 0x000033E8
	public static void tazeSelectedPlayer(CharacterSetup selectedPlayer)
	{
		Utils.smethod_0("CmdTaze", Nova.character.weaponSystem, new object[] { true, selectedPlayer.netId });
		Utils.Notify("Selected player tazed !");
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00005044 File Offset: 0x00003444
	public static void actInfiniteTaze(CharacterSetup selectedPlayer)
	{
		Player.Class2 @class = new Player.Class2();
		@class.uint_0 = selectedPlayer.netId;
		if (Player.infiniteTazedPlayer.Contains(@class.uint_0))
		{
			Player.infiniteTazedPlayer = Player.infiniteTazedPlayer.Where(new Func<uint, bool>(@class.method_0)).ToArray<uint>();
			Utils.Notify("Stop infinite taze of the selected player !");
		}
		else
		{
			Player.infiniteTazedPlayer.Append(@class.uint_0);
			Utils.Notify("Selected player tazed infinity !");
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000050D0 File Offset: 0x000034D0
	public static void tpTo(CharacterSetup selectedPlayer)
	{
		Vector3 position = selectedPlayer.gameObject.transform.position;
		Nova.character.transform.position = position;
	}

	// Token: 0x0400000B RID: 11
	public static uint[] infiniteTazedPlayer = new uint[0];

	// Token: 0x02000009 RID: 9
	[CompilerGenerated]
	private sealed class Class2
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000029AC File Offset: 0x00000DAC
		internal bool method_0(uint uint_1)
		{
			return uint_1 != this.uint_0;
		}

		// Token: 0x0400000C RID: 12
		public uint uint_0;
	}
}
