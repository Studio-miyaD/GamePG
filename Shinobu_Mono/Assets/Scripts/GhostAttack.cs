using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour {

	private GameObject ghostHolo;
	private int speed = 3;
	public int attackPoint = 10;
	private Life lifeScript;

	// Use this for initialization
	void Start () {
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
		ghostHolo = GameObject.FindWithTag("Enemy");
		//int ran = Random.Range (-1, 2);
		rigidbody2D.velocity = new Vector2(speed * ghostHolo.transform.localScale.x, rigidbody2D.velocity.y);
		Destroy(gameObject, 10);
		
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "UnityChan") {
			lifeScript.LifeDown (attackPoint);
			Destroy (gameObject);
		}
	}
}
