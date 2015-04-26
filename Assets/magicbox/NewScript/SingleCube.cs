using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleCube : MonoBehaviour
{
	public Dictionary<GameObject,MagicColor> cubecolor = new Dictionary<GameObject, MagicColor> ();

	public CubeStyle cubeStyle = CubeStyle.None;
	public int locationIndex;
	public int finishIndex;

	private MagicCubeOperate operater;

	private float size = 1f;

	public float Size { set { size = value; } }

	void Start ()
	{
		AdjustPos ();
		operater = FindObjectOfType <MagicCubeOperate> ();
	}

	#region AdjustPos

	public void AdjustPos ()
	{
		transform.position = AdjustPositionValue (transform.position);
		transform.eulerAngles = AdjustAngleValue (transform.eulerAngles);
	}

	// tools for adjust
	Vector3 AdjustPositionValue (Vector3 val)
	{
		return new Vector3 (
			val.x > 0.5f * size ? size : val.x < -0.5 * size ? -size : 0,
			val.y > 0.5f * size ? size : val.y < -0.5 * size ? -size : 0,
			val.z > 0.5f * size ? size : val.z < -0.5 * size ? -size : 0);
	}

	Vector3 AdjustAngleValue (Vector3 val)
	{
		return new Vector3 (GetValueGrade (val.x), GetValueGrade (val.y), GetValueGrade (val.z));
	}

	float GetValueGrade (float val)
	{
		float v = 0f;
		if (val <= -315f) {
			v = 0f;
		} else if (val <= -225f) {
			v = -270f;
		} else if (val <= -135f) {
			v = -180f;
		} else if (val <= -45f) {
			v = -90f;
		} else if (val <= 45f) {
			v = 0f;
		} else if (val <= 135f) {
			v = 90f;
		} else if (val <= 225f) {
			v = 180f;
		} else if (val <= 315f) {
			v = 270f;
		} else if (val > 315f) {
			v = 0f;
		}
		return v;
	}

	#endregion

	public void InitColor ()
	{
		SingleBoxPiece[] mypieces = GetComponentsInChildren <SingleBoxPiece> ();
		cubecolor.Clear ();
		foreach (SingleBoxPiece p in mypieces) {
			p.GetComponent <MeshRenderer> ().material.color = Color.gray;
			cubecolor.Add (p.gameObject, MagicColor.None);
		}
	}

	public void SetColor (GameObject p)
	{
		bool cansetcolor = true;
		if (operater == null) {
			operater = FindObjectOfType <MagicCubeOperate> ();
		}
		MagicColor curcolor = operater.EditColor;
		if (cubecolor.ContainsKey (p)) {
			foreach (GameObject g in cubecolor.Keys) {
				if (g != p) {
					if (!cubecolor [g].Equals (MagicColor.None)) {
						if (cubecolor [g] == curcolor) {
							cansetcolor = false;
							break;
						}
						int index = (int)curcolor;
						if (index >= 3 && (int)cubecolor [g] == index - 3) {
							cansetcolor = false;
							break;
						} else if (index < 3 && (int)cubecolor [g] == index + 3) {
							cansetcolor = false;
							break;
						}
					}
				}
			}
			if (cansetcolor) {
				p.GetComponent <MeshRenderer> ().material.color = MyMapPrefab.ColorMap [curcolor];
				cubecolor [p] = curcolor;
			}
		}
	}

}
