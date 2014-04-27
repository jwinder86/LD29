using UnityEngine;
using System.Collections;

public class LootCounterBehaviour : MonoBehaviour {

	public TextMesh text;

	public WaterClock waterClock;

	private int score;
	private int hullPercent;

	// Use this for initialization
	void Start () {
		text.text = "$ 0";
		waterClock = (WaterClock) FindObjectOfType(typeof(WaterClock));
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
		hullPercent = (int)waterClock.getHullHealth();
		return "Hull: " + hullPercent + "%     $ " + score.ToString("#,##0");
	}
}
