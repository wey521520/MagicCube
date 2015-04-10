using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EditFormula : MonoBehaviour
{
	public MagicCubeOperate operater;

	public string formula = "";
	public Text formulatext;

	Dictionary<OperateStep,string> stepmap = new Dictionary<OperateStep, string> ();

	void InitMap ()
	{
		stepmap.Add (OperateStep.L, "L");
		stepmap.Add (OperateStep.L0, "L'");
		stepmap.Add (OperateStep.L2, "L2");

		stepmap.Add (OperateStep.R, "R");
		stepmap.Add (OperateStep.R0, "R'");
		stepmap.Add (OperateStep.R2, "R2");

		stepmap.Add (OperateStep.U, "U");
		stepmap.Add (OperateStep.U0, "U'");
		stepmap.Add (OperateStep.U2, "U2");

		stepmap.Add (OperateStep.D, "D");
		stepmap.Add (OperateStep.D0, "D'");
		stepmap.Add (OperateStep.D2, "D2");

		stepmap.Add (OperateStep.F, "F");
		stepmap.Add (OperateStep.F0, "F'");
		stepmap.Add (OperateStep.F2, "F2");

		stepmap.Add (OperateStep.B, "B");
		stepmap.Add (OperateStep.B0, "B'");
		stepmap.Add (OperateStep.B2, "B2");

		stepmap.Add (OperateStep.X, "X");
		stepmap.Add (OperateStep.X0, "X'");
		stepmap.Add (OperateStep.X2, "X2");

		stepmap.Add (OperateStep.Y, "Y");
		stepmap.Add (OperateStep.Y0, "Y'");
		stepmap.Add (OperateStep.Y2, "Y2");

		stepmap.Add (OperateStep.Z, "Z");
		stepmap.Add (OperateStep.Z0, "Z'");
		stepmap.Add (OperateStep.Z2, "Z2");
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
				formula += "," + stepmap [onestep];
			}
		}
		//print (formula);
		formulatext.text = formula;
	}

	public void Delete ()
	{
		if (!string.IsNullOrEmpty (formula)) {
			int i = formula.LastIndexOf (',');
			formula = formula.Remove (i);
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
		print (mysteps.Count);
		while (!string.IsNullOrEmpty (formula) && mysteps.Count > 0) {
			if (operater.Stop) {
				OperateStep curstep = OperateStep.U;
				foreach (OperateStep step in stepmap.Keys) {
					if (stepmap [step] == mysteps [0]) {
						curstep = step;
					}
				}
				operater.DoSingleStep (curstep);
				if (mysteps.Count > 1) {
					formula = formula.Remove (0, mysteps [0].Length + 1);
				} else {
					formula = formula.Remove (0, mysteps [0].Length);
				}
				print (formula);
				mysteps.Remove (mysteps [0]);
			}
			formulatext.text = formula;
			yield return null;
		}
	}

	//	void OnGUI ()
	//	{
	//		if (GUILayout.Button ("DoMyFormula")) {
	//			StartCoroutine (DoFormula ());
	//		}
	//		if (GUILayout.Button ("Delete")) {
	//			Delete ();
	//		}
	//		GUILayout.Label ("\t\t\t" + formula);
	//	}

}
