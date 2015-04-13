using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicCubeOperate : MonoBehaviour
{

	SingleBox[] singleboxes;
	List<SingleBox> operatelist = new List<SingleBox> ();


	public State mystate = State.OperateAndFormula;

	private bool rotating;

	public bool Stop{ get { return !rotating; } }

	private bool judged;

	public  float singleanitime = 0.4f;
	private float rlength;

	//	private GameObject curobj;
	private float rollangle;

	public EditFormula editmyformula;

	// 当前编辑所选中的颜色
	private MagicColor curColor = MagicColor.White;

	public MagicColor EditColor { get { return curColor; } }

	//	public Dictionary<

	// Use this for initialization
	void Start ()
	{
		FindBoxes ();
		rlength = 180f / Screen.width / 0.23f / 3.14f;
	}

	void FindBoxes ()
	{
		singleboxes = FindObjectsOfType <SingleBox> ();
	}

	// Update is called once per frame
	void Update ()
	{
		#region For Test

		if (Input.GetKeyDown (KeyCode.L) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoLP2 ();
				editmyformula.Add (OperateStep.L2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoLN ();
				editmyformula.Add (OperateStep.L0);
			} else {
				DoLP ();
				editmyformula.Add (OperateStep.L);
			}
		}

		if (Input.GetKeyDown (KeyCode.R) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoRP2 ();
				editmyformula.Add (OperateStep.R2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoRN ();
				editmyformula.Add (OperateStep.R0);
			} else {
				DoRP ();
				editmyformula.Add (OperateStep.R);
			}
		}

		if (Input.GetKeyDown (KeyCode.U) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoUP2 ();
				editmyformula.Add (OperateStep.U2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoUN ();
				editmyformula.Add (OperateStep.U0);
			} else {
				DoUP ();
				editmyformula.Add (OperateStep.U);
			}
		}

		if (Input.GetKeyDown (KeyCode.D) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoDP2 ();
				editmyformula.Add (OperateStep.D2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoDN ();
				editmyformula.Add (OperateStep.D0);
			} else {
				DoDP ();
				editmyformula.Add (OperateStep.D);
			}
		}

		if (Input.GetKeyDown (KeyCode.F) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoFP2 ();
				editmyformula.Add (OperateStep.F2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoFN ();
				editmyformula.Add (OperateStep.F0);
			} else {
				DoFP ();
				editmyformula.Add (OperateStep.F);
			}
		}

		if (Input.GetKeyDown (KeyCode.B) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoBP2 ();
				editmyformula.Add (OperateStep.B2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoBN ();
				editmyformula.Add (OperateStep.B0);
			} else {
				DoBP ();
				editmyformula.Add (OperateStep.B);
			}
		}

		if (Input.GetKeyDown (KeyCode.X) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoXP2 ();
				editmyformula.Add (OperateStep.X2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoXN ();
				editmyformula.Add (OperateStep.X0);
			} else {
				DoXP ();
				editmyformula.Add (OperateStep.X);
			}
		}

		if (Input.GetKeyDown (KeyCode.Y) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoYP2 ();
				editmyformula.Add (OperateStep.Y2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoYN ();
				editmyformula.Add (OperateStep.Y0);
			} else {
				DoYP ();
				editmyformula.Add (OperateStep.Y);
			}
		}

		if (Input.GetKeyDown (KeyCode.Z) && !rotating) {
			if (Input.GetKey (KeyCode.LeftAlt)) {
				DoZP2 ();
				editmyformula.Add (OperateStep.Z2);
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				DoZN ();
				editmyformula.Add (OperateStep.Z0);
			} else {
				DoZP ();
				editmyformula.Add (OperateStep.Z);
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
		rollangle = 0f;
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
			foreach (SingleBox b in operatelist) {
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
		//print ("<|||||>" + offsetangle + "<|||||>" + targetangle + "<|||||>" + rollangle);

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
			foreach (SingleBox b in operatelist) {
				b.transform.RotateAround (Vector3.zero, direction, truthoffsetangle);
			}
			lastoffsetangle = curoffsetangle;
			yield return null;
		}

		foreach (SingleBox b in operatelist) {
			b.AdjustPos ();
		}

		rotating = false;
	}

	#endregion

	IEnumerator RotateAnimation (List<SingleBox> olist, Vector3 point, Vector3 axis, float angle, float length)
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
				foreach (SingleBox b in olist) {
					b.transform.RotateAround (point, axis, o);
				}
				lt = t;
				yield return null;
			}
		} else {
			foreach (SingleBox b in olist) {
				b.transform.RotateAround (point, axis, angle);
			}
			yield return null;
		}
		// 或许可以不需要
		foreach (SingleBox b in olist) {
			b.AdjustPos ();
		}
		rotating = false;
	}

	// Find operate suit. 找到当前操作的块儿组
	void GetOperateSuit (OperateSuit str)
	{
		if (singleboxes.Length < 1) {
			FindBoxes ();
		}
		switch (str) {
		case OperateSuit.Left:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.z.Equals (1)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.z > 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Right:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.z.Equals (-1)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.z < -0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Up:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.y.Equals (1)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.y > 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Down:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.y.Equals (-1)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.y < -0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Front:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.x.Equals (-1)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.x < -0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Back:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.x.Equals (1)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.x > 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.Entriety:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
				operatelist.Add (b);
			}
			break;
		case OperateSuit.MiddleX:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.x.Equals (0)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.x >= -0.5f && b.transform.position.x <= 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.MiddleY:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.y.Equals (0)) {
//					operatelist.Add (b);
//				}
				if (b.transform.position.y >= -0.5f && b.transform.position.y <= 0.5f) {
					operatelist.Add (b);
				}
			}
			break;
		case OperateSuit.MiddleZ:
			operatelist.Clear ();
			foreach (SingleBox b in singleboxes) {
//				if (b.transform.position.z.Equals (0)) {
//					operatelist.Add (b);
//				}
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
}
