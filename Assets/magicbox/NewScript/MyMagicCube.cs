﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MyMagicCube : MonoBehaviour
{
	#region Creat A Cube

	// 单纯的魔方单块的模型（不具备操作特性，即捕捉点击滑动等）
	public GameObject CubePrefab;
	// 魔方的（9*6）54个单元面，用来捕捉鼠标或touch事件
	public GameObject FacePrefab;

	public float singlecubesize = 1f;

	private SingleCube[] singlecubes;
	private CubeFace[] cubefaces;

	#endregion

	#region Operate The Cube

	public State mystate = State.Operate;

	private bool rotating;

	public bool Stop{ get { return !rotating; } }

	private bool judged;

	public  float singleanitime = 0.4f;
	private float rlength;

	// 当前编辑所选中的颜色
	private MagicColor curColor = MagicColor.White;

	public MagicColor Color { get { return curColor; } }

	List<SingleCube> operatelist = new List<SingleCube> ();

	#endregion



	void Start ()
	{
		CreatCube ();
		rlength = 180f / Screen.width / 0.23f / 3.14f;

	}

	// creat
	void CreatCube ()
	{
		if (CubePrefab == null || FacePrefab == null) {
			Debug.Log ("找不到单体！");
			return;
		}
		singlecubes = new SingleCube[27];
		// 先实例化魔方的块（这里只实例化实际的东西，初始化的时候再判断游戏的模式）
		for (int i = 0; i < 27; i++) {
			GameObject obj = Instantiate (CubePrefab);
			obj.transform.SetParent (this.transform);
			obj.transform.localScale = Vector3.one * singlecubesize * 0.99f;
			obj.transform.position = new Vector3 ((i % 9) / 3 - 1, i / 9 - 1, i % 3 - 1) * singlecubesize;
			obj.name = "Cube" + i.ToString ("D2");

			SingleCube cube = obj.GetComponent <SingleCube> ();
			singlecubes [i] = cube;

			cube.locationIndex = i;
			cube.Size = singlecubesize;

			switch (i) {
			case 0:
			case 2:
			case 6:
			case 8:
			case 18:
			case 20:
			case 24:
			case 26:
				cube.cubeStyle = CubeStyle.Corner;
				break;
			case 13:
				cube.cubeStyle = CubeStyle.None;
				break;
			case 4:
			case 10:
			case 12:
			case 14:
			case 16:
			case 22:
				cube.cubeStyle = CubeStyle.Face;
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
				cube.cubeStyle = CubeStyle.Edge;
				break;
			}

		}

		cubefaces = new CubeFace[54];
		for (int i = 0; i < 54; i++) {
			GameObject obj = Instantiate (FacePrefab);
			cubefaces [i] = obj.GetComponent <CubeFace> ();

			int j = 0;
			int l = 0;
			if (i < 9) {
				j = i;
				l = j;
				obj.transform.position = new Vector3 (j / 3 - 1, -1.5f, j % 3f - 1f) * singlecubesize;
				obj.transform.eulerAngles = new Vector3 (-90, 0, 0);
				cubefaces [i].facestyle = CubeFaceStyle.Down;
			} else if (i < 18) {
				j = i - 9;
				l = (j / 3) * 9 + (j % 3) * 3 + 2;
				obj.transform.position = new Vector3 (j % 3 - 1, j / 3 - 1, 1.5f) * singlecubesize;
				obj.transform.eulerAngles = new Vector3 (180, 0, 0);
				cubefaces [i].facestyle = CubeFaceStyle.Front;
			} else if (i < 27) {
				j = i - 18;
				l = (j / 3) * 9 + j % 3;
				obj.transform.position = new Vector3 (-1.5f, j / 3 - 1, j % 3 - 1) * singlecubesize;
				obj.transform.eulerAngles = new Vector3 (0, 90, 0);
				cubefaces [i].facestyle = CubeFaceStyle.Right;	
			} else if (i < 36) {
				j = i - 27;
				l = j + 18;
				obj.transform.position = new Vector3 (j / 3 - 1, 1.5f, j % 3f - 1f) * singlecubesize;
				obj.transform.eulerAngles = new Vector3 (90, 0, 0);
				cubefaces [i].facestyle = CubeFaceStyle.Up;
			} else if (i < 45) {
				j = i - 36;
				l = (j / 3) * 9 + (j % 3) * 3;
				obj.transform.position = new Vector3 (j % 3 - 1, j / 3 - 1, -1.5f) * singlecubesize;
				obj.transform.eulerAngles = new Vector3 (0, 0, 0);
				cubefaces [i].facestyle = CubeFaceStyle.Left;
			} else if (i < 54) {
				j = i - 45;
				l = (j / 3) * 9 + j % 3 + 6;
				obj.transform.position = new Vector3 (1.5f, j / 3 - 1, j % 3 - 1) * singlecubesize;
				obj.transform.eulerAngles = new Vector3 (0, -90, 0);
				cubefaces [i].facestyle = CubeFaceStyle.Back;
			}
			obj.transform.SetParent (singlecubes [l].transform);

			obj.transform.localScale = Vector3.one * singlecubesize;
			obj.name = "Face" + i.ToString ("D2");
		}
	}

	// set original color
	IEnumerator SetFullColor ()
	{
		for (int i = 0; i < cubefaces.Length; i++) {
			yield return new WaitForSeconds (0.1f);
			cubefaces [i].UpdateFaceStyle ();
			cubefaces [i].SetDefaultColor ();
		}
		yield return null;
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

	// for test
	void OnGUI ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			StartCoroutine (SetFullColor ());
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			SetEditColor ();
		}

		if (GUILayout.Button ("DoMyFormula")) {
			StartCoroutine (DoFormula ());
		}
		if (GUILayout.Button ("Delete")) {
			FormularDelete ();
		}
		GUILayout.Label ("\t\t\t" + formula);

		if (GUILayout.Button ("CreatBrokeFormula")) {
			CreatBrokeFormula ();
		}
		if (GUILayout.Button ("DoBrokeFormula")) {
			formula = astr;
			spacetime = 0f;
			singleanitime = 0f;
			StartCoroutine (DoFormula ());
		}
		if (GUILayout.Button ("DoRecoverFormula")) {
			formula = bstr;
			spacetime = 0.5f;
			singleanitime = 0.4f;
			StartCoroutine (DoFormula ());
		}
		GUILayout.Label ("\t\t\t" + astr);
		GUILayout.Label ("\t\t\t" + bstr);
	}





	// Update is called once per frame
	void Update ()
	{
		#region For Test

		if (Input.GetKeyDown (KeyCode.L) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoLP2 ();
				FormularAdd (OperateStep.L2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoLN ();
				FormularAdd (OperateStep.L0);
			} else {
				DoLP ();
				FormularAdd (OperateStep.L);
			}
		}

		if (Input.GetKeyDown (KeyCode.R) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoRP2 ();
				FormularAdd (OperateStep.R2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoRN ();
				FormularAdd (OperateStep.R0);
			} else {
				DoRP ();
				FormularAdd (OperateStep.R);
			}
		}

		if (Input.GetKeyDown (KeyCode.U) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoUP2 ();
				FormularAdd (OperateStep.U2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoUN ();
				FormularAdd (OperateStep.U0);
			} else {
				DoUP ();
				FormularAdd (OperateStep.U);
			}
		}

		if (Input.GetKeyDown (KeyCode.D) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoDP2 ();
				FormularAdd (OperateStep.D2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoDN ();
				FormularAdd (OperateStep.D0);
			} else {
				DoDP ();
				FormularAdd (OperateStep.D);
			}
		}

		if (Input.GetKeyDown (KeyCode.F) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoFP2 ();
				FormularAdd (OperateStep.F2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoFN ();
				FormularAdd (OperateStep.F0);
			} else {
				DoFP ();
				FormularAdd (OperateStep.F);
			}
		}

		if (Input.GetKeyDown (KeyCode.B) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoBP2 ();
				FormularAdd (OperateStep.B2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoBN ();
				FormularAdd (OperateStep.B0);
			} else {
				DoBP ();
				FormularAdd (OperateStep.B);
			}
		}

		if (Input.GetKeyDown (KeyCode.X) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoXP2 ();
				FormularAdd (OperateStep.X2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoXN ();
				FormularAdd (OperateStep.X0);
			} else {
				DoXP ();
				FormularAdd (OperateStep.X);
			}
		}

		if (Input.GetKeyDown (KeyCode.Y) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoYP2 ();
				FormularAdd (OperateStep.Y2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoYN ();
				FormularAdd (OperateStep.Y0);
			} else {
				DoYP ();
				FormularAdd (OperateStep.Y);
			}
		}

		if (Input.GetKeyDown (KeyCode.Z) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoZP2 ();
				FormularAdd (OperateStep.Z2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoZN ();
				FormularAdd (OperateStep.Z0);
			} else {
				DoZP ();
				FormularAdd (OperateStep.Z);
			}
		}
		#endregion
	}



	#region AutoOperate or PressButtonAndAnimation

	public void DoSingleStep (OperateStep step)
	{
		if (rotating) {
			return;
		}
		switch (step) {
		case OperateStep.L:
			DoLP ();
			break;
		case OperateStep.L0:
			DoLN ();
			break;
		case OperateStep.L2:
			DoLP2 ();
			break;
		case OperateStep.R:
			DoRP ();
			break;
		case OperateStep.R0:
			DoRN ();
			break;
		case OperateStep.R2:
			DoRP2 ();
			break;
		case OperateStep.U:
			DoUP ();
			break;
		case OperateStep.U0:
			DoUN ();
			break;
		case OperateStep.U2:
			DoUP2 ();
			break;
		case OperateStep.D:
			DoDP ();
			break;
		case OperateStep.D0:
			DoDN ();
			break;
		case OperateStep.D2:
			DoDP2 ();
			break;
		case OperateStep.F:
			DoFP ();
			break;
		case OperateStep.F0:
			DoFN ();
			break;
		case OperateStep.F2:
			DoFP2 ();
			break;
		case OperateStep.B:
			DoBP ();
			break;
		case OperateStep.B0:
			DoBN ();
			break;
		case OperateStep.B2:
			DoBP2 ();
			break;
		case OperateStep.X:
			DoXP ();
			break;
		case OperateStep.X0:
			DoXN ();
			break;
		case OperateStep.X2:
			DoXP2 ();
			break;
		case OperateStep.Y:
			DoYP ();
			break;
		case OperateStep.Y0:
			DoYN ();
			break;
		case OperateStep.Y2:
			DoYP2 ();
			break;
		case OperateStep.Z:
			DoZP ();
			break;
		case OperateStep.Z0:
			DoZN ();
			break;
		case OperateStep.Z2:
			DoZP2 ();
			break;
		}
	}

	#region SlngleStep 标准单步操作

	#region L L2 L'

	//L
	public void DoLP ()
	{
		GetOperateSuit (OperateSuit.Left);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.forward, 90f, singleanitime));
	}

	//L2
	public void DoLP2 ()
	{
		GetOperateSuit (OperateSuit.Left);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.forward, 180f, singleanitime * 2f));
	}

	//L‘
	public void DoLN ()
	{
		GetOperateSuit (OperateSuit.Left);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.forward, -90f, singleanitime));
	}

	#endregion

	#region R R2 R'

	//R
	public void DoRP ()
	{
		GetOperateSuit (OperateSuit.Right);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.back, 90f, singleanitime));
	}

	//R2
	public void DoRP2 ()
	{
		GetOperateSuit (OperateSuit.Right);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.back, 180f, singleanitime * 2f));
	}

	//R'
	public void DoRN ()
	{
		GetOperateSuit (OperateSuit.Right);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.back, -90f, singleanitime));
	}

	#endregion

	#region U U2 U'

	//U
	public void DoUP ()
	{
		GetOperateSuit (OperateSuit.Up);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.up, 90f, singleanitime));
	}

	//U
	public void DoUP2 ()
	{
		GetOperateSuit (OperateSuit.Up);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.up, 180f, singleanitime * 2f));
	}

	//U'
	public void DoUN ()
	{
		GetOperateSuit (OperateSuit.Up);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.up, -90f, singleanitime));
	}

	#endregion

	#region D D2 D'

	//D
	public void DoDP ()
	{
		GetOperateSuit (OperateSuit.Down);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.down, 90f, singleanitime));
	}

	//D2
	public void DoDP2 ()
	{
		GetOperateSuit (OperateSuit.Down);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.down, 180f, singleanitime * 2f));
	}

	//D'
	public void DoDN ()
	{
		GetOperateSuit (OperateSuit.Down);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.down, -90f, singleanitime));
	}

	#endregion

	#region F F2 F'

	//F
	public void DoFP ()
	{
		GetOperateSuit (OperateSuit.Front);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.left, 90f, singleanitime));
	}

	//F
	public void DoFP2 ()
	{
		GetOperateSuit (OperateSuit.Front);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.left, 180f, singleanitime * 2f));
	}

	//F'
	public void DoFN ()
	{
		GetOperateSuit (OperateSuit.Front);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.left, -90f, singleanitime));
	}

	#endregion

	#region B B2 B'

	//B
	public void DoBP ()
	{
		GetOperateSuit (OperateSuit.Back);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.right, 90f, singleanitime));
	}

	//B
	public void DoBP2 ()
	{
		GetOperateSuit (OperateSuit.Back);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.right, 180f, singleanitime * 2f));
	}

	//B'
	public void DoBN ()
	{
		GetOperateSuit (OperateSuit.Back);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.right, -90f, singleanitime));
	}

	#endregion

	#endregion

	#region EntiretyStep 整体操作（旋转魔方）

	#region X X2 X'

	//X
	public void DoXP ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.back, 90f, singleanitime));
	}

	//X2
	public void DoXP2 ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.back, 180f, singleanitime * 2f));
	}

	//X'
	public void DoXN ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.back, -90f, singleanitime));
	}

	#endregion

	#region Y Y2 Y'

	//Y
	void DoYP ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.up, 90f, singleanitime));
	}

	//Y2
	void DoYP2 ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.up, 180f, singleanitime * 2f));
	}

	//Y'
	void DoYN ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.up, -90f, singleanitime));
	}

	#endregion

	#region Z Z2 Z'

	//Z
	void DoZP ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.left, 90f, singleanitime));
	}

	//Z2
	void DoZP2 ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.left, 180f, singleanitime * 2f));
	}

	//Z'
	void DoZN ()
	{
		GetOperateSuit (OperateSuit.Entriety);
		StartCoroutine (RotateAnimation (operatelist, Vector3.zero, Vector3.left, -90f, singleanitime));
	}

	#endregion

	#endregion

	#endregion

	#region Manual Operate

	public void ManualOperate (OperatePiece piece, Vector3 pos, GameObject singleboxobj)
	{
		//		curobj = singleboxobj;
		StartCoroutine (DoJudge (piece, pos));
	}

	// Judge the actual operate step
	IEnumerator DoJudge (OperatePiece piece, Vector3 pos)
	{
		rotating = true;
		Vector2 startpos = Input.mousePosition;
		Vector2 curpos = Input.mousePosition;
		judged = false;
		while (Input.GetMouseButton (0) && !judged) {
			curpos = Input.mousePosition;
			if (Vector2.Distance (curpos, startpos) >= 10f) {
				judged = true;
				//print ("judged");
			}
			yield return null;
		}
		// 记录转动的角度
		float rollangle = 0f;
		Vector2 lastmousepos = Input.mousePosition;
		Vector2 offset = Vector2.zero;
		float stepangle = 0f;

		Vector3 direction = Vector3.up;
		int suit = 0;

		switch (piece) {
		case OperatePiece.Left:
			if (Mathf.Abs (curpos.y - startpos.y) >= Mathf.Abs (curpos.x - startpos.x) * 1.4f) {
				//Slide up and down (alike x axis)
				if (pos.z < -0.5f) {
					GetOperateSuit (OperateSuit.Right);
				} else if (pos.z < 0.5f) {
					GetOperateSuit (OperateSuit.MiddleZ);
				} else {
					GetOperateSuit (OperateSuit.Left);
				}
				direction = Vector3.back;
				suit = 1;
			} else {
				//Slide left and right (alike y axis)
				if (pos.y < -0.5f) {
					GetOperateSuit (OperateSuit.Down);
				} else if (pos.y < 0.5f) {
					GetOperateSuit (OperateSuit.MiddleY);
				} else {
					GetOperateSuit (OperateSuit.Up);
				}
				direction = Vector3.down;
				suit = 2;
			}
			break;
		case OperatePiece.Right:
			if (Mathf.Abs (curpos.y - startpos.y) >= Mathf.Abs (curpos.x - startpos.x) * 1.4f) {
				//Slide up and down (alike z axis)
				if (pos.x < -0.5f) {
					GetOperateSuit (OperateSuit.Front);
				} else if (pos.x < 0.5f) {
					GetOperateSuit (OperateSuit.MiddleX);
				} else {
					GetOperateSuit (OperateSuit.Back);
				}
				direction = Vector3.right;
				suit = 1;
			} else {
				//Slide left and right (alike y axis)
				if (pos.y < -0.5f) {
					GetOperateSuit (OperateSuit.Down);
				} else if (pos.y < 0.5f) {
					GetOperateSuit (OperateSuit.MiddleY);
				} else {
					GetOperateSuit (OperateSuit.Up);
				}
				direction = Vector3.down;
				suit = 2;
			}
			break;
		case OperatePiece.Top:
			if ((curpos.y - startpos.y) * (curpos.x - startpos.x) >= 0f) {
				//Slide alike x axis
				if (pos.z < -0.5f) {
					GetOperateSuit (OperateSuit.Right);
				} else if (pos.z < 0.5f) {
					GetOperateSuit (OperateSuit.MiddleZ);
				} else {
					GetOperateSuit (OperateSuit.Left);
				}
				direction = Vector3.back;
				suit = 2;
			} else {
				//Slide alike z axis
				if (pos.x < -0.5f) {
					GetOperateSuit (OperateSuit.Front);
				} else if (pos.x < 0.5f) {
					GetOperateSuit (OperateSuit.MiddleX);
				} else {
					GetOperateSuit (OperateSuit.Back);
				}
				direction = Vector3.left;
				suit = 3;
			}
			break;
		}

		while (Input.GetMouseButton (0)) {
			offset = new Vector2 (Input.mousePosition.x - lastmousepos.x, Input.mousePosition.y - lastmousepos.y);

			if (suit.Equals (1)) {
				stepangle = offset.y * rlength;
			} else if (suit.Equals (2)) {
				stepangle = (offset.x * 0.86f + offset.y * 0.25f) * rlength;
			} else if (suit.Equals (3)) {
				stepangle = (offset.x * 0.86f - offset.y * 0.25f) * rlength;
			}

			rollangle += stepangle;
			foreach (SingleCube b in operatelist) {
				b.transform.RotateAround (Vector3.zero, direction, stepangle);
			}
			lastmousepos = Input.mousePosition;
			yield return null;
		}

		float targetangle = 0f;
		if (rollangle > 0) {
			targetangle = ((int)((rollangle + 45f) / 90f)) * 90f;
		} else if (rollangle < 0) {
			targetangle = ((int)((rollangle - 45f) / 90f)) * 90f;
		}

		float offsetangle = targetangle - rollangle;

		float speed = 90f / singleanitime;
		//		bool toupper = true;
		if (offsetangle < 0) {
			//			toupper = false;
			speed *= -1f;
		}

		bool finished = false;

		float curoffsetangle = 0f;
		float lastoffsetangle = 0f;
		float truthoffsetangle = 0f;

		while (!finished) {
			float o = speed * Time.deltaTime;
			curoffsetangle += o;
			if (Mathf.Abs (curoffsetangle) > Mathf.Abs (offsetangle)) {
				curoffsetangle = offsetangle;
				finished = true;
				//print ("Finished!!!!!!!");
			}
			truthoffsetangle = curoffsetangle - lastoffsetangle;
			foreach (SingleCube b in operatelist) {
				b.transform.RotateAround (Vector3.zero, direction, truthoffsetangle);
			}
			lastoffsetangle = curoffsetangle;
			yield return null;
		}

		foreach (SingleCube b in operatelist) {
			b.AdjustPos ();
		}

		rotating = false;
	}

	#endregion

	IEnumerator RotateAnimation (List<SingleCube> olist, Vector3 point, Vector3 axis, float angle, float length)
	{
		rotating = true;
		float t = 0f;
		float o = 0f;
		float lt = 0f;
		if (length > 0f) {
			float s = angle / length;
			while (t < length) {
				t += Time.deltaTime;
				if (t > length) {
					t = length;
				}
				o = (t - lt) * s;
				foreach (SingleCube b in olist) {
					b.transform.RotateAround (point, axis, o);
				}
				lt = t;
				yield return null;
			}
		} else {
			foreach (SingleCube b in olist) {
				b.transform.RotateAround (point, axis, angle);
			}
			yield return null;
		}
		// 或许可以不需要
		foreach (SingleCube b in olist) {
			b.AdjustPos ();
		}
		rotating = false;
	}

	// Find operate suit. 找到当前操作的块儿组
	void GetOperateSuit (OperateSuit str)
	{
		
		switch (str) {
		case OperateSuit.Left:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.z > 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Right:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.z < -0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Up:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.y > 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Down:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.y < -0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Front:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.x < -0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Back:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.x > 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Entriety:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				operatelist.Add (b);
			}
			break;
		case OperateSuit.MiddleX:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.x >= -0.5f && b.transform.position.x <= 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.MiddleY:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.y >= -0.5f && b.transform.position.y <= 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.MiddleZ:
			operatelist.Clear ();
			foreach (SingleCube b in singlecubes) {
				if (b.transform.position.z >= -0.5f && b.transform.position.z <= 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		}
	}

	// 拾取颜色
	public void PickColor (MagicColor col)
	{
		curColor = col;
	}


	public string formula = "";
	public Text formulatext;

	public float spacetime = 1f;



	OperateStep GetString2Step (string val)
	{
		OperateStep step = OperateStep.L;
		foreach (OperateStep s in MyMapPrefab.Step2StringMap.Keys) {
			if (MyMapPrefab.Step2StringMap [s].Equals (val)) {
				step = s;
				break;
			}
		}
		return step;
	}

	#region Edit Formular

	public void FormularAdd (OperateStep onestep)
	{
		if (MyMapPrefab.Step2StringMap.ContainsKey (onestep)) {
			if (string.IsNullOrEmpty (formula)) {
				formula += MyMapPrefab.Step2StringMap [onestep];
			} else {

				char[] charSeparator = new char[] { ',' };
				string[] l = formula.Split (charSeparator, System.StringSplitOptions.RemoveEmptyEntries);

				// 获得上一步存储的步骤序号
				int ps = (int)GetString2Step (l [l.Length - 1]);
				int cs = (int)onestep;

				// TODO: 检查新步骤是否与上一步骤有冲突，进行公式合并。
				if ((cs / 3) == (ps / 3)) {
					FormularDelete ();
					if ((cs % 3) == (ps % 3)) {
						if (cs % 3 == 0) { // 相加等于2
							FormularAdd ((OperateStep)(cs + 1));
						} else if (cs % 3 == 2) {
							FormularAdd ((OperateStep)(cs - 1));
						}
					} else {
						if (cs % 3 == 1) {
							FormularAdd ((OperateStep)(cs * 2 - ps));
						} else if (ps % 3 == 1) {
							FormularAdd ((OperateStep)(ps * 2 - cs));
						}
					}
				} else {
					formula += "," + MyMapPrefab.Step2StringMap [onestep];
				}
			}
		}
		if (formulatext != null)
			formulatext.text = formula;
	}

	public void FormularDelete ()
	{
		if (!string.IsNullOrEmpty (formula)) {
			if (formula.Contains (",")) {
				int i = formula.LastIndexOf (',');
				formula = formula.Remove (i);
			} else {
				formula = "";
			}
		}
		if (formulatext != null)
			formulatext.text = formula;
	}

	// 执行公式
	public void DoMyFormula ()
	{
		StartCoroutine (DoFormula ());
	}

	IEnumerator DoFormula ()
	{
		List<string> mysteps = new List<string> ();

		string context = formula;
		char[] charSeparator = new char[] { ',' };
		string[] l = context.Split (charSeparator, System.StringSplitOptions.RemoveEmptyEntries);
		mysteps.AddRange (l);
//		for (int i = 0; i < l.Length; i++) {
//			mysteps.Add (l [i]);
//		}

		while (!string.IsNullOrEmpty (formula) && mysteps.Count > 0) {
			if (this.Stop) {
				// 等待1秒，如果是新手，应该多加等待
				yield return new WaitForSeconds (spacetime);
				// 找到map的第一个公式
				OperateStep curstep = GetString2Step (mysteps [0]);
				DoSingleStep (curstep);
				
				// 去掉公式的第一个操作字符串，生成剩下的公式字符串（适用于用户自己编写的公式）（盲拧）
				if (mysteps.Count > 1) {
					formula = formula.Remove (0, mysteps [0].Length + 1);
				} else {
					formula = formula.Remove (0, mysteps [0].Length);
				}
				mysteps.Remove (mysteps [0]);
			}
			if (formulatext != null)
				formulatext.text = formula;
			yield return null;
		}
	}


	// creat broken formular and the recover formular 适用于自动打乱
	private string astr = "";
	private string bstr = "";

	public void CreatBrokeFormula ()
	{
		int lf = Random.Range (1, 21);// length of formula
		int si = -1;// 记录当前公式的序号
		string fb = ""; // 生成的打乱公式
		string fr = ""; // 生成的还原公式

		for (int i = 0; i < lf;) {
			int cs = Random.Range (0, 18);
			if ((int)(cs / 3) != (int)(si / 3)) {
				si = cs;

				OperateStep onestep = (OperateStep)cs;
				if (string.IsNullOrEmpty (fb)) {
					fb += MyMapPrefab.Step2StringMap [onestep];
				} else {
					fb += "," + MyMapPrefab.Step2StringMap [onestep];
				}

				int rs = (int)(cs / 3) * 3 + (2 - cs % 3);
				OperateStep restep = (OperateStep)rs;

				if (string.IsNullOrEmpty (fr)) {
					fr = MyMapPrefab.Step2StringMap [restep];
				} else {
					fr = MyMapPrefab.Step2StringMap [restep] + "," + fr;
				}

				i++;
			}
		}
		astr = fb;
		bstr = fr;
	}

	#endregion

}