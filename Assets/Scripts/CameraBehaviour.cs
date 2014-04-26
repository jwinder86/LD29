using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public float nearDistance;
	public float farDistance;

	public float zoomTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void zoomCamera(bool zoomOut) {
		StopAllCoroutines();
		StartCoroutine(ZoomCoroutine(zoomOut));
	}

	private IEnumerator ZoomCoroutine(bool zoomOut) {
		float startDistance = transform.localPosition.z;
		
		for (float time = 0; time <= zoomTime; time += Time.deltaTime) {
			if (zoomOut) {
				transform.localPosition = newDistance(Mathf.Lerp(startDistance, farDistance, time / zoomTime));
			} else {
				transform.localPosition = newDistance(Mathf.Lerp(startDistance, nearDistance, time / zoomTime));
			}
			
			yield return null;
		}
		
		if (zoomOut) {			
			transform.localPosition = newDistance(farDistance);
		} else {
			transform.localPosition = newDistance(nearDistance);
		}
	}

	private Vector3 newDistance(float distance) {
		return new Vector3(transform.localPosition.x, transform.localPosition.y, distance);
	}
}
