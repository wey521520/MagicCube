using UnityEngine;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;

public class SingleBox : MonoBehaviour
{
	public Dictionary<GameObject,MagicColor> cubecolor = new Dictionary<GameObject, MagicColor> ();

	public CubeStyle myStyle = CubeStyle.None;
	public int locationIndex;

	private MagicCubeOperate operater;

	void Start ()
	{
		AdjustPos ();
		operater = FindObjectOfType <MagicCubeOperate> ();
	}

	public void AdjustPos ()
	{
		transform.position = AdjustPositionValue (transform.position);
		transform.eulerAngles = AdjustAngleValue (transform.eulerAngles);
	}

	#region tools for adjust

	Vector3 AdjustPositionValue (Vector3 val)
	{
		Vector3 posvalue = Vector3.zero;
		posvalue = new Vector3 (
//			val.x >= 0.75f ? 1f : val.x <= -0.75f ? -1f : val.x <= -0.25f ? -0.5f : val.x >= 0.25f ? 0.5f : 0f,
//			val.y >= 0.75f ? 1f : val.y <= -0.75f ? -1f : val.y <= -0.25f ? -0.5f : val.y >= 0.25f ? 0.5f : 0f,
//			val.z >= 0.75f ? 1f : val.z <= -0.75f ? -1f : val.z <= -0.25f ? -0.5f : val.z >= 0.25f ? 0.5f : 0f);
			val.x > 0.5 ? 1f : val.x < -0.5 ? -1f : 0,
			val.y > 0.5 ? 1f : val.y < -0.5 ? -1f : 0,
			val.z > 0.5 ? 1f : val.z < -0.5 ? -1f : 0);
		return posvalue;
	}

	Vector3 AdjustAngleValue (Vector3 val)
	{
		Vector3 anglevalue = Vector3.zero;
		anglevalue = new Vector3 (GetValueGrade (val.x), GetValueGrade (val.y), GetValueGrade (val.z));
		return anglevalue;
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
