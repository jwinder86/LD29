using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	private static readonly float X_COEF = 5f;
	private static readonly float Y_COEF = 7f;
	
	public float shakeMagnitude;
	public float shakeSpeed;
	
	public float heavyShakeMagnitude;
	public float heavyShakeSpeed;
	
	private float shakeTimer;
	private float heavyShakeTimer;

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
		if (shakeTimer > 0f) {
			shakeTimer -= Time.deltaTime;
		}
		
		if (heavyShakeTimer > 0f) {
			heavyShakeTimer -= Time.deltaTime;
		}

		if (transform.position.y > startDepth) {
			setBackgroundColor(startColor);
		} else if (transform.position.y < endDepth) {
			setBackgroundColor(endColor);
		} else {
			float frac = (transform.position.y - endDepth) / (startDepth - endDepth);
			setBackgroundColor(Color.Lerp(endColor, startColor, frac));
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPosition = new Vector3(0f, 0f, transform.localPosition.z);
		
		if (heavyShakeTimer > 0f) {
			newPosition += heavyShakeMagnitude * heavyShakeTimer * new Vector3(Mathf.Sin(Time.time * heavyShakeSpeed * X_COEF), Mathf.Cos(Time.time * heavyShakeSpeed * Y_COEF), 0f);
		}else if (shakeTimer > 0f) {
			newPosition += shakeMagnitude * shakeTimer * new Vector3(Mathf.Sin(Time.time * shakeSpeed * X_COEF), Mathf.Cos(Time.time * shakeSpeed * Y_COEF), 0f);
		}
		
		transform.localPosition = newPosition;
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
