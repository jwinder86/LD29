using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class PumpWarningBehavior : MonoBehaviour {

	public AudioClip warning1Sound;
	public AudioClip warning2Sound;

	public WaterClock waterClock;

	public float warning1Limit = 0.5f;
	public float warning2Limit = 0.75f;

	private float waterLevel;
	private float waterMax;
	private bool warning1Active;

	// Use this for initialization
	void Start () {
		waterMax = waterClock.getWaterMax();
	}
	
	// Update is called once per frame
	void Update () {
	
		waterLevel = waterClock.getWaterLevel();

//		if(!audio.isPlaying){
//			playSound(warning1Sound);
//		}

//		if(!audio.isPlaying){
//			Debug.Log(!audio.isPlaying);
//			if (waterLevel/waterMax > warning1Limit && !audio.isPlaying){
//				Debug.Log("playing warning 1");
//				playSound(warning1Sound);
//				warning1Active = true;
//			}else{
//				audio.Stop();
//			}
//		}
		if(!audio.isPlaying){
			if (waterLevel/waterMax > warning1Limit && waterLevel/waterMax  < warning2Limit){
				//Debug.Log("playing warning 1");
				//playSound(warning1Sound);
				Common.playSound(this.audio, warning1Sound);
				
			}else if(waterLevel/waterMax > warning2Limit){
				//playSound(warning2Sound);
				Common.playSound(this.audio, warning2Sound);
			}else{
				audio.Stop();
			}
		}
	}
	private void playSound(AudioClip sound){
		if (!audio.isPlaying) {
			audio.clip = sound;
			audio.Play();
		}
		
	}
}
