using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(FadeBehaviour))]
public class WaterClock : MonoBehaviour {
	private static CameraBehaviour screen = null;
	private static float[] tickTimes = {5f, 4f, 3f, 2.5f, 2f, 1.5f, 1f, 0.75f, 0.5f, 0.25f, -100f};
	
	public float waterMax = 60f;
	public float pumpRate = 4f;
	public float damageInc = 0.2f;

	public TimerBarBehaviour display;
	public TextMesh hullText;

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
	public AudioClip geiserSound;
	
	private int tickIndex;
	
	
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
		hullText.text = "hull: 100";
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
				Common.playSound(audio, pumpSound);

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


		if(Input.GetKeyDown(KeyCode.Escape)){
			StopAllCoroutines();
			StartCoroutine(ExitLevel(false));
		}

		display.setStatus(waterLevel / waterMax, waterLevel);
	}
	
	
	public void GameOver() {
		if (gameRunning) {
			waterLevel = waterMax;
			pig.Die();
			sub.sinkSub();
			StartCoroutine(ExitLevel(true));
			
			gameRunning = false;
		}
	}
	
	public bool isGameRunning(){
		return gameRunning;
	}
	
	
	public void setPumpingWater(bool isPumping){
		pumpingWater = isPumping;
	}

	public bool isPumping(){
		return pumpingWater;
	}
	

	private IEnumerator ExitLevel(bool wait) {
		if (wait) {
			yield return new WaitForSeconds(10f);
		}

		FadeBehaviour fade = this.GetComponent<FadeBehaviour>();
		fade.FadeOut();
		yield return new WaitForSeconds(fade.fadeTime);
		Application.LoadLevel("TitleScene");
	}

	public void takeDamage(){
		if (screen == null) {
			screen = (CameraBehaviour) FindObjectOfType(typeof(CameraBehaviour));
		}
		screen.HeavyShakeTime(0.7f);

		Debug.Log("Taking Damage!");
		leakMultiplyer = leakMultiplyer + damageInc;

		float hullHealth = getHullHealth();
		if (hullHealth < 100f && damageLeaks.Length > 0 && !damageLeaks[0].isPlaying) {
			damageLeaks[0].Play();
		}

		if (hullHealth <= 70f && damageLeaks.Length > 1 && !damageLeaks[1].isPlaying) {
			damageLeaks[1].Play();
		}

		if (hullHealth <= 50f && damageLeaks.Length > 2 && !damageLeaks[2].isPlaying) {
			damageLeaks[2].Play();
			audio.PlayOneShot(geiserSound);
		}

		if (hullHealth <= 20f && damageLeaks.Length > 3 && !damageLeaks[3].isPlaying) {
			damageLeaks[3].Play();
			audio.PlayOneShot(geiserSound);
		}

		if (hullHealth <= 20) {
			hullText.color = Color.red;
		}
		hullText.text = "hull: " + Mathf.RoundToInt(hullHealth / 10f) + "0";
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
