using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyGUI : MonoBehaviour
{
	public GameObject statebuttoncontainer;
	public Text statetext;
	public RectTransform statebt1;
	public RectTransform statebt2;
	public RectTransform statebt3;

	void Start ()
	{
		statebt1.anchoredPosition = new Vector2 (0f, -statebt1.rect.height);
		statebt2.anchoredPosition = new Vector2 (0f, -2f * statebt2.rect.height);
		statebt3.anchoredPosition = new Vector2 (0f, -3f * statebt3.rect.height);

		statebuttoncontainer.SetActive (false);
	}

	public void StateButtonClick ()
	{
		statetext.text = "";
		statebuttoncontainer.SetActive (true);
	}

	public void StateButtonSelect ()
	{
		statebuttoncontainer.SetActive (false);
	}
}
