using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour {
	private int gm;
	private int speed = 3;
	public int attackPoint = 10;
	private Life lifeScript;

	// Use this for initialization
	void Start () {
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
		rigidbody2D.velocity = new Vector2(0f, speed * rigidbody2D.velocity.y);
		Destroy(gameObject, 4);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "UnityChan") {
			lifeScript.LifeDown (attackPoint);
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "bullet") {
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "AbyssZone") {
			Destroy(gameObject);
		}
	}
}
