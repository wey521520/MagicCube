using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleCube : MonoBehaviour
{
	// for edit color
	public Dictionary<GameObject,MagicColor> cubecolor = new Dictionary<GameObject, MagicColor> ();
	// 块的类型，一旦设置将不再进行修改
	public CubeStyle cubestyle = CubeStyle.None;
	public int locationIndex;
	//	public int finishIndex;

	private MyMagicCube operater;

	private float size = 1f;

	public float Size { set { size = value; } }

	private bool editcolorfinished;

	void Start ()
	{
		AdjustPos ();
		operater = FindObjectOfType <MyMagicCube> ();
	}

	#region AdjustPos

	public void AdjustPos ()
	{
		transform.position = AdjustPositionValue (transform.position);
		transform.eulerAngles = AdjustAngleValue (transform.eulerAngles);
	}

	// tools for adjust
	Vector3 AdjustPositionValue (Vector3 val)
	{
		return new Vector3 (
			val.x > 0.5f * size ? size : val.x < -0.5 * size ? -size : 0,
			val.y > 0.5f * size ? size : val.y < -0.5 * size ? -size : 0,
			val.z > 0.5f * size ? size : val.z < -0.5 * size ? -size : 0);
	}

	Vector3 AdjustAngleValue (Vector3 val)
	{
		return new Vector3 (GetValueGrade (val.x), GetValueGrade (val.y), GetValueGrade (val.z));
	}

	float GetValueGrade (float val)
	{
		float v = 0f;
		if (val <= -315f) {
			v = 0f;
		} else if (val <= -225f) {
			v = -270f;
		} else if (val <= -135f) {
			v = -180f;
		} else if (val <= -45f) {
			v = -90f;
		} else if (val <= 45f) {
			v = 0f;
		} else if (val <= 135f) {
			v = 90f;
		} else if (val <= 225f) {
			v = 180f;
		} else if (val <= 315f) {
			v = 270f;
		} else if (val > 315f) {
			v = 0f;
		}
		return v;
	}

	#endregion

	//	public void InitColor ()
	//	{
	//		SingleBoxPiece[] mypieces = GetComponentsInChildren <SingleBoxPiece> ();
	//		cubecolor.Clear ();
	//		foreach (SingleBoxPiece p in mypieces) {
	//			p.GetComponent <MeshRenderer> ().material.color = Color.gray;
	//			cubecolor.Add (p.gameObject, MagicColor.None);
	//		}
	//	}

	public void SetColor (GameObject p)
	{
		if (operater == null) {
			operater = FindObjectOfType <MyMagicCube> ();
		}
		MagicColor curcolor = operater.Color;
//		print (operater.EditColorStore [curcolor]);
		if (curcolor != MagicColor.None && operater.EditColorStore [curcolor] > 7) {
			//print ("此颜色已经编辑完成" + curcolor + operater.EditColorStore [curcolor]);
			return;
		}
		if (cubecolor.ContainsKey (p) && cubecolor [p] == curcolor) {
			//print ("颜色相同");
			return;
		}

		bool cansetcolor = true;

		if (editcolorfinished) {
			operater.ColorMapDelete (this);
		}

		MagicColor orignalcolor = MagicColor.None;
		// 如果字典里面有当前的物体，则为替换颜色，如果没有，则为添加颜色
		if (cubecolor.ContainsKey (p)) {
			orignalcolor = cubecolor [p];
			// 这些只是为了防止同一个块上出现两个相同的颜色和对面的颜色
			foreach (GameObject g in cubecolor.Keys) {
				if (g != p) {
					if (!cubecolor [g].Equals (MagicColor.None)) {
						if (cubecolor [g] == curcolor) {
							cansetcolor = false;
							//print ("此块已经有相同颜色，请勿重复编辑");
							break;
						}
						int index = (int)curcolor;
						if (index >= 3 && (int)cubecolor [g] == index - 3) {
							cansetcolor = false;
							//print ("颜色不匹配");
							break;
						} else if (index < 3 && (int)cubecolor [g] == index + 3) {
							cansetcolor = false;
							//print ("颜色不匹配");
							break;
						}
					}
				}
			}
		} else {
			cubecolor.Add (p, curcolor);
			//print ("添加新颜色");
		}

		// 这里先假设将颜色附上去，如果不合适再去掉
		if (cansetcolor) {
			cubecolor [p] = curcolor;
		} else {
			return;
		}

		// TODO: 判断是否完成当前cube的颜色设定，如果完成则保存到字典中，如果没有则等待完成
		if ((cubestyle == CubeStyle.Edge && cubecolor.Count == 2) || (cubestyle == CubeStyle.Corner && cubecolor.Count == 3)) {
			editcolorfinished = true;
		} else {
			editcolorfinished = false;
		}

		if (editcolorfinished) {
//			foreach (GameObject g in cubecolor.Keys) {
//				editcolorfinished = true;
//				if (g.GetComponent <CubeFace> ().mycolor == MagicColor.None) {
//					editcolorfinished = false;
//				}
//			}
			if (cubecolor.ContainsValue (MagicColor.None)) {
				editcolorfinished = false;
			}
		}

		if (editcolorfinished) {
			//print ("当前块的颜色已经设置完成");

			// TODO: 根据设定的颜色来标识当前cube，判断是否有重复
			CubeMark mark = GetCubeMark ();
			if (operater.EditColorMap.ContainsValue (mark) && mark != CubeMark.None) {
				// print ("已经有这样的颜色块啦，请不要编辑错误了哦" + mark);
				cubecolor [p] = orignalcolor;
			} else {
				// print ("颜色编辑成功" + mark);

				operater.ColorDelete (orignalcolor);
				operater.ColorAdd ();
				operater.ColorMapAdd (this, mark);
				p.GetComponent <MeshRenderer> ().material.color = MyMapPrefab.ColorMap [curcolor];
			}
		} else {
			// print ("当前块的颜色尚未设置完成");
			operater.ColorDelete (orignalcolor);
			operater.ColorAdd ();
			p.GetComponent <MeshRenderer> ().material.color = MyMapPrefab.ColorMap [curcolor];
		}
	}

	// 每次编辑颜色的时候对棱块和角块的颜色列表进行清除
	public void CleanCubeColor ()
	{
		if (cubestyle == CubeStyle.Edge || cubestyle == CubeStyle.Corner) {
			cubecolor.Clear ();
			editcolorfinished = false;
		}
	}

	// TODO: 根据设定的颜色来标识当前cube
	private CubeMark GetCubeMark ()
	{
		CubeMark mark = CubeMark.None;
		if (cubestyle == CubeStyle.Corner) {
			if (cubecolor.ContainsValue (MagicColor.White) &&
			    cubecolor.ContainsValue (MagicColor.Red) &&
			    cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.WRB;
			} else if (cubecolor.ContainsValue (MagicColor.White) &&
			           cubecolor.ContainsValue (MagicColor.Red) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.WRG;
			} else if (cubecolor.ContainsValue (MagicColor.White) &&
			           cubecolor.ContainsValue (MagicColor.Orange) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.WOG;
			} else if (cubecolor.ContainsValue (MagicColor.White) &&
			           cubecolor.ContainsValue (MagicColor.Orange) &&
			           cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.WBO;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Orange) &&
			           cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.YBO;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Orange) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.YOG;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Red) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.YRG;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Red) &&
			           cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.YRB;
			}
		}
		if (cubestyle == CubeStyle.Edge) {
			if (cubecolor.ContainsValue (MagicColor.White) &&
			    cubecolor.ContainsValue (MagicColor.Red)) {
				mark = CubeMark.WR;
			} else if (cubecolor.ContainsValue (MagicColor.White) &&
			           cubecolor.ContainsValue (MagicColor.Orange)) {
				mark = CubeMark.WO;
			} else if (cubecolor.ContainsValue (MagicColor.White) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.WG;
			} else if (cubecolor.ContainsValue (MagicColor.White) &&
			           cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.WB;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Orange)) {
				mark = CubeMark.YO;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Red)) {
				mark = CubeMark.YR;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.YB;
			} else if (cubecolor.ContainsValue (MagicColor.Yellow) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.YG;
			} else if (cubecolor.ContainsValue (MagicColor.Red) &&
			           cubecolor.ContainsValue (MagicColor.Blue)) {
				mark = CubeMark.BR;
			} else if (cubecolor.ContainsValue (MagicColor.Red) &&
			           cubecolor.ContainsValue (MagicColor.Green)) {
				mark = CubeMark.GR;
			} else if (cubecolor.ContainsValue (MagicColor.Green) &&
			           cubecolor.ContainsValue (MagicColor.Orange)) {
				mark = CubeMark.GO;
			} else if (cubecolor.ContainsValue (MagicColor.Blue) &&
			           cubecolor.ContainsValue (MagicColor.Orange)) {
				mark = CubeMark.BO;
			}
		}
		return mark;

	}
}