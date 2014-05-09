using UnityEngine;
using System.Collections;

public class ReloadAnimBehaviour : MonoBehaviour {

	public float animTime;
	public float yDist;
	public float xDist;

	public bool playOnAwake = false;

	private Vector3 pos1, pos2, pos3;

	// Use this for initialization
	void Start () {
		pos2 = transform.localPosition;
		pos1 = pos2 - new Vector3(0f, yDist, 0f);
		pos3 = pos2 - new Vector3(xDist, 0f, 0f);

		transform.localPosition = pos1;

		if (playOnAwake) {
			InvokeRepeating("AnimateReload", 0f, animTime * 2);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimateReload() {
		StopAllCoroutines();
		StartCoroutine(AnimateCoroutine());
	}

	private IEnumerator AnimateCoroutine() {
		for (float timer = 0f; timer < animTime / 2f; timer += Time.deltaTime) {
			transform.localPosition = Vector3.Lerp(pos1, pos2, timer / (animTime / 2f));
			yield return null;
		}

		for (float timer = 0f; timer < animTime / 2f; timer += Time.deltaTime) {
			transform.localPosition = Vector3.Lerp(pos2, pos3, timer / (animTime / 2f));
			yield return null;
		}

		transform.localPosition = pos1;
	}
}
