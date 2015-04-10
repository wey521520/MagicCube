using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq.Expressions;

public class ReadXML : MonoBehaviour
{

	public string XMLpath = "";
	public Transform rootTransform;
	private BaseFrame root = null;

	private string pathstring = "";

	#region Read attributions

	private BaseFrame readAttribution (BaseFrame baseframe, XmlReader reader)
	{
		//read common attribution
		baseframe.uiname = reader.GetAttribute ("name");
		baseframe.res = reader.GetAttribute ("res");
		baseframe.layout_width = String2Lower (reader.GetAttribute ("width"));
		baseframe.layout_height = String2Lower (reader.GetAttribute ("height"));
		baseframe.layout_weight = String2float (reader.GetAttribute ("weight"));
		baseframe.layout_anchor = String2Lower (reader.GetAttribute ("anchor"));
		baseframe.layout_offsetleft = String2Lower (reader.GetAttribute ("marginleft"));
		baseframe.layout_offsetright = String2Lower (reader.GetAttribute ("marginright"));
		baseframe.layout_offsettop = String2Lower (reader.GetAttribute ("margintop"));
		baseframe.layout_offsetbottom = String2Lower (reader.GetAttribute ("marginbottom"));
		baseframe.background = String2Lower (reader.GetAttribute ("background"));
		baseframe.background_color = String2Lower (reader.GetAttribute ("bgcolor"));
		baseframe.background_type = String2Lower (reader.GetAttribute ("bgtype"));
		baseframe.background_aspect = String2Lower (reader.GetAttribute ("bgaspect"));
		baseframe.distribution = String2Lower (reader.GetAttribute ("distribution"));
		baseframe.maskable = String2Lower (reader.GetAttribute ("mask"));

		////============> trigger event.
		string pointerEnter = reader.GetAttribute ("pointerEnter");
		if (pointerEnter != null) {
			baseframe.events.pointerEnterEvents = AddFunctionToEvent (pointerEnter);
		}
		string pointerExit = reader.GetAttribute ("pointerExit");
		if (pointerExit != null) {
			baseframe.events.pointerExitEvents = AddFunctionToEvent (pointerExit);
		}
		string pointerDown = reader.GetAttribute ("pointerDown");
		if (pointerDown != null) {
			baseframe.events.pointerDownEvents = AddFunctionToEvent (pointerDown);
		}
		string pointerUp = reader.GetAttribute ("pointerUp");
		if (pointerUp != null) {
			baseframe.events.pointerUpEvents = AddFunctionToEvent (pointerUp);
		}
		string pointerClick = reader.GetAttribute ("pointerClick");
		if (pointerClick != null) {
			baseframe.events.pointerClickEvents = AddFunctionToEvent (pointerClick);
		}
		string drag = reader.GetAttribute ("drag");
		if (drag != null) {
			baseframe.events.dragEvents = AddFunctionToEvent (drag);
		}
		string drop = reader.GetAttribute ("drop");
		if (drop != null) {
			baseframe.events.dropEvents = AddFunctionToEvent (drop);
		}
		string scroll = reader.GetAttribute ("scroll");
		if (scroll != null) {
			baseframe.events.scrollEvents = AddFunctionToEvent (scroll);
		}
		string updateSelected = reader.GetAttribute ("updateSelected");
		if (updateSelected != null) {
			baseframe.events.updateSelectedEvents = AddFunctionToEvent (updateSelected);
		}
		string select = reader.GetAttribute ("select");
		if (select != null) {
			baseframe.events.selectEvents = AddFunctionToEvent (select);
		}
		string deselect = reader.GetAttribute ("deselect");
		if (deselect != null) {
			baseframe.events.deselectEvents = AddFunctionToEvent (deselect);
		}
		string move = reader.GetAttribute ("move");
		if (move != null) {
			baseframe.events.moveEvents = AddFunctionToEvent (move);
		}
		string initializePotentialDrag = reader.GetAttribute ("initializePotentialDrag");
		if (initializePotentialDrag != null) {
			baseframe.events.initializePotentialDragEvents = AddFunctionToEvent (initializePotentialDrag);
		}
		string beginDrag = reader.GetAttribute ("beginDrag");
		if (beginDrag != null) {
			baseframe.events.beginDragEvents = AddFunctionToEvent (beginDrag);
		}
		string endDrag = reader.GetAttribute ("endDrag");
		if (endDrag != null) {
			baseframe.events.endDragEvents = AddFunctionToEvent (endDrag);
		}
		string submit = reader.GetAttribute ("submit");
		if (submit != null) {
			baseframe.events.submitEvents = AddFunctionToEvent (submit);
		}
		string cancel = reader.GetAttribute ("cancel");
		if (cancel != null) {
			baseframe.events.cancelEvents = AddFunctionToEvent (cancel);
		}

		////============> default event.
		string onClick = reader.GetAttribute ("onClick");
		if (onClick != null) {
			baseframe.defaultevents.onClickEvents = AddFunctionToEvent (onClick);
		}
		string onValueChanged = reader.GetAttribute ("onValueChanged");
		if (onValueChanged != null) {
			baseframe.defaultevents.onValueChangedEvents = AddFunctionToEvent (onValueChanged);
		}
		string endEdit = reader.GetAttribute ("endEdit");
		if (endEdit != null) {
			baseframe.defaultevents.endEditEvents = AddFunctionToEvent (endEdit);
		}

		////============> customer event.
		string customerclick = reader.GetAttribute ("customerclick");
		if (customerclick != null) {
			baseframe.customerevents.onClickEvents = AddFunctionToEvent (customerclick);
		}
		string customersubmit = reader.GetAttribute ("customersubmit");
		if (customersubmit != null) {
			baseframe.customerevents.onSubmitEvents = AddFunctionToEvent (customersubmit);
		}
		return baseframe;
	}

