using UnityEngine;
using System;
using System.Collections;
using System.IO;
using LitJson;

public enum LogLevel {
	DEBUG   = 1,
	INFO    = 2,
	WARNING = 3,
	ERROR   = 4,
	EXCEPTION = 5,
	NOLOG = 9999,
}

public static class Utils
{
	/// <summary>
	/// The log level.
	/// </summary>
	public static LogLevel logLevel = LogLevel.DEBUG;

	public static bool OutputInScreen = true;

	public static string logs = "";
	public static int lines = 0;
	
	/// <summary>
	/// Print the specified level, format and args.
	/// </summary>
	/// <param name="level">Level.</param>
	/// <param name="format">Format.</param>
	/// <param name="args">Arguments.</param>
	public static void print(LogLevel level, string format, params object[] args) {
		if (OutputInScreen)
		{
			if (lines > 5)
			{
				logs = "";
				lines = 0;
			}
			logs += string.Format(format, args) + "\n"; 
			lines++;
		}

		if((int)level >= (int)Utils.logLevel) {
			switch(level) {
			case LogLevel.DEBUG:
			case LogLevel.INFO:
				Debug.Log(string.Format(format, args));
				break;
			case LogLevel.WARNING:
				Debug.LogWarning(string.Format(format, args));
				break;
			case LogLevel.ERROR:
				Debug.LogError(string.Format(format, args));
				break;
			case LogLevel.EXCEPTION:
				Debug.LogException(new Exception(string.Format(format, args)));
				break;
			default:
				break;             
			}
		}
	}

	public static void debug(string format, params object[] args) {
		Utils.print (LogLevel.DEBUG, format, args);
	}

    public static void warning(string format, params object[] args) {
        Utils.print (LogLevel.WARNING, format, args);
    }
    
    public static void error(string format, params object[] args) {
        Utils.print (LogLevel.ERROR, format, args);
    }

    public static void exception(string format, params object[] args) {
        Utils.print (LogLevel.EXCEPTION, format, args);
    }
	/// <summary>
	/// Jsons the data to JSO.
	/// </summary>
	/// <returns>The data to JSO.</returns>
	/// <param name="data">Data.</param>
	public static string JsonDataToJSON(object data) { 
		return JsonMapper.ToJson(data); 
	}
	
	/// <summary>
	/// JSONs to json data.
	/// </summary>
	/// <returns>The to json data.</returns>
	/// <param name="jsonstr">Jsonstr.</param>
	public static JsonData JSONToJsonData(string jsonstr) {
		return JsonMapper.ToObject(jsonstr);
	}
	
	/// <summary>
	/// Determines if hashtable to JSO the specified data.
	/// </summary>
	/// <returns><c>true</c> if hashtable to JSO the specified data; otherwise, <c>false</c>.</returns>
	/// <param name="data">Data.</param>
	public static string HashtableToJSON(object data) { 
		return JsonMapper.ToJson(data); 
	}
	
	/// <summary>
	/// Determines if hashtable to json data the specified data.
	/// </summary>
	/// <returns><c>true</c> if hashtable to json data the specified data; otherwise, <c>false</c>.</returns>
	/// <param name="data">Data.</param>
	public static JsonData HashtableToJsonData(Hashtable data) {
		return JSONToJsonData(HashtableToJSON(data));
	}
	
	/// <summary>
	/// Join the specified items and token.
	/// </summary>
	/// <param name="items">Items.</param>
	/// <param name="token">Token.</param>
	public static string Join(IList items, string token)
	{
		string result = "";
		if (items!=null)
		{
			for (int i=0;i<items.Count;i++)
			{
				if (i==0)
				{
					result = result + items[i];
				}
				else
				{
					result = result + token + items[i];
				}
			}
		}
		return result;
	}
	
	public static long getToken() {
		return System.DateTime.Now.Ticks;
	}

	public static bool isFileExisting(string filePath) {
		FileInfo info = new FileInfo(filePath);
		
		return !(info == null || info.Exists == false);

	}

	public static string bytes2string(byte[] bytes) {
		return System.Text.Encoding.UTF8.GetString(bytes);
	}

	public static void readStreamingAssets(string filePath, out string content) {
		if (filePath.Contains("://")) {
			WWW www = new WWW(filePath);
			while(!www.isDone) {}
			content = www.text;
		} else {
			content = System.IO.File.ReadAllText(filePath);
		}
	}

	public static void readStreamingAssets(string filePath, out byte[] content) {
		if (filePath.Contains("://")) {
			WWW www = new WWW(filePath);
			while(!www.isDone) {}
			content = www.bytes;
		} else {
			content = System.IO.File.ReadAllBytes(filePath);
		}
	}

	public static void writeFile(string filePath, byte[] content) {
		File.WriteAllBytes(filePath, content);
	}
	
	/// <summary>
	/// Parses the string.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="src">Source.</param>
	/// <param name="token">Token.</param>
	public static IList ParseString(string src, char token) 
	{
		string tmp = src;
		tmp = tmp.Trim();
		tmp = tmp.Replace(" ", "");
		return tmp.Split(token);
	}

	/// <summary>
	/// Gets the GUID.
	/// </summary>
	/// <returns>The GUID.</returns>
	public static string getGuid() {
		return Guid.NewGuid().ToString().Replace("-", "");;
	}

	/// <summary>
	/// B64decode the specified src.
	/// </summary>
	/// <param name="src">Source.</param>
	public static byte[] b64decode(string src)
	{
		return Convert.FromBase64String(src);
	}

	/// <summary>
	/// B64encode the specified src.
	/// </summary>
	/// <param name="src">Source.</param>
	public static string b64encode(byte[] src)
	{
		return Convert.ToBase64String(src);
	}

	/// <summary>
	/// Compose the specified src, key, t and val.
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="key">Key.</param>
	/// <param name="t">T.</param>
	/// <param name="val">Value.</param>
	public static void compose(ref JsonData src, string key, Type t, object val) {
		// FIXME: find a better way for comparing the type
		if(t.ToString().Equals("System.Byte[]")) {
			byte[] bb = val as Byte[];
			src[key] = Utils.b64encode(bb);
		} else if (t.ToString().Equals("System.DBNull")) {
			src[key] = null;
		} else if (t.ToString().Equals("System.DateTime")) {
			src[key] = new JsonData(val.ToString());
		} else {
			src[key] = new JsonData(val);
		}
	}

	/// <summary>
	/// Gets the key value.
	/// </summary>
	/// <returns>The key value.</returns>
	/// <param name="data">Data.</param>
	/// <param name="key">Key.</param>
	public static JsonData getKeyValue(JsonData data, string key) {
		JsonData rc = null;
		if( data != null) {
			try {
				rc = data[key];
			} catch {
				Utils.print(LogLevel.DEBUG, "can't find {0} in {1}", key, data);
			}
		}
		return rc;
	}
	
	public static T to<T> (object obj) {
		T rc;
		rc = (T)obj;
		return rc;
	}
	
}

public class TraceLog {
	private string _s = "";	
	private long _start = 0;
	public void time() {
		Utils.print(LogLevel.DEBUG, "time cost: {0}ms", (System.DateTime.Now.Ticks - _start)/10000); 
	}
	public TraceLog(string s) {
		_start = System.DateTime.Now.Ticks;
		_s = s; 
		Utils.print(LogLevel.DEBUG, "------>>>>>> {0}", _s); 
	}
	~TraceLog() { 
		Utils.print(LogLevel.DEBUG, "<<<<<<------ {0}", _s); 
	}
}
