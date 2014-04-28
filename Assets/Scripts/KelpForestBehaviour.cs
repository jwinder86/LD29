using UnityEngine;
using System.Collections;

public class KelpForestBehaviour : MonoBehaviour {

	public KelpScript kelpPrefab;
	public int kelpCount;

	public int minSegments;
	public int maxSegments;

	public float minSize;
	public float maxSize;

	public float spacing;
	public float amplitudeOne;
	public float frequencyOne;
	public float amplitudeTwo;
	public float frequencyTwo;
	public float speed;

	public Color[] colors;

	// Use this for initialization
	void Start () {
		if (renderer != null) {
			renderer.enabled = false;
		}

		for (int i = 0; i < kelpCount; i++) {
			// randomly place
			float xPos = Random.Range(-0.5f, 0.5f);
			float zPos = Random.Range(-0.5f, 0.5f);
			Vector3 kelpPos = transform.TransformPoint(new Vector3(xPos, 0f, zPos));

			// create kelp
			KelpScript kelp = (KelpScript) Instantiate(kelpPrefab, kelpPos, Quaternion.identity);
			kelp.segments = Random.Range(minSegments, maxSegments);

			// set colors
			Color start = colors[Random.Range(0, colors.Length)];
			start.a = 0.75f;
			Color end = colors[Random.Range(0, colors.Length)];
			end.a = 0.5f;
			float size = Random.Range(minSize, maxSize);
			LineRenderer line = kelp.GetComponent<LineRenderer>();
			line.SetColors(start, end);
			line.SetWidth(size * 0.75f, size);

			// set other parameters
			kelp.spacing = spacing;
			kelp.amplitudeOne = amplitudeOne;
			kelp.frequencyOne = frequencyOne;
			kelp.amplitudeTwo = amplitudeTwo;
			kelp.frequencyTwo = frequencyTwo;
			kelp.speed = speed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
