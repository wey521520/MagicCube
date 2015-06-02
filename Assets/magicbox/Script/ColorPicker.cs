using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{

	public MagicColor mycolor;
	//	private MagicCubeOperate operater;
	private MyMagicCube operater;

	void Start ()
	{
		//		operater = FindObjectOfType <MagicCubeOperate> ();
		operater = FindObjectOfType <MyMagicCube> ();
		GetComponent <Button> ().onClick.AddListener (OnClick);
	}

	public void OnClick ()
	{
		if (operater.mystate == State.EditColor) {
			operater.PickColor (mycolor);
		}
	}
}
