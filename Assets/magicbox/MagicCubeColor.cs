using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicCubeColor : MonoBehaviour
{

	public static Dictionary<MagicColor,UnityEngine.Color32> ColorMap = new Dictionary<MagicColor, Color32> ();

	void Awake ()
	{
		ColorMap.Add (MagicColor.White, new Color32 (255, 255, 255, 255));
		ColorMap.Add (MagicColor.Red, new Color32 (254, 5, 0, 255));
		ColorMap.Add (MagicColor.Blue, new Color32 (9, 3, 255, 255));
		ColorMap.Add (MagicColor.Yellow, new Color32 (255, 255, 0, 255));
		ColorMap.Add (MagicColor.Orange, new Color32 (255, 102, 0, 255));
		ColorMap.Add (MagicColor.Green, new Color32 (12, 255, 2, 255));
	}
}
