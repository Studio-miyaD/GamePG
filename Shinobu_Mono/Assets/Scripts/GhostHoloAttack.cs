using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHoloAttack : MonoBehaviour {
	private int gm;
	private int speed = 3;
	public int attackPoint = 10;
	private Life lifeScript;
	private int direction;
	private bool direction_ = false;
	public GameObject enemy;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("UnityChan");
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
		Destroy(gameObject, 4);
	}
	void Update() {
		if (player == null || enemy == null) { return; }
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2(speed * direction, player.transform.position.y - transform.position.y);
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Enemy" && direction_ == false) {
			enemy = col.gameObject;
			direction = enemy.GetComponent<GhostHolo>().ghost_m;
			direction_ = true;
		}
		if (col.gameObject.tag == "UnityChan") {
			lifeScript.LifeDown (attackPoint);
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Fireboll" || col.tag == "Shuriken" || col.tag == "Kunai") {
			Destroy(gameObject);
		}
		if (col.tag == "AbyssZone") {
			Destroy(gameObject);
		}
	}
}
