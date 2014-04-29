using UnityEngine;
using System.Collections;

public class TitleScoreBehavior : MonoBehaviour {
	public TextMesh text;

	// Use this for initialization
	void Start () {
		text.text = ScoreHolder.getLootString();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = ScoreHolder.getLootString();
	}
}