	private List<BaseFrameEvent> AddFunctionToEvent (string resource)
	{
		resource = resource.Replace ("\n", "");
		resource = resource.Replace ("\r", "");
		resource = resource.Replace ("\t", "");
		resource = resource.Replace (" ", "");
		List<BaseFrameEvent> list = new List<BaseFrameEvent> ();

		string[] l = null;

		char[] charSeparator1 = new char[] { ';' };
		char[] charSeparator2 = new char[] { ',' };

		l = resource.Split (charSeparator1, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < l.Length; i++) {
			if (l [i] != null) {
				string[] se = new string[3];
				se = l [i].Split (charSeparator2, 3, System.StringSplitOptions.RemoveEmptyEntries);
				BaseFrameEvent myevent = new BaseFrameEvent (se [0], se [1], se [2]);
				list.Add (myevent);
			}
		}
		return list;
	}

	private bfScrollPanel readAttribution (bfScrollPanel scrollpanel, XmlReader reader)
	{
		//read scroll panel attribution
		scrollpanel.scrolldirection = String2Lower (reader.GetAttribute ("scrolldir"));
		return scrollpanel;
	}

	private bfButton readAttribution (bfButton button, XmlReader reader)
	{
		//read button attribution
		button.text = reader.GetAttribute ("text");
		button.textColor = String2Lower (reader.GetAttribute ("textcolor"));
		button.textSize = String2Lower (reader.GetAttribute ("textsize"));
		button.textAlignment = String2Lower (reader.GetAttribute ("textalignment"));
		button.textFontStyle = String2Lower (reader.GetAttribute ("textfontstyle"));
		return button;
	}

	private bfImage readAttribution (bfImage image, XmlReader reader)
	{
		//read scroll panel attribution
		image.preserveaspect = String2Lower (reader.GetAttribute ("bgaspect"));
		image.background_type = String2Lower (reader.GetAttribute ("bgtype"));
		return image;
	}

	private bfText readAttribution (bfText text, XmlReader reader)
	{
		//read text attribution
		text.text = reader.GetAttribute ("text");
		text.textColor = String2Lower (reader.GetAttribute ("textcolor"));
		text.textSize = String2Lower (reader.GetAttribute ("textsize"));
		text.textAlignment = String2Lower (reader.GetAttribute ("textalignment"));
		text.textFontStyle = String2Lower (reader.GetAttribute ("textfontstyle"));
		return text;
	}

	#endregion

	bool iscombine;

	void Start ()
	{
		if (rootTransform == null) {
			rootTransform = this.transform;
			var UI = GetComponent<BaseFrameForUI> ();
			UI.parent = rootTransform.GetComponent<BaseFrameForUI> ();
		}

		#if UNITY_EDITOR
		pathstring = Application.dataPath + "/StreamingAssets";

		#elif UNITY_IPHONE
		pathstring = Application.dataPath +"/Raw";

		#elif UNITY_ANDROID
		pathstring = "jar:file://" + Application.dataPath + "!/assets/";

		#endif

//		Init ();
//		StartCoroutine (BeginCombine ());
		BeginCombine ();
	}

	void BeginCombine ()
//	IEnumerator BeginCombine ()
	{
		var t = new TraceLog ("Combine");
		int num = 0;
		iscombine = true;
		string srcpath = System.IO.Path.Combine (pathstring, XMLpath);

		CombineXml (srcpath);

		while (!iscombine && num < 20) {
			num++;
//			yield return null;
			CombineXml (System.IO.Path.Combine (Application.persistentDataPath, XMLpath));
		}
		t.time ();
		Init ();
		t.time ();
	}

