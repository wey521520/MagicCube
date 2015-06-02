using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateButton : MonoBehaviour
{

	public State state;
	public Text statetext;

	public void Click ()
	{
		FindObjectOfType <MyMagicCube> ().SwitchState (state);
		if (state == State.Operate) {
			statetext.text = "自由操作";
		} else if (state == State.EditColor) {
			statetext.text = "编辑颜色";
		} else if (state == State.EditFormula) {
			statetext.text = "编辑公式";
		}
	}
}
