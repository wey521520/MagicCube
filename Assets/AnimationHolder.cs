using UnityEngine;
using System.Collections;

public class AnimationHolder : MonoBehaviour
{
	public RectTransform tran;
	private float anitime = 0.4f;

	void Start ()
	{
		Init ();
	}

	void Init ()
	{
		tran.anchoredPosition = new Vector2 (0f, -tran.rect.height - 30f);
	}

	public void SetShow (bool show)
	{
		StopAllCoroutines ();
		StartCoroutine (DoAnimation (show));
	}

	private IEnumerator DoAnimation (bool showing)
	{
		if (tran == null) {
			tran = GetComponent <RectTransform> ();
		}
		float t = 0f;
		float offset = 0f;
		float speed = 1f / anitime * (tran.rect.height + 30f);
		if (!showing) {
			speed *= -1f;
		}
		while (t < anitime) {
			yield return null;
			t += Time.deltaTime;
			if (t > anitime) {
				t = anitime;
			}
			offset = speed * Time.deltaTime;
			tran.anchoredPosition += new Vector2 (0f, offset);
			if (tran.anchoredPosition.y >= 0) {
				tran.anchoredPosition = Vector2.zero;
			} else if (tran.anchoredPosition.y <= -tran.rect.height - 30f) {
				tran.anchoredPosition = new Vector2 (0f, -tran.rect.height - 30f);
			}
		}
	}

}