	public void Init ()
	{

		//		var p = System.IO.Path.Combine(Application.streamingAssetsPath,XMLpath);
		string p = "";
		XmlReader reader = null;
		try {
			p = System.IO.Path.Combine (Application.persistentDataPath, XMLpath);
			reader = XmlReader.Create (p);
		} catch (Exception e) {
			Utils.print (LogLevel.DEBUG, "init: {0}", e.ToString ());
		}
		BaseFrame current_parent = null;

		while (reader.Read ()) {
			try {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					var baseUI = getControlByName (reader.Name);
					if (reader.HasAttributes) {
						// read general attributions
						baseUI = readAttribution (baseUI, reader);
						// read special attributions
						if (baseUI.type == BaseFrameType.bfScrollPanel) {
							baseUI = readAttribution ((bfScrollPanel)baseUI, reader);
						} else if (baseUI.type == BaseFrameType.bfButton) {
							baseUI = readAttribution ((bfButton)baseUI, reader);
						} else if (baseUI.type == BaseFrameType.bfImage) {
							baseUI = readAttribution ((bfImage)baseUI, reader);
						} else if (baseUI.type == BaseFrameType.bfText) {
							baseUI = readAttribution ((bfText)baseUI, reader);
						}

						// Add Customer defined to dictionary.
						while (reader.MoveToNextAttribute ()) {
							//print (reader.Name + "<>" + reader.Value );
							if (reader.Name.Contains ("c_")) {
								string key = reader.Name;
								key = key.Replace ("c_", "");
								baseUI.customerdefined.Add (key, reader.Value);
								//print (key + "=" + reader.Value);
							}
						}
						// Move the reader back to the element node.
						reader.MoveToElement ();
					}

					// set the relationship.
					if (current_parent != null) {
						current_parent.childitems.Add (baseUI);
						baseUI.parent = current_parent;
					}
					if (root == null) {
						root = baseUI;
					}

					if (reader.IsEmptyElement) {
						// if this then there is no element. then creat same level element.
					} else {
						// if this then there is a child element. then creat child level element.
						current_parent = baseUI;
					}
					break;
				case XmlNodeType.EndElement:
	                // if this then creat a parent level element.
					current_parent = current_parent.parent;
					break;
				default :
					break;
				}
			} catch (Exception e) {
				Utils.print (LogLevel.DEBUG, "while: {0}", e.ToString ());
			}
		}

