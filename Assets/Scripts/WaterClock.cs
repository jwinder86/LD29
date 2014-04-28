using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class WaterClock : MonoBehaviour {
	private static CameraBehaviour screen = null;
	private static float[] tickTimes = {5f, 4f, 3f, 2.5f, 2f, 1.5f, 1f, 0.75f, 0.5f, 0.25f, -100f};
	
	public float waterMax = 60f;
	public float pumpRate = 4f;
	public float damageInc = 0.2f;

	public TimerBarBehaviour display;

	public ParticleSystem[] damageLeaks;
	
	private float waterLevel;


	private PigBehaviour pig;
	private MovementStationBehaviour sub;

	private bool gameRunning;
	private bool pumpingWater;
	private bool pumpBroke;
	public string[] levelList;

	public float leakMultiplyer;

	public AudioClip pumpSound;


	//public AudioClip moreTime;
	//public AudioClip tickSound;
	
	private int tickIndex;
	
	//FadeBehaviour fade;
	
	
	// Use this for initialization
	void Start () {
		waterLevel = 0f;
		leakMultiplyer = 0f;
		gameRunning = true;
		pumpingWater = false;
		pig = (PigBehaviour) FindObjectOfType(typeof(PigBehaviour));
		sub = (MovementStationBehaviour) FindObjectOfType(typeof(MovementStationBehaviour));
		pumpBroke=false;
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
			// activated pump
			if(pumpingWater && waterLevel > 0f && leakMultiplyer < (pumpRate/2)){
				 // pump still working
					waterLevel = waterLevel - pumpRate*Time.deltaTime;
					audio.PlayOneShot(pumpSound);

				if(waterLevel <0f){
					waterLevel = 0f;
				}
			}else if(leakMultiplyer >= (pumpRate/2) && !pumpBroke){
				leakMultiplyer += 8f;
				pumpBroke=true;
			}
			// water leaking
			else if(waterLevel >= 0f && leakMultiplyer > 0f){
				waterLevel = waterLevel + leakMultiplyer*Time.deltaTime;
				//audio.Stop();

			}
			// don't allow the water clock to be higher than waterMax
			if(waterLevel >= waterMax){
				waterLevel = waterMax;
				GameOver();
			}
		} 
//		else {
//			if (waterLevel <= 0f) {
//
//			}
//		}
		
		//playTick();


//		if(Input.GetKeyDown ("r")){
//			StartCoroutine(ReloadLevel());
//		}
//		if(Input.GetKeyDown ("f")){
//			takeDamage();
//		}
		display.setStatus(waterLevel / waterMax, waterLevel);

	}
	
	
//	public void increaseClock(float amount){
//		if (gameRunning) {
//			waterLevel -= amount;
//			//display.setShakeTime(0.5f);
//			
//			//resetTick();
//			
//			audio.PlayOneShot(moreTime);
//			
//			//Debug.Log ("decreasing water: " + amount + " = " + waterLevel);
//		}
//	}
	
	
	public void GameOver() {
		if (gameRunning) {
			waterLevel = waterMax;
			pig.Die();
			sub.sinkSub();
			StartCoroutine(ReloadLevel());
			
			gameRunning = false;
		}
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

	public bool isPumping(){
		return pumpingWater;
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
		yield return new WaitForSeconds(7f);
		Application.LoadLevel(Application.loadedLevel);
	}

	public void takeDamage(){
		if (screen == null) {
			screen = (CameraBehaviour) FindObjectOfType(typeof(CameraBehaviour));
		}
		screen.HeavyShakeTime(0.7f);

		Debug.Log("Taking Damage!");
		leakMultiplyer = leakMultiplyer + damageInc;

		if (getHullHealth() < 100f && damageLeaks.Length > 0 && !damageLeaks[0].isPlaying) {
			damageLeaks[0].Play();
		}

		if (getHullHealth() <= 70f && damageLeaks.Length > 1 && !damageLeaks[1].isPlaying) {
			damageLeaks[1].Play();
		}

		if (getHullHealth() <= 50f && damageLeaks.Length > 2 && !damageLeaks[2].isPlaying) {
			damageLeaks[2].Play();
		}

		if (getHullHealth() <= 20f && damageLeaks.Length > 2 && !damageLeaks[3].isPlaying) {
			damageLeaks[3].Play();
		}
	}

	public float getLeakMultiplyer(){
		return leakMultiplyer;
	}

	public float getWaterMax(){
		return waterMax;
	}
	public float getPumpRate(){
		return pumpRate;
	}
	public float getWaterLevel(){
		return waterLevel;
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

	public float getHullHealth(){
		float damage= getLeakMultiplyer();
		float pumpRate= getPumpRate()/2;
		float diff = pumpRate - damage;
		float hullPercent = (diff / pumpRate) * 100f;
		if (hullPercent < 0f){
			hullPercent = 0f;
		}
		return hullPercent;
	}
}
