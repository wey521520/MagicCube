using UnityEngine;
using System.Collections.Generic;

public class MyMapPrefab : MonoBehaviour
{

	public static Dictionary<MagicColor,Color32> ColorMap = new Dictionary<MagicColor, Color32> ();
	public static Dictionary<CubeFaceStyle,MagicColor> StyleColorMap = new Dictionary<CubeFaceStyle, MagicColor> ();

	public static Dictionary<OperateStep,string> Step2StringMap = new Dictionary<OperateStep, string> ();

	void Awake ()
	{

		Step2StringMap.Add (OperateStep.L, "L");
		Step2StringMap.Add (OperateStep.L2, "L2");
		Step2StringMap.Add (OperateStep.L0, "L'");

		Step2StringMap.Add (OperateStep.R, "R");
		Step2StringMap.Add (OperateStep.R2, "R2");
		Step2StringMap.Add (OperateStep.R0, "R'");

		Step2StringMap.Add (OperateStep.U, "U");
		Step2StringMap.Add (OperateStep.U2, "U2");
		Step2StringMap.Add (OperateStep.U0, "U'");

		Step2StringMap.Add (OperateStep.D, "D");
		Step2StringMap.Add (OperateStep.D2, "D2");
		Step2StringMap.Add (OperateStep.D0, "D'");

		Step2StringMap.Add (OperateStep.F, "F");
		Step2StringMap.Add (OperateStep.F2, "F2");
		Step2StringMap.Add (OperateStep.F0, "F'");

		Step2StringMap.Add (OperateStep.B, "B");
		Step2StringMap.Add (OperateStep.B2, "B2");
		Step2StringMap.Add (OperateStep.B0, "B'");

		Step2StringMap.Add (OperateStep.X, "X");
		Step2StringMap.Add (OperateStep.X2, "X2");
		Step2StringMap.Add (OperateStep.X0, "X'");

		Step2StringMap.Add (OperateStep.Y, "Y");
		Step2StringMap.Add (OperateStep.Y2, "Y2");
		Step2StringMap.Add (OperateStep.Y0, "Y'");

		Step2StringMap.Add (OperateStep.Z, "Z");
		Step2StringMap.Add (OperateStep.Z2, "Z2");
		Step2StringMap.Add (OperateStep.Z0, "Z'");

		ColorMap.Add (MagicColor.White, new Color32 (255, 255, 255, 255));
		ColorMap.Add (MagicColor.Red, new Color32 (254, 5, 0, 255));
		ColorMap.Add (MagicColor.Green, new Color32 (12, 255, 2, 255));
		ColorMap.Add (MagicColor.Yellow, new Color32 (255, 255, 0, 255));
		ColorMap.Add (MagicColor.Orange, new Color32 (255, 102, 0, 255));
		ColorMap.Add (MagicColor.Blue, new Color32 (9, 3, 255, 255));
		ColorMap.Add (MagicColor.None, new Color32 (128, 128, 128, 255));

		StyleColorMap.Add (CubeFaceStyle.Up, MagicColor.Yellow);
		StyleColorMap.Add (CubeFaceStyle.Right, MagicColor.Red);
		StyleColorMap.Add (CubeFaceStyle.Front, MagicColor.Blue);
		StyleColorMap.Add (CubeFaceStyle.Left, MagicColor.Orange);
		StyleColorMap.Add (CubeFaceStyle.Back, MagicColor.Green);
		StyleColorMap.Add (CubeFaceStyle.Down, MagicColor.White);
		StyleColorMap.Add (CubeFaceStyle.None, MagicColor.None);
	}
}
