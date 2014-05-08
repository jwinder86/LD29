using UnityEngine;
using System.Collections;

public class TitleScoreBehavior : MonoBehaviour {
	public TextMesh text;
	public TextMesh winText;

	public GameObject[] loots;

	// Use this for initialization
	void Start () {
		if (ScoreHolder.getScore() > 0) {
			text.text = "Treasure collected worth:\n" + ScoreHolder.getLootString();
		} else {
			text.text = "\nNo treasure collected.";
		}

		if (winText != null && ScoreHolder.getExitState() == ScoreHolder.ExitState.Won && ScoreHolder.getScore() == 0) {
			winText.text = "You Won?";

			foreach (GameObject loot in loots) {
				Renderer[] renderers = loot.GetComponentsInChildren<Renderer>();
				foreach (Renderer render in renderers) {
					render.enabled = false;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//text.text = ScoreHolder.getLootString();
	}
}
