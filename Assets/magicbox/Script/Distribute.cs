using UnityEngine;
using System.Collections;

public class Distribute : MonoBehaviour
{
	
	public enum SpaceStyle
	{
		None,
		Half,
		One,
		Two
	}

	public enum Distribution
	{
		Horizontal,
		Vertical
	}


	public static void AutoDistribute (GameObject[] objs, SpaceStyle style, Distribution dis)
	{
		RectTransform[] trans = new RectTransform[objs.Length];
		for (int i = 0; i < objs.Length; i++) {
			trans [i] = objs [i].GetComponent <RectTransform> ();
		}
		AutoDistribute (trans, style, dis);
	}

	public static void AutoDistribute (RectTransform[] rts, SpaceStyle style, Distribution dis)
	{
		float space = 0f;
		switch (style) {
		case SpaceStyle.None:
			space = 0f;
			break;
		case SpaceStyle.Half:
			space = 0.5f;
			break;
		case SpaceStyle.One:
			space = 1f;
			break;
		case SpaceStyle.Two:
			space = 2f;
			break;
		}

		float l = 1f / (rts.Length + space * rts.Length - space);
		switch (dis) {
		case Distribution.Horizontal:
			for (int i = 0; i < rts.Length; i++) {
				rts [i].anchorMin = new Vector2 (i * l * (1 + space), 0f);
				rts [i].anchorMax = new Vector2 (i * l * (1 + space) + l, 1f);
			}
			break;
		case Distribution.Vertical:
			for (int i = 0; i < rts.Length; i++) {
				rts [i].anchorMin = new Vector2 (0f, i * l * (1 + space));
				rts [i].anchorMax = new Vector2 (1f, i * l * (1 + space) + l);
			}
			break;
		}
	}
}
