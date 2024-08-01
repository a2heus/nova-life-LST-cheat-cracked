using System;
using System.Runtime.CompilerServices;
using CMF;
using Life;
using Life.CharacterSystem;
using Life.InventorySystem;
using UnityEngine;

namespace MST_dll
{
	// Token: 0x0200000E RID: 14
	public class Main : MonoBehaviour
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000057A4 File Offset: 0x00003BA4
		public void Update()
		{
			if (Input.GetKeyDown(292))
			{
				if (Utils.bool_0)
				{
					this.bool_0 = !this.bool_0;
					if (!this.bool_0)
					{
						this.bool_1 = false;
						this.bool_2 = false;
						this.bool_3 = false;
						this.bool_4 = false;
						this.bool_10 = false;
						this.bool_5 = false;
						this.bool_6 = false;
						this.bool_7 = false;
						this.bool_8 = false;
						this.bool_12 = false;
						this.bool_9 = false;
						this.bool_13 = false;
					}
				}
				else
				{
					Main.bool_11 = true;
				}
			}
			if (Personnel.isInvinsibility)
			{
				int networkhealth = Nova.character.Networkhealth;
				if (networkhealth > 100)
				{
					Utils.smethod_0("CmdDamagePlayer", Nova.character.weaponSystem, new object[]
					{
						Nova.character.netId,
						networkhealth - 100
					});
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005918 File Offset: 0x00003D18
		public void Start()
		{
			try
			{
				KeyAuthManager.KeyAuthApp.init();
			}
			catch (Exception ex)
			{
				string text = "Login error : ";
				Exception ex2 = ex;
				Utils.Notify(text + ((ex2 != null) ? ex2.ToString() : null));
			}
			if (KeyAuthManager.KeyAuthApp.response.success)
			{
				Utils.Notify("Press F11 to login !");
			}
			else
			{
				Utils.Notify("Login error !");
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005990 File Offset: 0x00003D90
		public void OnGUI()
		{
			if (this.bool_0)
			{
				this.rect_0 = GUI.Window(0, this.rect_0, new GUI.WindowFunction(this.method_0), "LST_Cheat");
			}
			if (this.bool_1)
			{
				this.rect_1 = GUI.Window(1, this.rect_1, new GUI.WindowFunction(this.method_1), "Self");
			}
			if (this.bool_2)
			{
				this.rect_2 = GUI.Window(2, this.rect_2, new GUI.WindowFunction(this.method_2), "Selected/Controled car");
			}
			if (this.bool_3)
			{
				this.rect_3 = GUI.Window(3, this.rect_3, new GUI.WindowFunction(this.method_3), "Selected Player");
			}
			if (this.bool_4)
			{
				this.rect_4 = GUI.Window(4, this.rect_4, new GUI.WindowFunction(this.method_4), "Object");
			}
			if (this.bool_10)
			{
				this.rect_10 = GUI.Window(6, this.rect_10, new GUI.WindowFunction(this.DrawClsPlayer), "Closest Players");
			}
			if (Main.bool_11)
			{
				this.rect_11 = GUI.Window(6, this.rect_11, new GUI.WindowFunction(this.method_10), "Login");
			}
			if (this.bool_5)
			{
				this.rect_5 = GUI.Window(8, this.rect_5, new GUI.WindowFunction(this.method_5), "Misc");
			}
			if (this.bool_6)
			{
				this.rect_6 = GUI.Window(9, this.rect_6, new GUI.WindowFunction(this.method_6), "Area");
			}
			if (this.bool_7)
			{
				this.rect_7 = GUI.Window(10, this.rect_7, new GUI.WindowFunction(this.method_7), "UI");
			}
			if (this.bool_8)
			{
				this.rect_8 = GUI.Window(11, this.rect_8, new GUI.WindowFunction(this.method_8), "Destroyer");
			}
			if (this.bool_12 && !this.bool_2)
			{
				this.ray_0 = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(this.ray_0, ref this.raycastHit_0, 1000f))
				{
					if (Input.GetMouseButtonDown(0))
					{
						if (this.raycastHit_0.transform.gameObject.tag == "Vehicle")
						{
							this.gameObject_0 = this.raycastHit_0.transform.gameObject;
							this.bool_2 = true;
							RCC_CarControllerV3 component = this.gameObject_0.GetComponent<RCC_CarControllerV3>();
							this.string_5 = "bug";
							this.string_6 = "bug";
							this.string_7 = component.MaxSpeed.ToString();
							this.string_10 = component.gearShiftingDelay.ToString();
							this.string_9 = component.gearShiftingThreshold.ToString();
							this.string_8 = this.gameObject_0.GetComponent<Rigidbody>().mass.ToString();
							this.string_11 = component.poweredWheels.ToString();
						}
						if (this.raycastHit_0.transform.gameObject.tag == "Interaction/FakeVehicle")
						{
							int vehicleDbId = this.raycastHit_0.transform.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<FakeVehicle>().vehicleDbId;
							Utils.smethod_0("Unfake", Nova.character.interaction, new object[] { vehicleDbId });
							this.gameObject_0 = this.raycastHit_0.transform.gameObject;
							this.bool_2 = true;
						}
					}
					if (this.raycastHit_0.transform.gameObject.tag == "Vehicle" || this.raycastHit_0.transform.gameObject.tag == "Interaction/FakeVehicle")
					{
						GUI.Label(new Rect(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y, 230f, 40f), "Click to select the vehicle");
					}
					else
					{
						GUI.Label(new Rect(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y, 230f, 40f), "Please select a vehicle");
					}
				}
			}
			if (this.bool_13)
			{
				this.ray_0 = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(this.ray_0, ref this.raycastHit_0, 1000f))
				{
					if (Input.GetMouseButtonDown(0))
					{
						PlaceableInventory component2 = this.raycastHit_0.transform.gameObject.GetComponent<PlaceableInventory>();
						Nova.ui.OpenInventory(component2);
						this.bool_13 = false;
					}
					if (this.raycastHit_0.transform.gameObject.tag == "Interaction/PlaceableInventory")
					{
						GUI.Label(new Rect(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y, 230f, 40f), "Click to open the chest");
					}
					else
					{
						GUI.Label(new Rect(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y, 230f, 40f), "Please select a chest");
					}
				}
			}
			if (this.bool_9)
			{
				this.rect_9 = GUI.Window(12, this.rect_9, new GUI.WindowFunction(this.method_9), "Custom current car");
			}
			if (this.bool_14)
			{
				Nova.closestPlayers.ForEach(new Action<CharacterSetup>(this.method_12));
			}
			if (this.bool_15)
			{
				GUI.Label(new Rect((float)(Screen.width / 2 - 100), 0f, 200f, 20f), "AimBot activate, Use RIGHT MOUSE to aim");
				Nova.closestPlayers.ForEach(new Action<CharacterSetup>(this.method_13));
				if (Input.GetMouseButton(1))
				{
					CameraController component3 = Nova.character.localCamera.cameraDistanceRaycaster.gameObject.GetComponent<CameraController>();
					component3.RotateTowardPosition(this.vector3_0, this.float_1);
					Render.DrawString(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), "AIM", Color.red, true);
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00006158 File Offset: 0x00004558
		private void method_0(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Self"))
			{
				this.bool_1 = !this.bool_1;
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Select Vehicle"))
			{
				this.bool_12 = !this.bool_12;
				this.bool_2 = false;
			}
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Players"))
			{
				this.bool_10 = !this.bool_10;
				this.bool_3 = false;
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "Object"))
			{
				this.bool_4 = !this.bool_4;
			}
			if (GUI.Button(new Rect(10f, 190f, 230f, 30f), "Misc"))
			{
				this.bool_5 = !this.bool_5;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000062D8 File Offset: 0x000046D8
		private void method_1(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Pseudo"))
			{
				Personnel.actPseudo();
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Fly"))
			{
				Personnel.actFly();
			}
			this.string_0 = GUI.TextArea(new Rect(10f, 110f, 130f, 30f), this.string_0);
			if (GUI.Button(new Rect(145f, 110f, 95f, 30f), "Add Bank"))
			{
				Personnel.actGiveBank((double)int.Parse(this.string_0));
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "Revive"))
			{
				Personnel.reviveSelf();
			}
			if (GUI.Button(new Rect(10f, 190f, 230f, 30f), "Kill"))
			{
				Personnel.killSelf();
			}
			if (GUI.Button(new Rect(10f, 230f, 230f, 30f), "Invisibility (DETECTED)"))
			{
				Personnel.actInvisible();
			}
			if (GUI.Button(new Rect(10f, 270f, 230f, 30f), "Give driving license"))
			{
				Personnel.actGivePermis();
			}
			if (GUI.Button(new Rect(10f, 310f, 230f, 30f), "Invinbility"))
			{
				Personnel.actInvincibility();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006494 File Offset: 0x00004894
		private void method_2(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Open trunk"))
			{
				Car.openSelectedVehicleInventory(this.gameObject_0);
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Open engine"))
			{
				Car.openSelectedEngineInventory(this.gameObject_0);
			}
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Enter"))
			{
				Car.enterSelectedVehicle(this.gameObject_0);
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "Start engine"))
			{
				Car.actEngine();
			}
			if (GUI.Button(new Rect(10f, 190f, 230f, 30f), "Custom"))
			{
				this.bool_9 = !this.bool_9;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000065C0 File Offset: 0x000049C0
		private void method_3(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			GUI.Label(new Rect(10f, 30f, 230f, 40f), "Selectionned Player : " + this.characterSetup_0.fullName);
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Inventory"))
			{
				Player.openSelectedPlayerInventory(this.characterSetup_0.inventory);
			}
			if (GUI.Button(new Rect(10f, 120f, 230f, 30f), "Kill"))
			{
				Player.killSelectedPlayer(this.characterSetup_0);
			}
			if (GUI.Button(new Rect(10f, 160f, 230f, 30f), "Revive"))
			{
				Player.reviveSelectedPlayer(this.characterSetup_0);
			}
			if (GUI.Button(new Rect(10f, 200f, 230f, 30f), "Taze"))
			{
				Player.tazeSelectedPlayer(this.characterSetup_0);
			}
			if (GUI.Button(new Rect(10f, 240f, 230f, 30f), "TP to"))
			{
				Player.tpTo(this.characterSetup_0);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00006728 File Offset: 0x00004B28
		private void method_4(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Open chest"))
			{
				this.bool_13 = !this.bool_13;
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Unlock closest door"))
			{
				global::Object.unlockClsDoor();
			}
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Unlock all doors"))
			{
				global::Object.unlockAllDoors();
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "UI"))
			{
				this.bool_7 = !this.bool_7;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00006824 File Offset: 0x00004C24
		private void method_5(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "ESP"))
			{
				if (this.bool_14)
				{
					this.bool_14 = false;
					Utils.Notify("ESP desactivate !");
				}
				else
				{
					this.bool_14 = true;
					Utils.Notify("ESP activate !");
				}
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "AIMBOT"))
			{
				if (this.bool_15)
				{
					this.bool_15 = false;
					Utils.Notify("AIMBOT desactivate !");
				}
				else
				{
					this.bool_15 = true;
					Utils.Notify("AIMBOT activate !");
				}
			}
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Area"))
			{
				this.bool_6 = !this.bool_6;
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "Destroyer"))
			{
				this.bool_8 = !this.bool_8;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00006990 File Offset: 0x00004D90
		private void method_6(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			GUI.Label(new Rect(10f, 30f, 230f, 30f), "Actual area position : " + Nova.character.areaId.ToString());
			GUI.Label(new Rect(10f, 70f, 130f, 30f), "Area selected : ");
			this.string_3 = GUI.TextArea(new Rect(135f, 70f, 95f, 30f), this.string_3);
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Buy area"))
			{
				Utils.smethod_0("CmdBuyArea", Nova.character, new object[] { int.Parse(this.string_3) });
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "Sell area"))
			{
				Utils.smethod_0("CmdAreaSell", Nova.character, new object[] { int.Parse(this.string_3) });
			}
			if (GUI.Button(new Rect(10f, 190f, 230f, 30f), "Kick area owner"))
			{
				Utils.smethod_0("CmdAreaRelease", Nova.character, new object[] { int.Parse(this.string_3) });
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00006B50 File Offset: 0x00004F50
		private void method_7(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Car DealerShip"))
			{
				Utils.smethod_0("UserCode_TargetOpenCarDealership", Nova.character, null);
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "DAB/ATM"))
			{
				Utils.smethod_0("CmdShowAtm", Nova.character.interaction, null);
			}
			if (!GUI.Button(new Rect(10f, 110f, 230f, 30f), "Unknown"))
			{
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00006C10 File Offset: 0x00005010
		private void method_8(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Kill all player"))
			{
				Destroyer.killAllPlayer();
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Revive all player"))
			{
				Destroyer.reviveAllPlayer();
			}
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Taze all player"))
			{
				Destroyer.tazeAllPlayer();
			}
			this.string_4 = GUI.TextArea(new Rect(10f, 150f, 230f, 30f), this.string_4);
			if (GUI.Button(new Rect(10f, 180f, 230f, 30f), "Stealing textures"))
			{
				Destroyer.stealingTextures(this.string_4);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006D1C File Offset: 0x0000511C
		private void method_9(int int_1)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			if (GUI.Button(new Rect(10f, 30f, 230f, 30f), "Use NOS"))
			{
				Car.actNOS();
			}
			if (GUI.Button(new Rect(10f, 70f, 230f, 30f), "Use Turbo"))
			{
				Car.actTurbo();
			}
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Drift Config"))
			{
				Car.actDrift();
			}
			if (GUI.Button(new Rect(10f, 150f, 230f, 30f), "Collisions"))
			{
				Car.actCollisions();
			}
			GUI.Label(new Rect(10f, 190f, 130f, 30f), "Brake Torque : ");
			this.string_5 = GUI.TextArea(new Rect(135f, 190f, 95f, 30f), this.string_5);
			GUI.Label(new Rect(10f, 230f, 130f, 30f), "Engine Torque : ");
			this.string_6 = GUI.TextArea(new Rect(135f, 230f, 95f, 30f), this.string_6);
			GUI.Label(new Rect(10f, 270f, 130f, 30f), "Max Speed : ");
			this.string_7 = GUI.TextArea(new Rect(135f, 270f, 95f, 30f), this.string_7);
			GUI.Label(new Rect(10f, 310f, 130f, 30f), "Mass : ");
			this.string_8 = GUI.TextArea(new Rect(135f, 310f, 95f, 30f), this.string_8);
			GUI.Label(new Rect(10f, 350f, 130f, 30f), "Gear Threshold : ");
			this.string_9 = GUI.TextArea(new Rect(135f, 350f, 95f, 30f), this.string_9);
			GUI.Label(new Rect(10f, 390f, 130f, 30f), "Gear Shifting Delay : ");
			this.string_10 = GUI.TextArea(new Rect(135f, 390f, 95f, 30f), this.string_10);
			GUI.Label(new Rect(10f, 430f, 130f, 30f), "Powered Wheels : ");
			this.string_11 = GUI.TextArea(new Rect(135f, 430f, 95f, 30f), this.string_11);
			if (!GUI.Button(new Rect(10f, 470f, 230f, 30f), "Apply values"))
			{
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002ADD File Offset: 0x00000EDD
		public void DrawClsPlayer(int windowID)
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
			Nova.closestPlayers.ForEach(new Action<CharacterSetup>(this.method_14));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000705C File Offset: 0x0000545C
		private void method_10(int int_1)
		{
			this.string_1 = GUI.TextArea(new Rect(10f, 30f, 230f, 30f), this.string_1);
			this.string_2 = GUI.TextArea(new Rect(10f, 70f, 230f, 30f), this.string_2);
			if (GUI.Button(new Rect(10f, 110f, 230f, 30f), "Login"))
			{
				Utils.smethod_1(this.string_1, this.string_2);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00007108 File Offset: 0x00005508
		private void method_11(Vector3 vector3_1, Vector3 vector3_2, Color color_0)
		{
			float num = vector3_2.y - vector3_1.y;
			float num2 = 4f;
			float num3 = num / num2;
			Render.DrawBox(vector3_1.x - num3 / 2f, (float)Screen.height - vector3_1.y - num + 2f, num3, num, color_0, 2f);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000074F4 File Offset: 0x000058F4
		[CompilerGenerated]
		private void method_12(CharacterSetup characterSetup_1)
		{
			Vector3 position = characterSetup_1.gameObject.transform.position;
			Vector3 vector;
			vector.x = position.x;
			vector.z = position.z;
			vector.y = position.y + 2f;
			Vector3 vector2;
			vector2.x = vector.x;
			vector2.z = vector.z;
			vector2.y = vector.y - 2f;
			Vector3 vector3 = Camera.main.WorldToScreenPoint(vector);
			Vector3 vector4 = Camera.main.WorldToScreenPoint(vector2);
			if (vector3.z > 0f)
			{
				this.method_11(vector3, vector4, Color.blue);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000075DC File Offset: 0x000059DC
		[CompilerGenerated]
		private void method_13(CharacterSetup characterSetup_1)
		{
			Vector3 position = characterSetup_1.gameObject.transform.position;
			Vector3 vector;
			vector.x = position.x;
			vector.z = position.z;
			vector.y = position.y + 1.8f;
			Vector3 vector2 = Camera.main.WorldToScreenPoint(vector);
			if (vector2.z > 0f && characterSetup_1.health > 0)
			{
				Vector2 vector3;
				vector3.x = (float)(Screen.width / 2);
				vector3.y = (float)(Screen.height / 2);
				if (Vector2.Distance(vector3, vector2) <= this.float_0)
				{
					Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector2.x, (float)Screen.height - vector2.y), Color.red, 2f);
					if (Vector2.Distance(vector3, vector2) < Vector2.Distance(vector3, this.vector3_0))
					{
						this.vector3_0 = vector;
					}
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00007750 File Offset: 0x00005B50
		[CompilerGenerated]
		private void method_14(CharacterSetup characterSetup_1)
		{
			int num = Nova.closestPlayers.IndexOf(characterSetup_1);
			this.rect_10.height = (float)(num * 40 + 70);
			if (GUI.Button(new Rect(10f, (float)(num * 40 + 30), 230f, 30f), characterSetup_1.fullName))
			{
				this.characterSetup_0 = characterSetup_1;
				this.bool_3 = true;
			}
		}

		// Token: 0x04000013 RID: 19
		private bool bool_0 = false;

		// Token: 0x04000014 RID: 20
		private bool bool_1 = false;

		// Token: 0x04000015 RID: 21
		private bool bool_2 = false;

		// Token: 0x04000016 RID: 22
		private bool bool_3 = false;

		// Token: 0x04000017 RID: 23
		private bool bool_4 = false;

		// Token: 0x04000018 RID: 24
		private bool bool_5 = false;

		// Token: 0x04000019 RID: 25
		private bool bool_6 = false;

		// Token: 0x0400001A RID: 26
		private bool bool_7 = false;

		// Token: 0x0400001B RID: 27
		private bool bool_8 = false;

		// Token: 0x0400001C RID: 28
		private bool bool_9 = false;

		// Token: 0x0400001D RID: 29
		private bool bool_10 = false;

		// Token: 0x0400001E RID: 30
		internal static bool bool_11;

		// Token: 0x0400001F RID: 31
		private bool bool_12 = false;

		// Token: 0x04000020 RID: 32
		private bool bool_13 = false;

		// Token: 0x04000021 RID: 33
		private bool bool_14 = false;

		// Token: 0x04000022 RID: 34
		private bool bool_15 = false;

		// Token: 0x04000023 RID: 35
		private Rect rect_0 = new Rect(20f, 20f, 250f, 230f);

		// Token: 0x04000024 RID: 36
		private Rect rect_1 = new Rect(270f, 20f, 250f, 350f);

		// Token: 0x04000025 RID: 37
		private Rect rect_2 = new Rect(520f, 20f, 250f, 230f);

		// Token: 0x04000026 RID: 38
		private Rect rect_3 = new Rect(770f, 20f, 250f, 280f);

		// Token: 0x04000027 RID: 39
		private Rect rect_4 = new Rect(1020f, 20f, 250f, 190f);

		// Token: 0x04000028 RID: 40
		private Rect rect_5 = new Rect(1270f, 20f, 250f, 190f);

		// Token: 0x04000029 RID: 41
		private Rect rect_6 = new Rect(1270f, 210f, 250f, 230f);

		// Token: 0x0400002A RID: 42
		private Rect rect_7 = new Rect(1020f, 210f, 250f, 150f);

		// Token: 0x0400002B RID: 43
		private Rect rect_8 = new Rect(1270f, 440f, 250f, 230f);

		// Token: 0x0400002C RID: 44
		private Rect rect_9 = new Rect(520f, 250f, 250f, 600f);

		// Token: 0x0400002D RID: 45
		private Rect rect_10 = new Rect(770f, 300f, 250f, 70f);

		// Token: 0x0400002E RID: 46
		private Rect rect_11 = new Rect((float)((Screen.width - 250) / 2), (float)((Screen.height - 150) / 2), 250f, 150f);

		// Token: 0x0400002F RID: 47
		internal CharacterSetup characterSetup_0;

		// Token: 0x04000030 RID: 48
		internal int int_0 = 1;

		// Token: 0x04000031 RID: 49
		private string string_0 = "1000";

		// Token: 0x04000032 RID: 50
		internal string string_1 = "User";

		// Token: 0x04000033 RID: 51
		internal string string_2 = "Password";

		// Token: 0x04000034 RID: 52
		private string string_3 = "0";

		// Token: 0x04000035 RID: 53
		private string string_4 = "webhook";

		// Token: 0x04000036 RID: 54
		private string string_5 = "";

		// Token: 0x04000037 RID: 55
		private string string_6 = "";

		// Token: 0x04000038 RID: 56
		private string string_7 = "";

		// Token: 0x04000039 RID: 57
		private string string_8 = "";

		// Token: 0x0400003A RID: 58
		private string string_9 = "";

		// Token: 0x0400003B RID: 59
		private string string_10 = "";

		// Token: 0x0400003C RID: 60
		private string string_11 = "";

		// Token: 0x0400003D RID: 61
		internal GameObject gameObject_0;

		// Token: 0x0400003E RID: 62
		private float float_0 = 100f;

		// Token: 0x0400003F RID: 63
		private float float_1 = 100000f;

		// Token: 0x04000040 RID: 64
		private Vector3 vector3_0;

		// Token: 0x04000041 RID: 65
		private Ray ray_0;

		// Token: 0x04000042 RID: 66
		private RaycastHit raycastHit_0;
	}
}
