using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public Renderer subFrontRenderer;

	public float nearDistance;
	public float farDistance;

	public float zoomTime;

	public Color startColor;
	public float startDepth;
	public Color endColor;
	public float endDepth;

	// Use this for initialization
	void Start () {
		RenderSettings.fog = true;
		RenderSettings.fogDensity = 0.005f;
		RenderSettings.fogStartDistance = 80f;
		RenderSettings.fogEndDistance = 300f;
		RenderSettings.fogMode = FogMode.Linear;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > startDepth) {
			setBackgroundColor(startColor);
		} else if (transform.position.y < endDepth) {
			setBackgroundColor(endColor);
		} else {
			float frac = (transform.position.y - endDepth) / (startDepth - endDepth);
			setBackgroundColor(Color.Lerp(endColor, startColor, frac));
		}
	}

	public void zoomCamera(bool zoomOut) {
		StopAllCoroutines();
		StartCoroutine(ZoomCoroutine(zoomOut));
	}

	private IEnumerator ZoomCoroutine(bool zoomOut) {
		float startDistance = transform.localPosition.z;
		subFrontRenderer.enabled = true;
		
		for (float time = 0; time <= zoomTime; time += Time.deltaTime) {
			if (zoomOut) {
				transform.localPosition = newDistance(Mathf.Lerp(startDistance, farDistance, time / zoomTime));
				setSubFrontAlpha(time / zoomTime);
			} else {
				transform.localPosition = newDistance(Mathf.Lerp(startDistance, nearDistance, time / zoomTime));
				setSubFrontAlpha(1f - time / zoomTime);
			}
			
			yield return null;
		}
		
		if (zoomOut) {			
			transform.localPosition = newDistance(farDistance);
			setSubFrontAlpha(1f);
		} else {
			transform.localPosition = newDistance(nearDistance);
			setSubFrontAlpha(0f);
			subFrontRenderer.enabled = false;
		}
	}

	private Vector3 newDistance(float distance) {
		return new Vector3(transform.localPosition.x, transform.localPosition.y, distance);
	}

	private void setSubFrontAlpha(float alpha) {
		Color oldColor = subFrontRenderer.material.color;
		subFrontRenderer.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
	}

	private void setBackgroundColor(Color color) {
		Camera.main.backgroundColor = color;
		RenderSettings.fogColor = color;
	}
}
