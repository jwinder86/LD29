using UnityEngine;
using System.Collections;

public class UndulateBehaviour : MonoBehaviour {

	public float speed;
	public float amplitude;
	public bool scale;

	private float startOne, startTwo, startThree;
	private Vector3 original;

	// Use this for initialization
	void Start () {
		if (scale) {
			original = transform.localScale;
		} else {
			original = transform.position;
		}

		startOne = Random.Range(0f, Mathf.PI * 2f);
		startTwo = Random.Range(0f, Mathf.PI * 2f);
		startThree = Random.Range(0f, Mathf.PI * 2f);
	}
	
	// Update is called once per frame
	void Update () {
		startOne += Time.deltaTime * Mathf.PI * speed;
		startTwo += Time.deltaTime * Mathf.PI * speed;
		startThree += Time.deltaTime * Mathf.PI * speed;

		float x = amplitude * Mathf.Sin(startOne);
		float y = amplitude * Mathf.Sin(startTwo);
		float z = amplitude * Mathf.Sin(startThree);

		if (scale) {
			transform.localScale = original + new Vector3(x, y, z);
		} else {
			transform.position = original + new Vector3(x, y, z);
		}
	}
}
