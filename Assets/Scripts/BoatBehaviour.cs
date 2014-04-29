using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class BoatBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("boat entered: " + other);
		SubExteriorBehaviour sub = other.GetComponent<SubExteriorBehaviour>();
		if (sub != null) {
			LootCounterBehaviour loot = (LootCounterBehaviour) FindObjectOfType(typeof(LootCounterBehaviour));
			int score = loot.getLootInt();

			ScoreHolder.exitWin(loot.getLootInt());
			StartCoroutine(ExitLevel());
		}
	}

	private IEnumerator ExitLevel() {
		FadeBehaviour fade = FindObjectOfType(typeof(FadeBehaviour)) as FadeBehaviour;
		fade.FadeOut();
		yield return new WaitForSeconds(fade.fadeTime);
		Application.LoadLevel("TitleScene");
	}
}