		if (rootTransform == null) {
			Utils.error ("Error : Null root transform.");
			rootTransform = GameObject.FindObjectOfType<Canvas> ().transform;
		}
		try {

			CreatUI (root, rootTransform);
		} catch (Exception e) {
			Utils.print (LogLevel.DEBUG, "create ui: {0}", e.ToString ());
		}
	}
	// combine the source xml and inclode.
	void CombineXml (string pathstr)
	{
		//Utils.print (LogLevel.DEBUG, "11===={0}", pathstr);
		iscombine = true;
		//给定了文件的全路径，载入文档
		try {
			XmlDocument xmlDoc = new XmlDocument ();
			string content = "";
			Utils.readStreamingAssets (pathstr, out content);

			xmlDoc.LoadXml (content);

			XmlNode node = xmlDoc.DocumentElement;
			FindInclude (xmlDoc, node);

			xmlDoc.Save (System.IO.Path.Combine (Application.persistentDataPath, XMLpath));
		} catch (Exception e) {
			Utils.print (LogLevel.DEBUG, "combine: {0}", e.ToString ());
		}
//		print (System.IO.Path.Combine(Application.persistentDataPath, XMLpath));
		// then could be used.
	}

	void FindInclude (XmlDocument origonal, XmlNode curnode)
	{
		for (int i = 0; i < curnode.ChildNodes.Count; i++) {
			if (curnode.ChildNodes [i].Name == "Include") {
				iscombine = false;
				XmlElement em = (XmlElement)curnode.ChildNodes [i];
				// Find the include xml.
				string childpath = em.GetAttribute ("filepath");
				string path = System.IO.Path.Combine (pathstring, childpath);
				//Utils.print (LogLevel.DEBUG, "222===={0}", path);

				XmlDocument doc = new XmlDocument ();

				string content = "";
				Utils.readStreamingAssets (path, out content);

				doc.LoadXml (content);

				//doc.Load (path); //这个在Android设备上无法使用。
				//Utils.print (LogLevel.DEBUG, "222====aaa");

				// 如果执行深层克隆，则为 true；否则为 false。
				XmlNode rootnode = origonal.ImportNode (doc.DocumentElement, true);
				//Utils.print (LogLevel.DEBUG, "222====bbb");

				// replace the new xml for this element.
				curnode.ReplaceChild (rootnode, curnode.ChildNodes [i]);

				//Utils.print (LogLevel.DEBUG, "333===={0}", curnode.ToString ());

			} else if (curnode.ChildNodes [i].ChildNodes.Count > 0) {
				FindInclude (origonal, curnode.ChildNodes [i]);
			}
		}
	}

	/// <summary>
	/// Gets the name of the control by.
	/// </summary>
	/// <returns>The control by name.</returns>
	/// <param name="name">Name.</param>
	private BaseFrame getControlByName (string name)
	{
		BaseFrame rc = null;
		switch (name) {
		case "Panel":
			rc = new bfPanel ();
			break;
		case "ScrollPanel":
			rc = new bfScrollPanel ();
			break;
		case "Image":
			rc = new bfImage ();
			break;
		case "Button":
			rc = new bfButton ();
			break;
		case "Text":
			rc = new bfText ();
			break;
		case "Prefab":
			rc = new bfPrefab ();
			break;
		default:
			print ("UnknownStyle");
			rc = new BaseFrame ();
			break;
		}
		return rc;
	}

	/// <summary>
	/// Creats the user interface by style.
	/// </summary>
	/// <returns>The user interface by style.</returns>
	/// <param name="typename">Typename.</param>
	/// <param name="res">Res.</param>
	private BaseFrameForUI CreatUIByStyle (BaseFrameType typename, string res)
	{
		GameObject obj = null;
		BaseFrameForUI baseframeui = null;
		switch (typename) {
		case BaseFrameType.bfPanel:
		case BaseFrameType.bfScrollPanel:
		case BaseFrameType.bfImage:
		case BaseFrameType.bfButton:
		case BaseFrameType.bfText:
			obj = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/" + typename.ToString ()));
			break;
		case BaseFrameType.bfPrefab:
			obj = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/" + res));
			break;
		default :
			GameObject prefab = Resources.Load<GameObject> ("Prefabs/" + res);
			if (prefab == null) {
				Utils.error ("Cannot find suitable resource obj.");
			} else {
				obj = (GameObject)Instantiate (prefab);
			}
			break;
		}
		baseframeui = obj.GetComponent<BaseFrameForUI> ();
		return baseframeui;
	}


	// Creat the UI.
	public void CreatUI (BaseFrame ui_xml, Transform parenttransform)
	{

		BaseFrameForUI UI = CreatUIByStyle (ui_xml.type, ui_xml.res);

		UI.LoadFromBaseFrame (ui_xml);
		#region Set the relationship between UIs.
		UI.gameObject.name = UI.baseFrame.uiname;
		UI.mytransform = UI.transform;
		UI.transform.SetParent (parenttransform, false);
		// For screen space camera
		UI.transform.localScale = Vector3.one;
		UI.transform.position = new Vector3 (UI.transform.position.x, UI.transform.position.y, 0f);
		UI.parent = parenttransform.GetComponent<BaseFrameForUI> ();
		if (UI.parent != null && UI.parent.childitems != null) {
			UI.parent.childitems.Add (UI);
		}
		if (UI.baseFrame.maskable == "true") {
			UI.GetComponent<Mask> ().enabled = true;
		}

		// Settings for scroll panel.
		if (ui_xml.type == BaseFrameType.bfScrollPanel) {
			var c = ui_xml as bfScrollPanel;
			UI.parent.gameObject.GetComponent<Mask> ().enabled = true;
			ScrollRect scr = UI.parent.gameObject.AddComponent<ScrollRect> ();
			scr.content = UI.GetComponent<RectTransform> ();
			if (c.scrolldirection == "horizontal") {
				scr.horizontal = true;
				scr.vertical = false;
			} else if (c.scrolldirection == "vertical") {
				scr.horizontal = false;
				scr.vertical = true;
			} else if (c.scrolldirection == "both") {
				scr.horizontal = true;
				scr.vertical = true;
			} else {
				scr.horizontal = false;
				scr.vertical = false;
			}
		}
		// Settings for scroll panel.
		if (ui_xml.type == BaseFrameType.bfButton) {
			var b = ui_xml as bfButton;
			Text btext = UI.GetComponentInChildren<Text> ();

			if (b.text != null) {
				btext.text = b.text;
			}
			if (b.textColor != null) {
				Color32 c = new Color32 ();
				string s = b.textColor;
				c.r = Convert.ToByte (s.Substring (1, 2), 16);
				c.g = Convert.ToByte (s.Substring (3, 2), 16);
				c.b = Convert.ToByte (s.Substring (5, 2), 16);
				c.a = Convert.ToByte (s.Substring (7, 2), 16);
				btext.color = c;
			}
			if (b.textSize != null) {
				float val = 0f;
				if (IsNumberic (b.textSize.Replace ("sp", ""), out val)) {
					btext.fontSize = (int)(Global.dp2px (val));
				}
			}
			if (b.textAlignment != null) {
				btext.alignment = SetTextAnchor (b.textAlignment);
			}
			if (b.textFontStyle != null) {
				btext.fontStyle = SetFontStyle (b.textFontStyle);
			}
		}

		if (!string.IsNullOrEmpty (ui_xml.background_type)) {
			UI.GetComponent<Image> ().type = SetImageType (ui_xml.background_type);
		}
		if (ui_xml.background_aspect == "true") {
			UI.GetComponent<Image> ().preserveAspect = true;
		}

//		// Setting for image.
//		if (ui_xml.type == BaseFrameType.bfImage) {
//			var m = ui_xml as bfImage;
//			if (!string.IsNullOrEmpty (m.background_type)) {
//				UI.GetComponent<Image> ().type = SetImageType (m.background_type);
//			}
//			if (m.preserveaspect == "true") {
//				UI.GetComponent<Image> ().preserveAspect = true;
//			}
//		}
		// Setting for text.
		if (ui_xml.type == BaseFrameType.bfText) {
			var t = ui_xml as bfText;
			Text ttext = UI.GetComponentInChildren<Text> ();
			if (t.text != null) {
				ttext.text = t.text;
			}
			if (t.textColor != null) {
				Color32 c = new Color32 ();
				string s = t.textColor;
				c.r = Convert.ToByte (s.Substring (1, 2), 16);
				c.g = Convert.ToByte (s.Substring (3, 2), 16);
				c.b = Convert.ToByte (s.Substring (5, 2), 16);
				c.a = Convert.ToByte (s.Substring (7, 2), 16);
				ttext.color = c;
			}
			if (t.textSize != null) {
				float val = 0f;
				if (IsNumberic (t.textSize.Replace ("sp", ""), out val)) {
					ttext.fontSize = (int)(Global.dp2px (val));
				}
			}
			if (!string.IsNullOrEmpty (t.textAlignment)) {
				ttext.alignment = SetTextAnchor (t.textAlignment);
			}
			if (!string.IsNullOrEmpty (t.textFontStyle)) {
				ttext.fontStyle = SetFontStyle (t.textFontStyle);
			}
		}
		#endregion

		if (!ui_xml.events.isEmpty () || !ui_xml.defaultevents.isEmpty () || !ui_xml.customerevents.isEmpty ()) {
			BFEventHandler handler = UI.GetComponent<BFEventHandler> ();
			handler = handler == null ? UI.gameObject.AddComponent<BFEventHandler> () : handler;

			handler.events.copy (ui_xml.events);
			AddTriggers (ui_xml, UI, handler);

			handler.defaultevents.copy (ui_xml.defaultevents);
			AddDefaultTriggers (ui_xml, UI, handler);

			handler.customerevents.copy (ui_xml.customerevents);
			AddCustomerTriggers (ui_xml, UI, handler);
		}

		#region Set the size and position.
		// cal from width and height mode.
		UI.widthpct = UI.PercentageOfParentWidth ();
		UI.heightpct = UI.PercentageOfParentHeight ();
		// Add in 2015.1.12 for calculate the remained(rest) size. Then I could use exact number and percentage together.
		if (UI.widthpct == -1f || UI.heightpct == -1f) {
			int index = UI.parent.childitems.Count;
			if (UI.widthpct == -1f) {
				float sumsizew = 0f;
				for (int i = 0; i < index - 1; i++) {
					sumsizew += UI.parent.childitems [i].size_width;
				}
				sumsizew = 1f - sumsizew / UI.parent.size_width;
				sumsizew = Mathf.Max (sumsizew, 0f);
				UI.widthpct = sumsizew;
			}
			if (UI.heightpct == -1f) {
				float sumsizeh = 0f;
				for (int i = 0; i < index - 1; i++) {
					sumsizeh += UI.parent.childitems [i].size_height;
				}
				sumsizeh = 1f - sumsizeh / UI.parent.size_height;
				sumsizeh = Mathf.Max (sumsizeh, 0f);
				UI.heightpct = sumsizeh;
			}
		}
		// when i had finish the size of UI, i will set the anchor.
		UI.AdjustAnchor ();
		UI.GetComponent<RectTransform> ().anchorMin = UI.anchormin;
		UI.GetComponent<RectTransform> ().anchorMax = UI.anchormax;
		// set the offset.
		UI.CalculateOffset ();

		UI.GetComponent<RectTransform> ().offsetMin = new Vector2 (UI.offsetleft, UI.offsetbottom);
		UI.GetComponent<RectTransform> ().offsetMax = new Vector2 (UI.offsetright, UI.offsettop);

		//adjust anchor
//		RectTransform parentrect = UI.transform.parent.GetComponent<RectTransform> ();
//		Vector2 rect = new Vector2(parentrect.rect.width,parentrect.rect.height);
//		UI.offsetleft 	= UI.offsetleft / rect.x;
//		UI.offsetright 	= UI.offsetright / rect.x;
//		UI.offsetbottom = UI.offsetbottom / rect.y;
//		UI.offsettop 	= UI.offsettop / rect.y;
//		UI.GetComponent<RectTransform> ().anchorMin += new Vector2 (UI.offsetleft, UI.offsetbottom);
//		UI.GetComponent<RectTransform> ().anchorMax += new Vector2 (UI.offsetright, UI.offsettop);
//		UI.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
		#endregion

		// when ui in a scrollpanel. auto offset it pos.
		if (ui_xml.type == BaseFrameType.bfScrollPanel) {
			float totalwidth = 0f;
			float val = 0f;
			int i = 0;
			UI.unchanged_width = UI.GetComponent<RectTransform> ().rect.width;
			UI.unchanged_height = UI.GetComponent<RectTransform> ().rect.height;

			for (i = 0; i < UI.baseFrame.childitems.Count; i++) {
				string value = UI.baseFrame.childitems [i].layout_width;
				if (value == null) {
				} else if (value == "fill") {
					totalwidth += UI.parent.size_width;
				} else if (IsNumberic (value, out val)) {   // if percentage mode.
					totalwidth += UI.parent.size_width * val;
				} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
					totalwidth += Global.dp2px (val);
				}
			}
			float totalheight = 0f;
			for (i = 0; i < UI.baseFrame.childitems.Count; i++) {
				string value = UI.baseFrame.childitems [i].layout_height;
				if (value == null) {
				} else if (value == "fill") {
					totalheight += UI.parent.size_height;
				} else if (IsNumberic (value, out val)) {   // if percentage mode.
					totalheight += UI.parent.size_height * val;
				} else if (IsNumberic (value.Replace ("dp", ""), out val)) {
					totalheight += Global.dp2px (val);
				}
			}

			totalwidth -= 2f * UI.GetComponent<RectTransform> ().rect.width;
			totalheight -= UI.GetComponent<RectTransform> ().rect.height;

			// add in 2015.2.28 for check the scroll area is larger than rect or not, when is smaller, switch down the scroll.
			if (totalwidth < 0) {
				UI.transform.parent.GetComponent <ScrollRect> ().horizontal = false;
			}
			if (totalheight < 0) {
				UI.transform.parent.GetComponent <ScrollRect> ().vertical = false;
			}

			totalwidth = Mathf.Max (0f, totalwidth);
			totalheight = Mathf.Max (0f, totalheight);

			var c = ui_xml as bfScrollPanel;
			if (c.scrolldirection == "horizontal") {
				UI.GetComponent<RectTransform> ().offsetMax =
                    new Vector2 (UI.offsetright + totalwidth, UI.offsettop);
			} else if (c.scrolldirection == "vertical") {
				UI.GetComponent<RectTransform> ().offsetMin =
                    new Vector2 (UI.offsetleft, UI.offsetbottom - totalheight);
			} else if (c.scrolldirection == "both") {
				UI.GetComponent<RectTransform> ().offsetMax =
                    new Vector2 (UI.offsetright + totalwidth, UI.offsettop);
				UI.GetComponent<RectTransform> ().offsetMin =
                    new Vector2 (UI.offsetleft, UI.offsetbottom - totalheight);
			}
		}

		// write the change.
		UI.size_width = UI.GetComponent<RectTransform> ().rect.width;
		UI.size_height = UI.GetComponent<RectTransform> ().rect.height;

		UI.SetUIShow ();

		// Add wrap mode offset.
		if (UI.baseFrame.layout_width == "wrap" || UI.baseFrame.layout_height == "wrap") {
			int index = UI.parent.childitems.Count;
			float offset = 0f;
			for (int i = 0; i < index - 1; i++) {
				offset += UI.parent.baseFrame.childitems [i].layout_weight;
			}
			if (UI.baseFrame.layout_width == "wrap") {
				UI.GetComponent<RectTransform> ().anchorMin += new Vector2 (offset / UI.parent.weightsum, 0f);
				UI.GetComponent<RectTransform> ().anchorMax += new Vector2 (offset / UI.parent.weightsum, 0f);
			}
			if (UI.baseFrame.layout_height == "wrap") {
				UI.GetComponent<RectTransform> ().anchorMin += new Vector2 (0f, -offset / UI.parent.weightsum);
				UI.GetComponent<RectTransform> ().anchorMax += new Vector2 (0f, -offset / UI.parent.weightsum);
			}
		} else if (UI.parent != null && UI.parent.baseFrame.distribution != "relative"
		           && UI.parent.baseFrame.distribution != null) {
			int index = UI.parent.childitems.Count;
			float offset = 0f;
			if (UI.parent.baseFrame.distribution == "horizontal") {
				for (int i = 0; i < index - 1; i++) {
					offset += UI.parent.childitems [i].size_width;
				}
				//UI.GetComponent<RectTransform> ().anchoredPosition += new Vector2 (offset, 0f);// For set the rect of ui.
				UI.GetComponent<RectTransform> ().anchorMin += new Vector2 (offset / UI.parent.size_width, 0f);
				UI.GetComponent<RectTransform> ().anchorMax += new Vector2 (offset / UI.parent.size_width, 0f);
			} else if (UI.parent.baseFrame.distribution == "vertical") {
				for (int i = 0; i < index - 1; i++) {
					offset += UI.parent.childitems [i].size_height;
				}
				//UI.GetComponent<RectTransform> ().anchoredPosition += new Vector2 (0f, - offset);
				//UI.GetComponent<RectTransform> ().anchoredPosition += new Vector2 (0f, - offset);
				UI.GetComponent<RectTransform> ().anchorMin += new Vector2 (0f, -offset / UI.parent.size_height);
				UI.GetComponent<RectTransform> ().anchorMax += new Vector2 (0f, -offset / UI.parent.size_height);
			}

		}

		// add 15.1.27, for build my style.
		if (UI.GetComponent<CustomerDefinedStyle> () != null) {
			UI.GetComponent<CustomerDefinedStyle> ().BuildMyStyle ();
		}

		if (UI.baseFrame.childitems != null) {
			if (UI.baseFrame.childitems.Count > 0) {
				UI.weightsum = 0;
				for (int i = 0; i < UI.baseFrame.childitems.Count; i++) {
					UI.weightsum += UI.baseFrame.childitems [i].layout_weight;
				}
			}
			// Creat child UI.
			if (UI.baseFrame.childitems.Count > 0) {
				for (int i = 0; i < UI.baseFrame.childitems.Count; i++) {
					CreatUI (UI.baseFrame.childitems [i], UI.mytransform);
				}
			}
		}
	}

	#region Static function tools

	public static float String2float (string message)
	{
		float result = 0f;
		if (!string.IsNullOrEmpty (message)) {
			try {
				result = float.Parse (message);
			} catch (Exception e) {
				Utils.warning ("invalid number format: {0}", message);
			}
		}
		return result;
	}

	public static string String2Lower (string message)
	{
		if (message == null) {
			return null;
		}
		return message.ToLowerInvariant ();
	}

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

	public static FontStyle SetFontStyle (string fontstylename)
	{
		FontStyle mystyle = FontStyle.Normal;
		switch (fontstylename) {
		case "bold":
			mystyle = FontStyle.Bold;
			break;
		case "italic":
			mystyle = FontStyle.Italic;
			break;
		case "boldanditalic":
			mystyle = FontStyle.BoldAndItalic;
			break;
		default:
			mystyle = FontStyle.Normal;
			break;
		}
		return mystyle;
	}

	public static TextAnchor SetTextAnchor (string anchorname)
	{
		TextAnchor myanchor = TextAnchor.MiddleCenter;
		switch (anchorname) {
		case "upperleft":
			myanchor = TextAnchor.UpperLeft;
			break;
		case "uppercenter":
			myanchor = TextAnchor.UpperCenter;
			break;
		case "upperright":
			myanchor = TextAnchor.UpperRight;
			break;
		case "middleleft":
			myanchor = TextAnchor.MiddleLeft;
			break;
		case "middlecenter":
			myanchor = TextAnchor.MiddleCenter;
			break;
		case "middleright":
			myanchor = TextAnchor.MiddleRight;
			break;
		case "lowerleft":
			myanchor = TextAnchor.LowerLeft;
			break;
		case "lowercenter":
			myanchor = TextAnchor.LowerCenter;
			break;
		case "lowerright":
			myanchor = TextAnchor.LowerRight;
			break;
		default:
			myanchor = TextAnchor.MiddleCenter;
			break;
		}
		return myanchor;
	}

	public static Image.Type SetImageType (string imagetypename)
	{
		Image.Type mystyle = Image.Type.Sliced;
		switch (imagetypename) {
		case "simple":
			mystyle = Image.Type.Simple;
			break;
		case "sliced":
			mystyle = Image.Type.Sliced;
			break;
		case "tiled":
			mystyle = Image.Type.Tiled;
			break;
		case "filled":
			mystyle = Image.Type.Filled;
			break;
		default:
			mystyle = Image.Type.Sliced;
			break;
		}
		return mystyle;
	}

	#endregion

	#region Add event and the script.

	private void AddDefaultTriggers (BaseFrame ui_xml, BaseFrameForUI UI, BFEventHandler defaulthandler)
	{

		if (!defaulthandler.defaultevents.isEmpty ()) {
			if (ui_xml.defaultevents.onClickEvents.Count > 0) {
				Button button = UI.GetComponent<Button> ();
				button.onClick.AddListener (defaulthandler.OnClick);
			}
			if (ui_xml.defaultevents.onValueChangedEvents.Count > 0) {
				Slider slider = UI.GetComponent<Slider> ();
				if (slider != null) {
					slider.onValueChanged.AddListener (defaulthandler.OnValueChanged);
				}
				Scrollbar scrollbar = UI.GetComponent<Scrollbar> ();
				if (scrollbar != null) {
					scrollbar.onValueChanged.AddListener (defaulthandler.OnValueChanged);
				}
				Toggle toggle = UI.GetComponent<Toggle> ();
				if (toggle != null) {
					toggle.onValueChanged.AddListener (defaulthandler.OnValueChanged);
				}
				InputField inputfield = UI.GetComponent<InputField> ();
				if (inputfield != null) {
					inputfield.onValueChange.AddListener (defaulthandler.OnValueChanged);
				}
			}
			if (ui_xml.defaultevents.endEditEvents.Count > 0) {
				InputField inputfield = UI.GetComponent<InputField> ();
				if (inputfield != null) {
					inputfield.onEndEdit.AddListener (defaulthandler.EndEdit);
				}
			}
		}
	}
	// For customer events.
	private void AddCustomerTriggers (BaseFrame ui_xml, BaseFrameForUI UI, BFEventHandler handler)
	{
		if (!handler.customerevents.isEmpty ()) {
			CustomerDefinedStyle customerscript = UI.GetComponent<CustomerDefinedStyle> ();
			if (ui_xml.customerevents.onClickEvents.Count > 0) {
				customerscript.onCustomerClick.AddListener (handler.OnCustomerClick);
			}
			if (ui_xml.customerevents.onSubmitEvents.Count > 0) {
				customerscript.onCustomerSubmit.AddListener (handler.OnCustomerSubmit);
			}
		}
	}

	/// <summary>
	/// Adds the triggers.
	/// </summary>
	/// <param name="ui_xml">Ui_xml.</param>
	/// <param name="UI">U.</param>
	/// <param name="handler">Handler.</param>
	private void AddTriggers (BaseFrame ui_xml, BaseFrameForUI UI, BFEventHandler handler)
	{

		if (!handler.events.isEmpty ()) {
			EventTrigger ev = GetComponent<EventTrigger> ();
			ev = ev == null ? UI.gameObject.AddComponent<EventTrigger> () : ev;
			ev.delegates = new List<EventTrigger.Entry> ();

			if (ui_xml.events.pointerEnterEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.PointerEnter;
				entry.callback.AddListener (handler.OnPointerEnter);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.pointerExitEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.PointerExit;
				entry.callback.AddListener (handler.OnPointerExit);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.pointerDownEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener (handler.OnPointerDown);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.pointerUpEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.PointerUp;
				entry.callback.AddListener (handler.OnPointerUp);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.pointerClickEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener (handler.OnPointerClick);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.dragEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Drag;
				entry.callback.AddListener (handler.OnDrag);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.dropEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Drop;
				entry.callback.AddListener (handler.OnDrop);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.scrollEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Scroll;
				entry.callback.AddListener (handler.OnScroll);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.updateSelectedEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.UpdateSelected;
				entry.callback.AddListener (handler.OnUpdateSelected);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.selectEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Select;
				entry.callback.AddListener (handler.OnSelect);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.deselectEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Deselect;
				entry.callback.AddListener (handler.OnDeselect);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.moveEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Move;
				entry.callback.AddListener (handler.OnMove);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.initializePotentialDragEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.InitializePotentialDrag;
				entry.callback.AddListener (handler.OnInitializePotentialDrag);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.beginDragEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.BeginDrag;
				entry.callback.AddListener (handler.OnBeginDrag);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.endDragEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.EndDrag;
				entry.callback.AddListener (handler.OnEndDrag);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.submitEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Submit;
				entry.callback.AddListener (handler.OnSubmit);

				ev.delegates.Add (entry);
			}
			if (ui_xml.events.cancelEvents.Count > 0) {
				EventTrigger.Entry entry = new EventTrigger.Entry ();

				entry.eventID = EventTriggerType.Cancel;
				entry.callback.AddListener (handler.OnCancel);

				ev.delegates.Add (entry);
			}
		}
	}

	#endregion
}
