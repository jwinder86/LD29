using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (TextMesh))]
public class TextBehaviour : MonoBehaviour {

	private static Dictionary<string, string> textTable;

	private TextMesh text;
	public string textKey;

	// Use this for initialization
	void Start () {
		if (textTable == null) {
			buildTextTable();
		}

		text = GetComponent<TextMesh>();

		if (textTable.ContainsKey(textKey)) {
			text.text = textTable[textKey];
		} else {
			Debug.LogError("Text Key not found: " + textKey);
			text.text = "MISSING";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void buildTextTable() {
		textTable = new Dictionary<string, string>();

		textTable.Add("moveInstructions1", 
		              "This is your submarine.  You\n" +
		              "can move around it using the\n" +
		              "WASD or arrow keys.  Use\n" +
		              "[space] to interact with the\n" +
		              "terminals.");

		textTable.Add("steerInstructions1",
		              "Use the Steering Terminal to\n" +
		              "navigate your submarine.  The\n" +
		              "WASD keys will move the\n" +
		              "submarine until you leave\n" +
		              "the terminal by pressing [space].");

		textTable.Add("fireInstructions1",
		              "The Weapons Terminal can be used to fire torpedos.\n" +
		              "Aim using the mouse and click [left-mouse] to\n" +
		              "launch a torpedo.\n\n" +
		              "Watch out for other submarines that want to steal\n" +
		              "your treasure.");

		textTable.Add("grappleInstructions1",
		              "The Grappling Hook Terminal\n" +
		              "can be used to collect treasure\n" +
		              "from the ocean floor.  Aim using\n" +
		              "the mouse and click [left-mouse]\n" +
		              "to fire the grappling hook.\n\n" +
		              "Collect lots of treasure to increase\n" +
		              "your score!");

		textTable.Add("pumpInstructions1",
		              "As your submarine is damaged,\n" +
		              "it will begin to leak faster\n" +
		              "and faster.  When it is \n" +
		              "entirely filled with water\n" +
		              "you will drown and the game\n" +
		              "will be over.\n\n" +
		              "The Pump Terminal can be\n" +
		              "used to pump water out of\n" +
		              "your submarine.");

		textTable.Add("goalInstructions1",
		              "Can you find the treasures of these\n" +
		              "depths and return with them alive?");
	}
}
