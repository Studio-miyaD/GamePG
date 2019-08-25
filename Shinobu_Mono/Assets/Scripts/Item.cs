using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int healPoint = 20;
	private Life lifeScript;
	//効果音
	public AudioClip healSound;
	AudioSource audioSource;


	void Start () {
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();

		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = healSound;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//ユニティちゃんと衝突した時
		if (col.gameObject.tag == "UnityChan") {
			//LifeUpメソッドを呼び出す　引数はhealPoint
			lifeScript.LifeUp(healPoint);
			AudioSource.PlayClipAtPoint (healSound, transform.position);
			//アイテムを削除する
			Destroy(gameObject);
		}
	}
}
