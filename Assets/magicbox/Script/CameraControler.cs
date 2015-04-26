using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class CameraControler : MonoBehaviour
{

	// public Camera mycam;
	// Use this for initialization

	public Camera MainCamera;
	public Camera MirrorCamera;
	public Camera BackCamera;
	public MirrorReflection mirror;

	private float spliterate = 0.4f;

	void Start ()
	{
		SetMainScreen ();
	}

	void SetSpliteScreen ()
	{
		BackCamera.enabled = false;
		MirrorCamera.enabled = true;

		MirrorCamera.depth = 0f;
		float sx = Screen.width;
		float sy = Screen.height * spliterate;
		float cs = Mathf.Min (sx, sy);
		Rect nr = new Rect (Screen.width / 2f - cs / 2f, Screen.height - Screen.height * spliterate, cs, cs);
		mirror.m_TextureSize = (int)nr.size.x;
		MirrorCamera.pixelRect = nr;

		float sxm = Screen.width;
		float sym = Screen.height * (1f - spliterate);
		float csm = Mathf.Min (sxm, sym);
		Rect nrm = new Rect (Screen.width / 2f - csm / 2f, Screen.height * (1f - spliterate) - csm, csm, csm);
		MainCamera.pixelRect = nrm;
	}

	void SetBackScreen ()
	{
		MirrorCamera.enabled = false;
		BackCamera.enabled = true;

		BackCamera.depth = 0f;
		float sx = Screen.width;
		float sy = Screen.height * spliterate;
		float cs = Mathf.Min (sx, sy);
		Rect nr = new Rect (Screen.width / 2f - cs / 2f, Screen.height - Screen.height * spliterate, cs, cs);
		BackCamera.pixelRect = nr;

		float sxm = Screen.width;
		float sym = Screen.height * (1f - spliterate);
		float csm = Mathf.Min (sxm, sym);
		Rect nrm = new Rect (Screen.width / 2f - csm / 2f, Screen.height * (1f - spliterate) - csm, csm, csm);
		MainCamera.pixelRect = nrm;
	}

	void SetMainScreen ()
	{
		MirrorCamera.enabled = false;
		BackCamera.enabled = false;

		float sx = Screen.width;
		float sy = Screen.height;
		float cs = Mathf.Min (sx, sy);
		Rect nr = new Rect (Screen.width / 2f - cs / 2f, Screen.height / 2f - cs / 2f, cs, cs);

		MainCamera.pixelRect = nr;
		MirrorCamera.depth = -20f;
	}

	void OnGUI ()
	{
		if (GUILayout.Button ("mirror screen")) {
			SetSpliteScreen ();
		}
		if (GUILayout.Button ("main screen")) {
			SetMainScreen ();
		}
		if (GUILayout.Button ("back screen")) {
			SetBackScreen ();
		}
	}
}
