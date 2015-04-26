using UnityEngine;
using System.Collections;

public class SingleBoxPiece : MonoBehaviour
{
	//惰性距离，滑动不动的距离
	private const float inertDistance = 0.05f;

	private MagicCubeOperate operater;

	public SingleBox box;

	void Start ()
	{
		operater = FindObjectOfType <MagicCubeOperate> ();
	}

	void OnMouseDown ()
	{
		if (!this.isActiveAndEnabled) {
			return;
		}
		switch (operater.mystate) {
		case State.Operate:
			if (operater.Stop) {
				//print ("Current face pos :" + this.transform.position);
				if (this.transform.position.x <= (-1.25f)) {
					operater.ManualOperate (OperatePiece.Left, 
						this.transform.position, this.transform.parent.gameObject);
				} else if (this.transform.position.y >= (1.25f)) {
					operater.ManualOperate (OperatePiece.Top, 
						this.transform.position, this.transform.parent.gameObject);
				} else if (this.transform.position.z <= (-1.25f)) {
					operater.ManualOperate (OperatePiece.Right, 
						this.transform.position, this.transform.parent.gameObject);
				}
			}
			break;																																																																																																																																																																																																																																																																								
		case State.EditColor:
			if (!box.myStyle.Equals (CubeStyle.Face)) {
				box.SetColor (this.gameObject);
			}
			break;
		}
	}
}
