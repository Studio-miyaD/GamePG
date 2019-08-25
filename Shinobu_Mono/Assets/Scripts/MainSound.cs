using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour {

	public AudioClip main;
	public AudioClip gameover;
	public AudioClip gameclear;
	private AudioSource audioSource;

	private Life lifeScript;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = main;
		audioSource.Play ();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeScript.gameOver == true) {
			audioSource.Stop ();
		}
		
	}
}
