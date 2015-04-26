using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
	public GameObject BoxPrefab;

	void Start ()
	{
		CreatCube ();
	}

	void CreatCube ()
	{
		if (BoxPrefab == null) {
			return;
		}

		for (int i = 0; i < 27; i++) {
			GameObject obj = Instantiate (BoxPrefab);
			obj.transform.SetParent (this.transform);
			obj.transform.localScale = Vector3.one * 0.95f;
			obj.transform.position = new Vector3 (i % 3 - 1, i / 9 - 1, (i % 9) / 3 - 1);
			obj.name = "Cube" + i.ToString ("D2");

			SingleBox box = obj.GetComponent <SingleBox> ();
			box.locationIndex = i;

			switch (i) {
			case 0:
			case 2:
			case 6:
			case 8:
			case 18:
			case 20:
			case 24:
			case 26:
				box.myStyle = CubeStyle.Corner;
				break;
			case 13:
				box.myStyle = CubeStyle.None;
				break;
			case 4:
			case 10:
			case 12:
			case 14:
			case 16:
			case 22:
				box.myStyle = CubeStyle.Face;
				break;
			case 1:
			case 3:
			case 5:
			case 7:
			case 9:
			case 11:
			case 15:
			case 17:
			case 19:
			case 21:
			case 23:
			case 25:
				box.myStyle = CubeStyle.Edge;
				break;
			}

			box.InitColor ();

//			if (i.Equals (13)) {
//				obj.GetComponent <SingleBox> ().mystyle = CubeStyle.None;
//			} else if (i.Equals (4) || i.Equals (10) || i.Equals (12) || i.Equals (14) ||
//			           i.Equals (16) || i.Equals (22)) {
//				obj.GetComponent <SingleBox> ().mystyle = CubeStyle.Face;
//			} else if (i.Equals (0) || i.Equals (2) || i.Equals (6) || i.Equals (8) ||
//			           i.Equals (18) || i.Equals (20) || i.Equals (24) || i.Equals (26)) {
//				obj.GetComponent <SingleBox> ().mystyle = CubeStyle.Corner;
//			} else {
//				obj.GetComponent <SingleBox> ().mystyle = CubeStyle.Edge;
//			}
		}

		SetFullColor ();
	}

	public void SetFullColor ()
	{
		SingleBox[] boxes = FindObjectsOfType <SingleBox> ();
		foreach (SingleBox b in boxes) {
			b.InitColor ();
		}
		// 设置初始颜色
		MagicCubeOperate opetarer = FindObjectOfType <MagicCubeOperate> ();
		SingleBoxPiece[] pieces = FindObjectsOfType <SingleBoxPiece> ();
		foreach (SingleBoxPiece p in pieces) {
			if (Mathf.Abs (p.transform.position.x) < 1.25f &&
			    Mathf.Abs (p.transform.position.y) < 1.25f &&
			    Mathf.Abs (p.transform.position.z) < 1.25f) {
				p.GetComponent <MeshRenderer> ().material.color = new Color (0f, 0f, 0f, 0.5f);
				p.enabled = false;
			}
			if (p.transform.position.x >= 1.25f) {
				opetarer.PickColor (MagicColor.Green);
				p.GetComponentInParent <SingleBox> ().SetColor (p.gameObject);
			} else if (p.transform.position.x <= -1.25f) {
				opetarer.PickColor (MagicColor.Blue);
				p.GetComponentInParent <SingleBox> ().SetColor (p.gameObject);
			} else if (p.transform.position.y >= 1.25f) {
				opetarer.PickColor (MagicColor.Yellow);
				p.GetComponentInParent <SingleBox> ().SetColor (p.gameObject);
			} else if (p.transform.position.y <= -1.25f) {
				opetarer.PickColor (MagicColor.White);
				p.GetComponentInParent <SingleBox> ().SetColor (p.gameObject);
			} else if (p.transform.position.z <= -1.25f) {
				opetarer.PickColor (MagicColor.Red);
				p.GetComponentInParent <SingleBox> ().SetColor (p.gameObject);
			} else if (p.transform.position.z >= 1.25f) {
				opetarer.PickColor (MagicColor.Orange);
				p.GetComponentInParent <SingleBox> ().SetColor (p.gameObject);
			}
		}
		
	}

	public void SetEditColor ()
	{
		SingleBox[] boxes = FindObjectsOfType <SingleBox> ();
		foreach (SingleBox b in boxes) {
			b.InitColor ();
		}
		// 设置编辑颜色的初始颜色
		MagicCubeOperate opetarer = FindObjectOfType <MagicCubeOperate> ();
		SingleBoxPiece[] pieces = FindObjectsOfType <SingleBoxPiece> ();
		foreach (SingleBoxPiece p in pieces) {
			if (Mathf.Abs (p.transform.position.x) < 1.25f &&
			    Mathf.Abs (p.transform.position.y) < 1.25f &&
			    Mathf.Abs (p.transform.position.z) < 1.25f) {
				p.GetComponent <MeshRenderer> ().material.color = new Color (0f, 0f, 0f, 0.5f);
				p.enabled = false;
			} else {
				SingleBox sb = p.GetComponentInParent <SingleBox> ();
				if (sb.myStyle.Equals (CubeStyle.Face)) {
					if (p.transform.position.x >= 1.25f) {
						opetarer.PickColor (MagicColor.Green);
						sb.SetColor (p.gameObject);
					} else if (p.transform.position.x <= -1.25f) {
						opetarer.PickColor (MagicColor.Blue);
						sb.SetColor (p.gameObject);
					} else if (p.transform.position.y >= 1.25f) {
						opetarer.PickColor (MagicColor.Yellow);
						sb.SetColor (p.gameObject);
					} else if (p.transform.position.y <= -1.25f) {
						opetarer.PickColor (MagicColor.White);
						sb.SetColor (p.gameObject);
					} else if (p.transform.position.z <= -1.25f) {
						opetarer.PickColor (MagicColor.Red);
						sb.SetColor (p.gameObject);
					} else if (p.transform.position.z >= 1.25f) {
						opetarer.PickColor (MagicColor.Orange);
						sb.SetColor (p.gameObject);
					}
				}
			}
		}

	}

	void OnGUI ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			SetFullColor ();
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			SetEditColor ();
		}
	}

}
