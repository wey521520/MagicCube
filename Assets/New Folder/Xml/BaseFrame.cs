using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public enum BaseFrameType
{
	bfBaseFrame,
	//Unknow.
	bfPanel,
	bfScrollPanel,
	bfImage,
	bfButton,
	bfText,
	bfPrefab
}



public class BaseFrame
{

	#region Write in XML

	public string uiname;
	public string res;

	// the mode of fill or wrap and rest(but rest could only be used in the last ui)
	// the percentage of parent size
	// fixed size in dpi (unit)
	public string layout_width;
	public string layout_height;

	// the weight of wrap mode. If wrap, fill the parent.
	public float layout_weight;

	// topleft,topcenter,toptight,middleleft,middlecenter,
	// middleright,bottomleft,bottomcenter,bottomright
	public string layout_anchor;

	// 这个是用来裁剪大小的，但是如果上下都加一个等额的裁剪的话，相当于偏移
	// margin in dpi or percentage. >0 inner, <0 outer.
	public string layout_offsetleft;
	public string layout_offsetright;
	public string layout_offsettop;
	public string layout_offsetbottom;

	// background texture
	public string background;
	public string background_color;
	public string background_type;
	public string background_aspect;

	// horizontal,vertical,relative or ...
	public string distribution;

	public BaseFrame parent = null;
	public List<BaseFrame> childitems = new List<BaseFrame> ();
	// can not set. Used for children set.

	#endregion

	public BaseFrameType type = BaseFrameType.bfBaseFrame;
	public string maskable = "false";


	public string addscript;

	public BaseFrameEventList events = new BaseFrameEventList ();
	public DefaultBaseFrameEventList defaultevents = new DefaultBaseFrameEventList ();
	//add.14.12.11
	public CustomerEventList customerevents = new CustomerEventList ();
	//add.15.1.26
	public Dictionary<string,string> customerdefined = new Dictionary<string, string> ();
}


public class bfPanel : BaseFrame
{
	public bfPanel ()
	{
		type = BaseFrameType.bfPanel;
	}
}

public class bfScrollPanel : BaseFrame
{
	public bfScrollPanel ()
	{
		type = BaseFrameType.bfScrollPanel;
	}

	public string scrolldirection;
}

public class bfButton : BaseFrame
{
	public bfButton ()
	{
		type = BaseFrameType.bfButton;
	}

	public string text;
	public string textColor;
	public string textSize;
	//sp
	public string textAlignment;
	public string textFontStyle;
}

public class bfImage : BaseFrame
{
	public bfImage ()
	{
		type = BaseFrameType.bfImage;
	}

	public string preserveaspect;
}

public class bfText : BaseFrame
{
	public bfText ()
	{
		type = BaseFrameType.bfText;
	}

	public string text;
	public string textColor;
	public string textSize;
	//sp
	public string textAlignment;
	public string textFontStyle;
}

public class bfPrefab : BaseFrame
{
	public bfPrefab ()
	{
		type = BaseFrameType.bfPrefab;
	}
}

