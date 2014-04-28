using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class PumpWarningBehavior : MonoBehaviour {

	public AudioClip warning1Sound;
	public AudioClip warning2Sound;

	public WaterClock waterClock;

	public WarningLightBehaviour warningLight;

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

		if(!audio.isPlaying){
			if (waterLevel/waterMax > warning1Limit && waterLevel/waterMax  < warning2Limit){
				Common.playSound(this.audio, warning1Sound);
				warningLight.enableWarning(true);
			}else if(waterLevel/waterMax > warning2Limit){
				Common.playSound(this.audio, warning2Sound);
				warningLight.enableWarning(true);
			}else{
				audio.Stop();
				warningLight.enableWarning(false);
			}
		}
	}
}
