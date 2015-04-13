using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
	public OperateStep mystep;
	private MagicCubeOperate operater;
	private EditFormula editor;

	void Start ()
	{
		operater = FindObjectOfType <MagicCubeOperate> ();
		editor = FindObjectOfType<EditFormula> ();
		GetComponent <Button> ().onClick.AddListener (OnClick);
	}

	public void OnClick ()
	{
		switch (operater.mystate) {
		case State.Operate:
			operater.DoSingleStep (mystep);
			break;
		case State.EditFormula:
			editor.Add (mystep);
			break;
		case State.OperateAndFormula:
			operater.DoSingleStep (mystep);
			editor.Add (mystep);
			break;
		case State.EditColor:
			operater.DoSingleStep (mystep);
			break;
		}
	}
}
