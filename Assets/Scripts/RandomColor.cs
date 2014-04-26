using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

	public Color[] altColors;

	// Use this for initialization
	void Start () {
		int index = Random.Range(0, altColors.Length);

		if (index < altColors.Length) {
			renderer.material.color = altColors[index];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
