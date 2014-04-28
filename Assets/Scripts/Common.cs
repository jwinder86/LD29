using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Common : MonoBehaviour {


	public static void playSound(AudioSource audioSource, AudioClip sound){
		if (!audioSource.isPlaying) {
			audioSource.clip = sound;
			audioSource.Play();
		}
		
	}

}
