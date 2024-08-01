using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class Render : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600002A RID: 42 RVA: 0x000029C4 File Offset: 0x00000DC4
	// (set) Token: 0x0600002B RID: 43 RVA: 0x000029CB File Offset: 0x00000DCB
	public static GUIStyle StringStyle { get; set; }

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600002C RID: 44 RVA: 0x00005108 File Offset: 0x00003508
	// (set) Token: 0x0600002D RID: 45 RVA: 0x000029D6 File Offset: 0x00000DD6
	public static Color Color
	{
		get
		{
			return GUI.color;
		}
		set
		{
			GUI.color = value;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000029E1 File Offset: 0x00000DE1
	public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
	{
		Render.Color = color;
		Render.DrawBox(position, size, centered);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00005124 File Offset: 0x00003524
	public static void DrawBox(Vector2 position, Vector2 size, bool centered = true)
	{
		if (centered)
		{
			position - size / 2f;
		}
		GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, 0);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000029FD File Offset: 0x00000DFD
	public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
	{
		Render.Color = color;
		Render.DrawString(position, label, centered);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00005174 File Offset: 0x00003574
	public static void DrawString(Vector2 position, string label, bool centered = true)
	{
		GUIContent guicontent = new GUIContent(label);
		Vector2 vector = Render.StringStyle.CalcSize(guicontent);
		Vector2 vector2 = (centered ? (position - vector / 2f) : position);
		GUI.Label(new Rect(vector2, vector), guicontent);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000051E0 File Offset: 0x000035E0
	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
	{
		Matrix4x4 matrix = GUI.matrix;
		if (!Render.lineTex)
		{
			Render.lineTex = new Texture2D(1, 1);
		}
		Color color2 = GUI.color;
		GUI.color = color;
		float num = Vector3.Angle(pointB - pointA, Vector2.right);
		if (pointA.y > pointB.y)
		{
			num = -num;
		}
		GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
		GUIUtility.RotateAroundPivot(num, pointA);
		GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), Render.lineTex);
		GUI.matrix = matrix;
		GUI.color = color2;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00005304 File Offset: 0x00003704
	public static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
	{
		Render.DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
		Render.DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
		Render.DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
		Render.DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000053D8 File Offset: 0x000037D8
	public static void DrawBoxOutline(Vector2 Point, float width, float height, Color color, float thickness)
	{
		Render.DrawLine(Point, new Vector2(Point.x + width, Point.y), color, thickness);
		Render.DrawLine(Point, new Vector2(Point.x, Point.y + height), color, thickness);
		Render.DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x + width, Point.y), color, thickness);
		Render.DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x, Point.y + height), color, thickness);
	}

	// Token: 0x0400000D RID: 13
	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static GUIStyle guistyle_0 = new GUIStyle(GUI.skin.label);

	// Token: 0x0400000E RID: 14
	public static Texture2D lineTex;
}
