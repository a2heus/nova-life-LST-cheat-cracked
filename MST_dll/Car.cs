using System;
using Life;
using Life.InventorySystem;
using Life.VehicleSystem;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class Car
{
	// Token: 0x06000001 RID: 1 RVA: 0x00004458 File Offset: 0x00002858
	public static void openSelectedVehicleInventory(GameObject Vehicle)
	{
		VehicleInventory component = Vehicle.GetComponent<VehicleInventory>();
		Nova.ui.OpenInventory(component);
		Utils.Notify("Trunk inventory opened !");
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000448C File Offset: 0x0000288C
	public static void openSelectedEngineInventory(GameObject Vehicle)
	{
		EngineInventory component = Vehicle.GetComponent<EngineInventory>();
		Nova.ui.OpenInventory(component);
		Utils.Notify("Engine inventory open !");
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000044C0 File Offset: 0x000028C0
	public static void enterSelectedVehicle(GameObject Vehicle)
	{
		uint netId = Vehicle.GetComponent<Vehicle>().netId;
		if (Vehicle.GetComponent<Vehicle>().IsSeatOccupied(0))
		{
			Utils.smethod_0("CmdEnterInSeat", Nova.character.driver, new object[] { netId, 1 });
		}
		else
		{
			Utils.smethod_0("CmdEnterInSeat", Nova.character.driver, new object[] { netId, 0 });
		}
		Utils.smethod_0("UserCode_RpcUpdateSeat", Nova.character, null);
		Utils.Notify("You enter in a car !");
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00004590 File Offset: 0x00002990
	public static void actEngine()
	{
		Vehicle vehicle = Nova.character.driver.vehicle;
		Utils.smethod_0("OnEngineStart", vehicle, new object[] { true, true });
		Utils.Notify("Engine start !");
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000045F4 File Offset: 0x000029F4
	public static void actNOS()
	{
		if (Car.bool_0)
		{
			Car.bool_0 = false;
			Nova.character.driver.vehicle.newController.useNOS = false;
			Utils.Notify("Vehicle NOS desactivate !");
		}
		else
		{
			Car.bool_0 = true;
			Nova.character.driver.vehicle.newController.useNOS = true;
			Utils.Notify("Vehicle NOS activate !");
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00004670 File Offset: 0x00002A70
	public static void actTurbo()
	{
		if (Car.bool_1)
		{
			Car.bool_1 = false;
			Nova.character.driver.vehicle.newController.useTurbo = false;
			Utils.Notify("Vehicle turbo desactivate !");
		}
		else
		{
			Car.bool_1 = true;
			Nova.character.driver.vehicle.newController.useTurbo = true;
			Utils.Notify("Vehicle turbo activate !");
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000046EC File Offset: 0x00002AEC
	public static void actDrift()
	{
		RCC_CarControllerV3 newController = Nova.character.driver.vehicle.newController;
		if (Car.bool_2)
		{
			Car.bool_2 = false;
			newController.ESP = true;
			newController.tractionHelper = true;
			newController.poweredWheels = 4;
			Utils.Notify("Vehicle drift mode desactivate !");
		}
		else
		{
			Car.bool_2 = true;
			newController.ESP = false;
			newController.tractionHelper = false;
			newController.poweredWheels = 2;
			Utils.Notify("Vehicle drift mode activate !");
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00004798 File Offset: 0x00002B98
	public static void actCollisions()
	{
		if (Car.bool_3)
		{
			Nova.character.driver.vehicle.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
			Car.bool_3 = false;
			Utils.Notify("Vehicle collisions activate !");
		}
		else
		{
			Nova.character.driver.vehicle.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
			Car.bool_3 = false;
			Utils.Notify("Vehicle collisions desactivate !");
		}
	}

	// Token: 0x04000001 RID: 1
	internal static bool bool_0;

	// Token: 0x04000002 RID: 2
	internal static bool bool_1;

	// Token: 0x04000003 RID: 3
	internal static bool bool_2;

	// Token: 0x04000004 RID: 4
	internal static bool bool_3;
}
