using UnityEngine;
using System.Collections;

public class LootCounterBehaviour : MonoBehaviour {

	public TextMesh text;

	private int score;

	// Use this for initialization
	void Start () {
		text.text = "$ 0";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void addLoot(int lootValue) {
		score += lootValue;
		text.text = getLootString();
	}

	private string getLootString() {
		return "$ " + score.ToString("#,##0");
	}
}
