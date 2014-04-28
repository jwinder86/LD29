using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	public float flapAngle;
	public float flapSpeed;

	private float start;

	// Use this for initialization
	void Start () {
		start = Random.Range(0f, Mathf.PI * 2f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(0f, 0f, getRot(start));
		start += Time.deltaTime * flapSpeed;
	}

	private float getRot(float timer) {
		return flapAngle * Mathf.Sin(timer * flapSpeed);
	}
}
