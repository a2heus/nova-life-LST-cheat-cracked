using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Life;
using Life.AreaSystem;
using Life.InventorySystem;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class Object
{
	// Token: 0x0600000F RID: 15 RVA: 0x000049EC File Offset: 0x00002DEC
	public static void openClosestChestInventory()
	{
		global::Object.Class0 @class = new global::Object.Class0();
		@class.vector3_0 = Nova.character.transform.position;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Interaction/PlaceableInventory");
		GameObject gameObject = array.OrderBy(new Func<GameObject, float>(@class.method_0)).First<GameObject>();
		PlaceableInventory component = gameObject.GetComponent<PlaceableInventory>();
		Nova.ui.OpenInventory(component);
		Nova.ui.OpenPlayerInventory();
		Utils.Notify("Chest open/close !");
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00004A78 File Offset: 0x00002E78
	public static void unlockClsDoor()
	{
		global::Object.Class1 @class = new global::Object.Class1();
		@class.vector3_0 = Nova.character.transform.position;
		KeyValuePair<string, InteractableDoor> keyValuePair = Nova.man.doors.OrderBy(new Func<KeyValuePair<string, InteractableDoor>, float>(@class.method_0)).First<KeyValuePair<string, InteractableDoor>>();
		Utils.smethod_0("CmdForceDoor", Nova.character.weaponSystem, new object[] { keyValuePair.Key });
		Utils.Notify("Door unlock/lock !");
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00004B08 File Offset: 0x00002F08
	public static void unlockAllDoors()
	{
		foreach (KeyValuePair<string, InteractableDoor> keyValuePair in Nova.man.doors)
		{
			if (keyValuePair.Value.isLocked)
			{
				Utils.smethod_0("CmdForceDoor", Nova.character.weaponSystem, new object[] { keyValuePair.Key });
			}
		}
		Utils.Notify("All door unlocked !");
	}

	// Token: 0x02000005 RID: 5
	[CompilerGenerated]
	private sealed class Class0
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000293B File Offset: 0x00000D3B
		internal float method_0(GameObject gameObject_0)
		{
			return Vector3.Distance(gameObject_0.transform.position, this.vector3_0);
		}

		// Token: 0x04000005 RID: 5
		public Vector3 vector3_0;
	}

	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	private sealed class Class1
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002959 File Offset: 0x00000D59
		internal float method_0(KeyValuePair<string, InteractableDoor> keyValuePair_0)
		{
			return Vector3.Distance(keyValuePair_0.Value.gameObject.transform.position, this.vector3_0);
		}

		// Token: 0x04000006 RID: 6
		public Vector3 vector3_0;
	}
}
