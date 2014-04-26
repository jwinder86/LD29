using UnityEngine;
using System.Collections;

public class WaterClock : MonoBehaviour {

	private static float[] tickTimes = {5f, 4f, 3f, 2.5f, 2f, 1.5f, 1f, 0.75f, 0.5f, 0.25f, -100f};
	
	public float waterMax = 60f;
	public float pumpRate = 4f;
	public TimerBarBehaviour display;
	
	private float waterLevel;


	private PigBehaviour pig;
	private bool gameRunning;
	private bool pumpingWater;
	
	public string[] levelList;
	
	public AudioClip moreTime;
	public AudioClip tickSound;
	
	private int tickIndex;
	
	//FadeBehaviour fade;
	
	
	// Use this for initialization
	void Start () {
		waterLevel = 0;
		gameRunning = true;
		pumpingWater = false;
		pig = (PigBehaviour) FindObjectOfType(typeof(PigBehaviour));
		
		tickIndex = 0;
//		fade = this.GetComponent<FadeBehaviour>();
//		fade.FadeIn();
	}
	
	// Update is called once per frame
	void Update () {

//		if(Input.GetKeyDown ("f")){
//			Debug.Log("pumping water");
//			pumpingWater = true;
//		}

		//Debug.Log("gamerunning: " + gameRunning + "| waterLevel: " + waterLevel + "waterMax" + waterMax);
		if (gameRunning) {
			if(pumpingWater && waterLevel >= 0f){
				waterLevel = waterLevel - pumpRate*Time.deltaTime;
				if(waterLevel <0f){
					waterLevel = 0f;
				}
			}else if(waterLevel >= 0f){
				waterLevel = waterLevel + Time.deltaTime;
							
			} else {
				waterLevel = waterMax;
				//GameOver();
			}
			
			// don't allow the boredom clock to be higher than boredomMax
			if(waterLevel > waterMax){
				waterLevel = waterMax;
			}
		} else {
			if (waterLevel <= 0f) {

			}
		}
		
		//playTick();
		
		if(Input.GetKeyDown ("r")){
			Application.LoadLevel(Application.loadedLevel);
		}
		display.setStatus(waterLevel / waterMax, waterLevel);

	}
	
	
	public void increaseClock(float amount){
		if (gameRunning) {
			waterLevel -= amount;
			//display.setShakeTime(0.5f);
			
			//resetTick();
			
			audio.PlayOneShot(moreTime);
			
			//Debug.Log ("decreasing water: " + amount + " = " + waterLevel);
		}
	}
	
	
	public void GameOver() {
//		if (gameRunning) {
//			waterLevel = 0f;
//			pig.Die();
//			
//			StartCoroutine(ReloadLevel());
//			
//			gameRunning = false;
//		}
	}
	
	public bool isGameRunning(){
		return gameRunning;
	}
	
	
	public void WinGame() {
		if (gameRunning) {
			gameRunning = false;
			
			//StartCoroutine(loadNextLevel());
		}		
	}

	public void setPumpingWater(bool isPumping){
		pumpingWater = isPumping;
	}

	
//	private IEnumerator loadNextLevel() {
//		
//		yield return new WaitForSeconds(3f);
//		fade = this.GetComponent<FadeBehaviour>();
//		fade.FadeOut();
//		yield return new WaitForSeconds(2f);
//		
//		string nextLevel = levelList[0];
//		for (int i = 0; i < levelList.Length - 1; i++) {
//			if (levelList[i].Equals(Application.loadedLevelName)) {
//				nextLevel = levelList[i+1];
//			}
//		}
//		
//		Application.LoadLevel(nextLevel);
//	}
	
	private IEnumerator ReloadLevel() {
		yield return new WaitForSeconds(5f);
		Application.LoadLevel(Application.loadedLevel);
	}
	
//	private void playTick() {
//		if (waterLevel < tickTimes[tickIndex]) {
//			audio.PlayOneShot(tickSound);
//			tickIndex++;
//		}
//	}
	
//	private void resetTick() {
//		for (int i = 0; i < tickTimes.Length; i++) {
//			if (tickTimes[i] < waterLevel) {
//				tickIndex = i;
//				break;
//			}
//		}
//		
//		Debug.Log("New Tick Time" + tickIndex);
//	}
}
