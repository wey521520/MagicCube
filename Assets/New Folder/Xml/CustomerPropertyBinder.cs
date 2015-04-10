using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CustomerPropertyBinder : MonoBehaviour
{
	[Serializable]
	public class PropertyBinderBase
	{
		public string PropertyName = "";
		public string DefaultPropertyValue = "";

		public virtual void bind (CustomerDefinedStyle cds)
		{
			throw new Exception ("please implement in derived class");
		}
	}

	[Serializable]
	public class FontSizePropertyBinder: PropertyBinderBase
	{
	
		public List<Text> textObjs = new List<Text> ();

		public override void bind (CustomerDefinedStyle cds)
		{
			string val = DefaultPropertyValue;

			if (cds.hasProperty (PropertyName))
			{
				val = cds.getProperty (PropertyName);
			}

			foreach (Text t in textObjs)
			{
				t.fontSize = (int)Global.dp2px (float.Parse (val.Replace ("sp", "")));
			}
		}
	}

	[Serializable]
	public class TextInfoPropertyBinder: PropertyBinderBase
	{

		public List<Text> textObjs = new List<Text> ();

		public override void bind (CustomerDefinedStyle cds)
		{
			string val = DefaultPropertyValue;

			if (cds.hasProperty (PropertyName))
			{
				val = cds.getProperty (PropertyName);
			}

			foreach (Text t in textObjs)
			{
				t.text = val;
			}
		}
	}

	[Serializable]
	public class PositionPropertyBinder: PropertyBinderBase
	{
		public List<RectTransform> tranObjs = new List<RectTransform> ();

		public override void bind (CustomerDefinedStyle cds)
		{
			string val = DefaultPropertyValue;

			if (cds.hasProperty (PropertyName))
			{
				val = cds.getProperty (PropertyName);
			}

			// 首先对val进行转化。val = 0.5，0.5，22dp，-7.5dp，35dp，7.5dp
			// 这里使用6个参数，前两个是中心点的相对坐标，取值范围为0-1，后面四位为左下点和右上点的实际坐标，带dp的是实际数值，不带dp的是相对的绝对值。
			string[] l = null;

			char[] charSeparator1 = new char[] { ',' };

			l = val.Split (charSeparator1, 6, System.StringSplitOptions.RemoveEmptyEntries);

			float x = 0.5f;
			float y = 0.5f;
			if (!Global.IsNumberic (l [0], out x))
			{
				return;
			}
			if (!Global.IsNumberic (l [1], out y))
			{
				return;
			}

			Vector2 center = new Vector2 (x, y);
			float a = 0f;
			float b = 0f;
			float c = 0f;
			float d = 0f;
			bool aa = true;
			bool bb = true;
			bool cc = true;
			bool dd = true;
			if (Global.IsNumberic (l [2], out a))
			{
			}
			else if (Global.IsNumberic (l [2].Replace ("dp", ""), out a))
			{
				aa = false;
			}
			else
			{
				return;
			}
			if (Global.IsNumberic (l [3], out b))
			{
			}
			else if (Global.IsNumberic (l [3].Replace ("dp", ""), out b))
			{
				bb = false;
			}
			else
			{
				return;
			}
			if (Global.IsNumberic (l [4], out c))
			{
			}
			else if (Global.IsNumberic (l [4].Replace ("dp", ""), out c))
			{
				cc = false;
			}
			else
			{
				return;
			}
			if (Global.IsNumberic (l [5], out d))
			{
			}
			else if (Global.IsNumberic (l [5].Replace ("dp", ""), out d))
			{
				dd = false;
			}
			else
			{
				return;
			}

			foreach (RectTransform t in tranObjs)
			{
				RectTransform tt = t;
				if (!aa || !bb || !cc || !dd)
				{
					tt = t.parent.GetComponent <RectTransform> ();
					//print ("tt");
				}
				t.anchorMin = new Vector2 (aa ? a : (Global.dp2px (a) * 1f / tt.rect.width + center.x),
					bb ? b : (Global.dp2px (b) * 1f / tt.rect.height + center.y));
				t.anchorMax = new Vector2 (cc ? c : (Global.dp2px (c) * 1f / tt.rect.width + center.x),
					dd ? d : (Global.dp2px (d) * 1f / tt.rect.height + center.y));
				t.anchoredPosition = Vector2.zero;
				t.localScale = Vector3.one;
				t.sizeDelta = Vector2.zero;
				//print (t.anchorMin + "<>" + t.anchorMax + t.name);
			}
		}
	
	}

	[Serializable]
	public class InputFieldPropertyBinder: PropertyBinderBase
	{

		public List<InputField> inputObjs = new List<InputField> ();

		public override void bind (CustomerDefinedStyle cds)
		{
			string val = DefaultPropertyValue;

			if (cds.hasProperty (PropertyName))
			{
				val = cds.getProperty (PropertyName);
			}

			foreach (InputField t in inputObjs)
			{
				t.contentType = (InputField.ContentType)Enum.Parse (typeof(InputField.ContentType), val, true);
			}
		}
	}

	[Serializable]
	public class ImagePropertyBinder: PropertyBinderBase
	{

		public List<Image> inputObjs = new List<Image> ();

		public override void bind (CustomerDefinedStyle cds)
		{
			string val = DefaultPropertyValue;

			if (cds.hasProperty (PropertyName))
			{
				val = cds.getProperty (PropertyName);
			}

			foreach (Image t in inputObjs)
			{
				t.sprite = Global.LoadSprite (val);
			}
		}
	}

	public List<FontSizePropertyBinder> fontSizeBinders = new List<FontSizePropertyBinder> ();
	public List<PositionPropertyBinder> positionBinders = new List<PositionPropertyBinder> ();
	public List<TextInfoPropertyBinder> textinfoBinders = new List<TextInfoPropertyBinder> ();

	public List<InputFieldPropertyBinder> inputfieldBinders = new List<InputFieldPropertyBinder> ();
	public List<ImagePropertyBinder> imageBinders = new List<ImagePropertyBinder> ();


	void Start ()
	{
		var cds = GetComponent<CustomerDefinedStyle> ();
		if (cds != null)
		{
			Init (cds);
		}
	}

	public void Init (CustomerDefinedStyle cds)
	{
		foreach (FontSizePropertyBinder b in fontSizeBinders)
		{
			b.bind (cds);
		}

		foreach (PositionPropertyBinder b in positionBinders)
		{
			b.bind (cds);
		}

		foreach (TextInfoPropertyBinder b in textinfoBinders)
		{
			b.bind (cds);
		}

		foreach (InputFieldPropertyBinder b in inputfieldBinders)
		{
			b.bind (cds);
		}

		foreach (ImagePropertyBinder b in imageBinders)
		{
			b.bind (cds);
		}
	}
}

