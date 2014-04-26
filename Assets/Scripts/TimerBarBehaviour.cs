using UnityEngine;
using System.Collections;

public class TimerBarBehaviour : MonoBehaviour {
	
	public Transform bar;
	//public TextMesh text;
	
	public Color fullColor;
	public Color emptyColor;
	

	
	private Vector3 parentInitialScale;
	private Vector3 initialScale;
	private Vector3 initialPosition;
	
	// Use this for initialization
	void Start () {
		parentInitialScale = transform.localScale;
		initialScale = bar.localScale;
		initialPosition = bar.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		//	float scaleMult = Mathf.Sin(Time.timeSinceLevelLoad * Mathf.PI ) + 1f;
		//	transform.localScale = parentInitialScale * scaleMult;
	}
	
	public void setStatus(float fraction, float value) {
		//Debug.Log("fraction:value:" + fraction + " : " + value);
		bar.transform.localScale = new Vector3(initialScale.x, initialScale.y * fraction, initialScale.z);
		bar.transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + (fraction - 1f) * initialScale.y / 2f, initialPosition.z);
		bar.renderer.material.color = Color.Lerp(emptyColor, fullColor, fraction);
		bar.renderer.enabled = fraction > 0f;
		//text.text = value.ToString("F2");
		//text.color = Color.Lerp(emptyColor, fullColor, fraction);
	}
	

}
