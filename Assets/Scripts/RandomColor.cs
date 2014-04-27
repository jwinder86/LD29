﻿using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

	public Color[] altColors;
	public bool update = false;
	private int index = 0;

	// Use this for initialization
	void Start () {
		setRandomColor();

		if (update) {
			StartCoroutine(UpdateColor());
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void setRandomColor() {
		index += Random.Range(0, altColors.Length - 1) % altColors.Length;
		
		if (index < altColors.Length) {
			renderer.material.color = altColors[index];
		}
	}

	private IEnumerator UpdateColor() {
		while (true) {
			setRandomColor();

			yield return new WaitForSeconds(Random.Range(1f, 8f));
		}
	}
}
