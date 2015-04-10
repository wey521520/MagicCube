using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class BaseFrameForUI : MonoBehaviour
{

	public BaseFrame baseFrame = null;

	public float weightsum;
	// can not set.

	public IList<BaseFrameForUI> childitems;
	// can not set. Used for children set.
	public BaseFrameForUI parent;
	// can not set. Used for children set.
	
	public float size_width;
	// can not set. Used for children set.
	public float size_height;
	// can not set. Used for children set.

	public float unchanged_width;
	//had never changed size for scroll panel
	public float unchanged_height;
	//had never changed size for scroll panel
	
	public Transform mytransform;
	
	///--------------------
	public float widthpct;
	public float heightpct;
	
	public Vector2 anchormin;
	public Vector2 anchormax;
	
	public float offsetleft;
	public float offsetright;
	public float offsettop;
	public float offsetbottom;
	
	public float dptopxrate = 1f;
	// Calaulated by Global.
	///--------------------
	
	public int mycount;

	// Read data from mxl.
	public void LoadFromBaseFrame (BaseFrame ui)
	{
		this.baseFrame = ui;
		mycount = this.baseFrame.customerdefined.Count;
		this.childitems = new List<BaseFrameForUI> ();
		this.weightsum = 0f;
		if (this.baseFrame.childitems != null && this.baseFrame.childitems.Count > 0) {
			for (int i = 0; i < this.baseFrame.childitems.Count; i++) {
				this.weightsum += this.baseFrame.childitems [i].layout_weight;
			}
		}
	}

	public void SetUIShow ()
	{
		if (!string.IsNullOrEmpty (baseFrame.background)) {
//			GetComponent<Image> ().sprite = Resources.Load<Sprite> (baseFrame.background);
			GetComponent<Image> ().sprite = Global.LoadSprite (baseFrame.background);
		}
		if (baseFrame.background_color != null) {
			Color32 c = new Color32 ();
			string s = baseFrame.background_color;

			c.r = Convert.ToByte (s.Substring (1, 2), 16);
			c.g = Convert.ToByte (s.Substring (3, 2), 16);
			c.b = Convert.ToByte (s.Substring (5, 2), 16);
			c.a = Convert.ToByte (s.Substring (7, 2), 16);

			GetComponent<Image> ().color = c;
		}
	}

	#region SubCalculate

	public void AdjustAnchor ()
	{
		// topleft,topcenter,toptight,middleleft,middlecenter,
		// middleright,bottomleft,bottomcenter,bottomright
		switch (this.baseFrame.layout_anchor) {
		
		case "topcenter":
			this.anchormin = new Vector2 (0.5f - this.widthpct * 0.5f, 1f - this.heightpct);
			this.anchormax = new Vector2 (this.widthpct * 0.5f + 0.5f, 1f);
			break;
		case "topright":
			this.anchormin = new Vector2 (1f - this.widthpct, 1f - this.heightpct);
			this.anchormax = new Vector2 (1f, 1f);
			break;
		case "middleleft":
			this.anchormin = new Vector2 (0f, 0.5f - this.heightpct * 0.5f);
			this.anchormax = new Vector2 (this.widthpct, this.heightpct * 0.5f + 0.5f);
			break;
		case "middlecenter":
			this.anchormin = new Vector2 (0.5f - this.widthpct * 0.5f, 0.5f - this.heightpct * 0.5f);
			this.anchormax = new Vector2 (this.widthpct * 0.5f + 0.5f, this.heightpct * 0.5f + 0.5f);
			break;
		case "middleright":
			this.anchormin = new Vector2 (1f - this.widthpct, 0.5f - this.heightpct * 0.5f);
			this.anchormax = new Vector2 (1f, this.heightpct * 0.5f + 0.5f);
			break;
		case "bottomleft":
			this.anchormin = new Vector2 (0f, 0f);
			this.anchormax = new Vector2 (this.widthpct, this.heightpct);
			break;
		case "bottomcenter":
			this.anchormin = new Vector2 (0.5f - this.widthpct * 0.5f, 0f);
			this.anchormax = new Vector2 (this.widthpct * 0.5f + 0.5f, this.heightpct);
			break;
		case "bottomright":
			this.anchormin = new Vector2 (1f - this.widthpct, 0f);
			this.anchormax = new Vector2 (1f, this.heightpct);
			break;
		case "topleft":
		default :
			this.anchormin = new Vector2 (0f, 1f - this.heightpct);
			this.anchormax = new Vector2 (this.widthpct, 1f);
			break;
		}
		
	}

	public void CalculateOffset ()
	{
		this.offsetleft = 0f;
		this.offsetright = 0f;
		this.offsettop = 0f;
		this.offsetbottom = 0f;

		float val = 0f;
		string value = this.baseFrame.layout_offsetleft;
		if (value == null) {
		} else if (IsNumberic (value, out val)) {
			if (this.parent != null) {
				this.offsetleft = val * this.parent.size_width;
			} else {
				this.offsetleft = val * Screen.width;
			}
		} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
			this.offsetleft = Global.dp2px (val);
		} else if (IsNumberic (value.Replace ("xp", ""), out val)) {
			this.offsetleft = val;
		}
		
		value = this.baseFrame.layout_offsetright;
		if (value == null) {
		} else if (IsNumberic (value, out val)) {
			if (this.parent != null) {
				this.offsetright = -val * this.parent.size_width;
			} else {
				this.offsetright = -val * Screen.width;
				throw new NullReferenceException ();
			}
		} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
			this.offsetright = -Global.dp2px (val);
		} else if (IsNumberic (value.Replace ("xp", ""), out val)) {
			this.offsetright = -val;
		}
		
		value = this.baseFrame.layout_offsetbottom;
		if (value == null) {
		} else if (IsNumberic (value, out val)) {
			if (this.parent != null) {
				this.offsetbottom = val * this.parent.size_height;
			} else {
				this.offsetbottom = val * Screen.height;
			}
		} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
			this.offsetbottom = Global.dp2px (val);
		} else if (IsNumberic (value.Replace ("xp", ""), out val)) {
			this.offsetbottom = val;
		}
		
		value = this.baseFrame.layout_offsettop;
		if (value == null) {
		} else if (IsNumberic (value, out val)) {
			if (this.parent != null) {
				this.offsettop = -val * this.parent.size_height;
			} else {
				this.offsettop = -val * Screen.height;
			}
		} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
			this.offsettop = -Global.dp2px (val);
		} else if (IsNumberic (value.Replace ("xp", ""), out val)) {
			this.offsettop = -val;
		}
	}

	public float PercentageOfParentWidth ()
	{
		float val = 0f;
		string value = this.baseFrame.layout_width;
		if (value == null) {
		} else if (value == "fill") {
			if (this.parent != null && this.parent.baseFrame.type == BaseFrameType.bfScrollPanel) {
				return this.parent.unchanged_width / this.parent.size_width;
			} else {
				return 1f;
			}
		} else if (value == "wrap") {
			// find the parent weight sum, and add the previous weight value.
			if (this.parent != null && this.parent.weightsum == 0f) {
				return 0f;
			}
			return this.baseFrame.layout_weight / this.parent.weightsum;
		} else if (value == "rest") {
			// find the parent weight sum, and add the previous weight value.
			return -1f;
		} else if (IsNumberic (value, out val)) {	// if percentage mode.
			if (this.parent != null && this.parent.baseFrame.type == BaseFrameType.bfScrollPanel) {
				return val * this.parent.unchanged_width / this.parent.size_width;
			} else {
				return val;
			}
		} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
			if (this.parent != null) {
				return Global.dp2px (val) / this.parent.size_width;
			} else {
				return Global.dp2px (val) / Screen.width;
			}
		} else if (IsNumberic (value.Replace ("xp", ""), out val)) {
			if (this.parent != null) {
				return val / this.parent.size_width;
			} else {
				return val / Screen.width;
			}
		}
		return 0f;
	}

	public float PercentageOfParentHeight ()
	{
		float val = 0f;
		string value = this.baseFrame.layout_height;
		if (value == null) {
		} else if (value == "fill") {
			if (this.parent != null && this.parent.baseFrame.type == BaseFrameType.bfScrollPanel) {
				return this.parent.unchanged_height / this.parent.size_height;
			} else {
				return 1f;
			}
		} else if (value == "wrap") {
			// find the parent weight sum, and add the previous weight value.
			if (this.parent != null && this.parent.weightsum == 0f) {
				return 0f;
			}
			return this.baseFrame.layout_weight / this.parent.weightsum;
		} else if (value == "rest") {
			// find the parent weight sum, and add the previous weight value.
			return -1f;
		} else if (IsNumberic (value, out val)) {	// if percentage mode.
			if (this.parent != null && this.parent.baseFrame.type == BaseFrameType.bfScrollPanel) {
				return val * this.parent.unchanged_height / this.parent.size_height;
			} else {
				return val;
			}
		} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
			if (this.parent != null) {
				return Global.dp2px (val) / this.parent.size_height;
			} else {
				return Global.dp2px (val) / Screen.height;
			}
		} else if (IsNumberic (value.Replace ("xp", ""), out val)) {
			if (this.parent != null) {
				return val / this.parent.size_height;
			} else {
				return val / Screen.height;
			}
		}
		return 0f;
	}

	#endregion

	public static bool IsNumberic (string message, out float result)
	{
		System.Text.RegularExpressions.Regex rex_int_p = new System.Text.RegularExpressions.Regex (@"^\d+$");
		System.Text.RegularExpressions.Regex rex_int_n = new System.Text.RegularExpressions.Regex (@"^-\d+$");
		System.Text.RegularExpressions.Regex rex_float_p = new System.Text.RegularExpressions.Regex (@"^\d+\.\d+$");
		System.Text.RegularExpressions.Regex rex_float_n = new System.Text.RegularExpressions.Regex (@"^-\d+\.\d+$");
		result = 0f;
		bool rc = false;
		if (rex_int_p.IsMatch (message) || rex_int_n.IsMatch (message) || rex_float_p.IsMatch (message) || rex_float_n.IsMatch (message)) {
			try {
				result = float.Parse (message);
				rc = true;
			} catch {
				Utils.print (LogLevel.WARNING, "invalid number format: {0}", message);
			}
		}
		return rc;
	}

}