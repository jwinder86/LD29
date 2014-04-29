using UnityEngine;
using System.Collections;

public class TitleScoreBehavior : MonoBehaviour {
	public TextMesh text;

	// Use this for initialization
	void Start () {
		if (ScoreHolder.getScore() > 0) {
			text.text = "Treasure collected worth:\n" + ScoreHolder.getLootString();
		} else {
			text.text = "\nNo treasure collected.";
		}
	}
	
	// Update is called once per frame
	void Update () {
		//text.text = ScoreHolder.getLootString();
	}
}
