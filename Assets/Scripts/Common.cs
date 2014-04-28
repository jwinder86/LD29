using UnityEngine;
using System.Collections;

public class Common {


	public static void playSound(AudioSource audioSource, AudioClip sound){
		if (!audioSource.isPlaying) {
			audioSource.clip = sound;
			audioSource.Play();
		} else {
		}
	}

}
