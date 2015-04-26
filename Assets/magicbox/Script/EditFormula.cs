using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EditFormula : MonoBehaviour
{
	public MagicCubeOperate operater;

	public string formula = "";
	public Text formulatext;

	public float spacetime = 1f;

	Dictionary<OperateStep,string> stepmap = new Dictionary<OperateStep, string> ();

	void InitMap ()
	{
		stepmap.Add (OperateStep.L, "L");
		stepmap.Add (OperateStep.L2, "L2");
		stepmap.Add (OperateStep.L0, "L'");

		stepmap.Add (OperateStep.R, "R");
		stepmap.Add (OperateStep.R2, "R2");
		stepmap.Add (OperateStep.R0, "R'");

		stepmap.Add (OperateStep.U, "U");
		stepmap.Add (OperateStep.U2, "U2");
		stepmap.Add (OperateStep.U0, "U'");

		stepmap.Add (OperateStep.D, "D");
		stepmap.Add (OperateStep.D2, "D2");
		stepmap.Add (OperateStep.D0, "D'");

		stepmap.Add (OperateStep.F, "F");
		stepmap.Add (OperateStep.F2, "F2");
		stepmap.Add (OperateStep.F0, "F'");

		stepmap.Add (OperateStep.B, "B");
		stepmap.Add (OperateStep.B2, "B2");
		stepmap.Add (OperateStep.B0, "B'");

		stepmap.Add (OperateStep.X, "X");
		stepmap.Add (OperateStep.X2, "X2");
		stepmap.Add (OperateStep.X0, "X'");

		stepmap.Add (OperateStep.Y, "Y");
		stepmap.Add (OperateStep.Y2, "Y2");
		stepmap.Add (OperateStep.Y0, "Y'");

		stepmap.Add (OperateStep.Z, "Z");
		stepmap.Add (OperateStep.Z2, "Z2");
		stepmap.Add (OperateStep.Z0, "Z'");
	}

	OperateStep GetString2Step (string val)
	{
		OperateStep step = OperateStep.B;
		foreach (OperateStep s in stepmap.Keys) {
			if (stepmap [s].Equals (val)) {
				step = s;
				break;
			}
		}
		return step;
	}

	List<string> mysteps = new List<string> ();

	void Awake ()
	{
		InitMap ();
	}

	public void Add (OperateStep onestep)
	{
		if (stepmap.ContainsKey (onestep)) {
			if (string.IsNullOrEmpty (formula)) {
				formula += stepmap [onestep];
			} else {

				char[] charSeparator = new char[] { ',' };
				string[] l = formula.Split (charSeparator, System.StringSplitOptions.RemoveEmptyEntries);

				// 获得上一步存储的步骤序号
				int ps = (int)GetString2Step (l [l.Length - 1]);
				int cs = (int)onestep;

				// TODO: 检查新步骤是否与上一步骤有冲突，进行公式合并。
				if ((cs / 3) == (ps / 3)) {
					Delete ();
					if ((cs % 3) == (ps % 3)) {
						if (cs % 3 == 0) { // 相加等于2
							Add ((OperateStep)(cs + 1));
						} else if (cs % 3 == 2) {
							Add ((OperateStep)(cs - 1));
						}
					} else {
						if (cs % 3 == 1) {
							Add ((OperateStep)(cs * 2 - ps));
						} else if (ps % 3 == 1) {
							Add ((OperateStep)(ps * 2 - cs));
						}
					}
				} else {
					formula += "," + stepmap [onestep];
				}
			}
		}
		formulatext.text = formula;
	}

	public void Delete ()
	{
		if (!string.IsNullOrEmpty (formula)) {
			if (formula.Contains (",")) {
				int i = formula.LastIndexOf (',');
				formula = formula.Remove (i);
			} else {
				formula = "";
			}
		}
		formulatext.text = formula;
	}

	public void DoMyFormula ()
	{
		StartCoroutine (DoFormula ());
	}

	IEnumerator DoFormula ()
	{
		mysteps.Clear ();
		string context = formula;
//		context = context.Replace ("\n", "");
//		context = context.Replace ("\r", "");
//		context = context.Replace ("\t", "");
//		context = context.Replace (" ", "");
		char[] charSeparator = new char[] { ',' };
		string[] l = null;
		l = context.Split (charSeparator, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < l.Length; i++) {
			mysteps.Add (l [i]);
		}
		while (!string.IsNullOrEmpty (formula) && mysteps.Count > 0) {
			if (operater.Stop) {
				// 等待1秒，如果是新手，应该多加等待
				yield return new WaitForSeconds (spacetime);
				// 找到map的第一个公式
				OperateStep curstep = GetString2Step (mysteps [0]);// OperateStep.U;
//				foreach (OperateStep step in stepmap.Keys) {
//					if (stepmap [step] == mysteps [0]) {
//						curstep = step;
//						break;
//					}
//				}

				operater.DoSingleStep (curstep);
				// 去掉公式的第一个操作字符串，生成剩下的公式字符串（适用于用户自己编写的公式）（盲拧）
				if (mysteps.Count > 1) {
					formula = formula.Remove (0, mysteps [0].Length + 1);
				} else {
					formula = formula.Remove (0, mysteps [0].Length);
				}
				mysteps.Remove (mysteps [0]);
			}
			formulatext.text = formula;
			yield return null;
		}
	}

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
					fb += stepmap [onestep];
				} else {
					fb += "," + stepmap [onestep];
				}

				int rs = (int)(cs / 3) * 3 + (2 - cs % 3);
				OperateStep restep = (OperateStep)rs;

				if (string.IsNullOrEmpty (fr)) {
					fr = stepmap [restep];
				} else {
					fr = stepmap [restep] + "," + fr;
				}

				i++;
			}
		}
		astr = fb;
		bstr = fr;
		print (fb);
		print (fr);
	}

	void OnGUI ()
	{
		if (GUILayout.Button ("DoMyFormula")) {
			StartCoroutine (DoFormula ());
		}
		if (GUILayout.Button ("Delete")) {
			Delete ();
		}
		GUILayout.Label ("\t\t\t" + formula);

		if (GUILayout.Button ("CreatBrokeFormula")) {
			CreatBrokeFormula ();
		}
		if (GUILayout.Button ("DoBrokeFormula")) {
			formula = astr;
			spacetime = 0f;
			operater.singleanitime = 0f;
			StartCoroutine (DoFormula ());
		}
		if (GUILayout.Button ("DoRecoverFormula")) {
			formula = bstr;
			spacetime = 0.5f;
			operater.singleanitime = 0.4f;
			StartCoroutine (DoFormula ());
		}
		GUILayout.Label ("\t\t\t" + astr);
		GUILayout.Label ("\t\t\t" + bstr);
	}

}
