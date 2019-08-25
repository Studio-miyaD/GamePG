using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
	public float attackPoint = 10f;
	// Use this for initialization
	private Life lifeScript;
	public float timeCount = 0;

	public bool isCount = false;

	void Start() {
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
	}

	void Update() {
		if (isCount) {
			if (timeCount > 0.8) {
				lifeScript.LifeDown(attackPoint);
				timeCount = 0;
			}
			timeCount += Time.deltaTime;
		}
	}

	//オブジェクトが衝突したとき
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "UnityChan") {
			isCount = true;
			lifeScript.LifeDown(attackPoint);
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "UnityChan") {
			isCount = false;
		}
	}
}
