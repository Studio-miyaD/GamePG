using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottonSound : MonoBehaviour {

	public AudioSource bottonSound;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		bottonSound = audioSource;
	}

	public void PushBotton() {
		audioSource.PlayOneShot (bottonSound.clip);
	}
}
