using UnityEngine;
using System.Collections;

public class CubeFace : MonoBehaviour
{
	//惰性距离，滑动不动的距离
	private const float inertDistance = 0.05f;

	private MyMagicCube operater;

	public SingleCube cube;

	public CubeFaceStyle facestyle = CubeFaceStyle.None;

	public MagicColor mycolor;

	void Start ()
	{
		operater = FindObjectOfType <MyMagicCube> ();
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
			if (!cube.cubestyle.Equals (CubeStyle.Face)) {
				cube.SetColor (this.gameObject);
			}
			break;
		}
	}

	// 根据位置来设置面片的颜色，适用于自动重新设置魔方颜色
	public void SetDefaultColor ()
	{
		this.mycolor = MyMapPrefab.StyleColorMap [facestyle];
		this.GetComponent <MeshRenderer> ().material.color = MyMapPrefab.ColorMap [MyMapPrefab.StyleColorMap [facestyle]];
	}

	// 用户自定义编辑颜色（添加颜色之后应该同步修改块儿的记录）（编辑颜色要判断颜色的个数是否过多和组合是否重复）
	public void SetEditColor (MagicColor pickedcolor)
	{
		operater.ColorDelete (mycolor);
		this.GetComponent <MeshRenderer> ().material.color = MyMapPrefab.ColorMap [pickedcolor];
		mycolor = pickedcolor;
		operater.ColorAdd ();

		cube.SetColor (this.gameObject);
	}

	public void UpdateFaceStyle ()
	{
		Vector3 pos = transform.position;
		if (pos.x < -1.25f) {
			this.facestyle = CubeFaceStyle.Front;
		} else if (pos.x > 1.25f) {
			this.facestyle = CubeFaceStyle.Back;
		} else if (pos.y > 1.25f) {
			this.facestyle = CubeFaceStyle.Up;
		} else if (pos.y < -1.25f) {
			this.facestyle = CubeFaceStyle.Down;
		} else if (pos.z < -1.25f) {
			this.facestyle = CubeFaceStyle.Right;
		} else if (pos.z > 1.25f) {
			this.facestyle = CubeFaceStyle.Left;
		} else {
			this.facestyle = CubeFaceStyle.None;
		}
	}

}
