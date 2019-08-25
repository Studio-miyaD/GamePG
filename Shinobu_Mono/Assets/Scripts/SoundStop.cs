using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStop : MonoBehaviour {

	AudioSource audioSource;
	private Life lifeScript;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
	}

	void Update () {
		if (lifeScript.gameOver == true) {
			audioSource.Stop ();
		}
	}
}
