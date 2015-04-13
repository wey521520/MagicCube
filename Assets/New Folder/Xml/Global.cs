using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

using LitJson;
using System;

public class Global : MonoBehaviour
{

	public enum SysType
	{
		OS_MACOSX = 1,
		OS_LINUX,
		OS_WINDOWS,
		OS_ANDROID,
		OS_IOS,
		OS_UNKNOWN,
	}

	private static SysType _sysType = SysType.OS_UNKNOWN;
	private static bool _isEditorMode = false;
	private static bool _isStandalone = false;

	public static SysType SystemType { get { return _sysType; } }

	public static bool EditorMode { get { return _isEditorMode; } }

	public static bool Standalone { get { return _isStandalone; } }

	//	public string appVersionFile;

	//	private static JsonData SimpleDataStore = new JsonData ();
	//	private static bool isSimpleDataStoreDirty = false;
	//	private static string filename = "DataStore";

	void Awake ()
	{
		DontDestroyOnLoad (this);
		init ();

//		//Compose db path
//		switch (Global.SystemType) {
//		case Global.SysType.OS_ANDROID:
//		case Global.SysType.OS_IOS:
//			filename = string.Format ("{0}/{1}", Application.persistentDataPath, filename);
//			break;
//		case Global.SysType.OS_MACOSX:
//		case Global.SysType.OS_LINUX:
//		case Global.SysType.OS_WINDOWS:
//			filename = string.Format ("{0}/{1}", Application.dataPath, filename);
//			break;
//		default:
//			break;
//		}
//
//		if (!Utils.isFileExisting (filename)) {
//			Utils.writeFile (filename, "{}");
//		}
//
//		string content = "";
//		Utils.readFile (filename, out content);
//
//		SimpleDataStore = JsonMapper.ToObject (content);
	}

	public static T GetEnumFromString<T> (string val, T default_val)
	{
		T rc = default_val;
		try {
			rc = (T)Enum.Parse (typeof(T), val, true);
		} catch (ArgumentException e) {
			Utils.print (LogLevel.DEBUG, e.ToString ());
		}
		return rc;
	}

	public static string getSystemName ()
	{
		string rc = "Unknown";
		switch (_sysType) {
		case SysType.OS_MACOSX:
			rc = "Mac OSX";
			break;
		case SysType.OS_LINUX:
			rc = "Linux";
			break;
		case SysType.OS_WINDOWS:
			rc = "Windows";
			break;
		case SysType.OS_ANDROID:
			rc = "Android";
			break;
		case SysType.OS_IOS:
			rc = "iOS";
			break;
		default:
			break;
		}
		return rc;
	}

	private void init ()
	{
		// init os type
		#if UNITY_STANDALONE_OSX
		_sysType = SysType.OS_MACOSX;
		#elif UNITY_STANDALONE_LINUX
		_sysType= SysType.OS_LINUX;
		#elif UNITY_STANDALONE_WIN
		_sysType= SysType.OS_WINDOWS;
		#elif UNITY_ANDROID
		_sysType = SysType.OS_ANDROID;
		#elif UNITY_IPHONE
		_sysType = SysType.OS_IOS;
		#endif

		// init editor
		#if UNITY_EDITOR
		_isEditorMode = true;
		#endif

		#if UNITY_STANDALONE
		_isStandalone = true;
		#endif
	}


	public static Sprite LoadSprite (string name)
	{
		Sprite mysprite = null;
		string spritename = name;
		if ((int)Global.dp2px (1f) == 2) {
			spritename = spritename + "_x2";
			mysprite = Resources.Load<Sprite> (spritename);
		} else if ((int)Global.dp2px (1f) == 3) {
			spritename = spritename + "_x3";
			mysprite = Resources.Load<Sprite> (spritename);
		}
		if (mysprite == null) {
			mysprite = Resources.Load<Sprite> (name);
		}
//		print (mysprite.name);
		return mysprite;
	}

	public static float dp2px (float dp)
	{
		float ppi = Screen.dpi > 0f ? Screen.dpi : 160f;
		if (_sysType == SysType.OS_IOS) {
			return Mathf.Max (1f, Mathf.CeilToInt ((ppi / 160f) - 0.5f)) * dp;
		} else if (_sysType == SysType.OS_ANDROID) {
			return Mathf.Max (1f, (ppi / 160f)) * dp;
		} else {
			return Mathf.Max (1f, (ppi / 160f)) * dp;
		}
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

	/*

	public static void Read (string key, out int value, int default_val)
	{
		if (SimpleDataStore.Keys.Contains (key)) {
			value = (int)SimpleDataStore [key];
		} else {
			value = default_val;
		}
	}

	public static void Read (string key, out float value, float default_val)
	{
		if (SimpleDataStore.Keys.Contains (key)) {
			value = (float)SimpleDataStore [key];
		} else {
			value = default_val;
		}
	}

	public static void Read (string key, out string value, string default_val)
	{
		if (SimpleDataStore.Keys.Contains (key)) {
			value = (string)SimpleDataStore [key];
		} else {
			value = default_val;
		}
	}

	public static void Read (string key, out bool value, bool default_val)
	{
		if (SimpleDataStore.Keys.Contains (key)) {
			value = (bool)SimpleDataStore [key];
		} else {
			value = default_val;
		}
	}

	public static void Save (string key, string value)
	{
		SimpleDataStore [key] = value;
		isSimpleDataStoreDirty = true;
	}

	public static void Save (string key, bool value)
	{
		SimpleDataStore [key] = value;
		isSimpleDataStoreDirty = true;
	}

	public static void Save (string key, int value)
	{
		SimpleDataStore [key] = value;
		isSimpleDataStoreDirty = true;
	}

	public static void Save (string key, float value)
	{
		SimpleDataStore [key] = value;
		isSimpleDataStoreDirty = true;
	}
	*/
}
