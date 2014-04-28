using UnityEngine;
using System.Collections;

public class LootCounterBehaviour : MonoBehaviour {

	public TextMesh text;

	private int score;
	private int hullPercent;

	// Use this for initialization
	void Start () {
		text.text = "$ 0";
	}
	
	// Update is called once per frame
	void Update () {
		text.text = getLootString();
	}

	public void addLoot(int lootValue) {
		score += lootValue;
		text.text = getLootString();
	}

	private string getLootString() {
		return "$ " + score.ToString("#,##0");
	}

	public int getLootInt(){
		return score;
	}
}
