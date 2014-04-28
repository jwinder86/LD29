using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
public class KelpScript : MonoBehaviour {

	public int segments;
	public float spacing;

	public float amplitudeOne;
	public float frequencyOne;
	public float amplitudeTwo;
	public float frequencyTwo;
	public float speed;

	private LineRenderer line;
	private float startOne;
	private float startTwo;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();

		startOne = Random.Range(0f, Mathf.PI * 2f);
		startTwo = Random.Range(0f, Mathf.PI * 2f);
	}
	
	// Update is called once per frame
	void Update () {
		line.SetVertexCount(segments);

		for (int i = 0; i < segments; i++) {
			line.SetPosition(i, new Vector3(transform.position.x + getOffsetOne(i) + getOffsetTwo(i), transform.position.y + spacing * i, transform.position.z));
		}

		startOne += Time.deltaTime / frequencyOne * Mathf.PI * speed;
		startTwo += Time.deltaTime / frequencyTwo * Mathf.PI * speed;
	}

	float getOffsetOne(int position) {
		return amplitudeOne * Mathf.Sin(startOne - (position * spacing) / frequencyOne) * (position / (float)segments);
	}

	float getOffsetTwo(int position) {
		return amplitudeTwo * Mathf.Sin(startTwo - (position * spacing) / frequencyTwo) * (position / (float)segments);
	}
}
