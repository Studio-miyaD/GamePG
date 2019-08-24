using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHolo : MonoBehaviour
{
	Rigidbody2D rigidbody2D;
	public int speed = 2;
	//爆発処理1
	public GameObject explosion;
	//HP
	public int attackPoint = 10;
	public GameObject item;
	private Life lifeScript;
	//待機
	private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	//カメラに写っているかの判定
	private bool _isRendered = false;
	//Ghost
	public int ghost_m = 1;
	float TimeCount = 4;

	float attackCount = 1;
	public GameObject bullet;

	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
	}

	void Update()
	{
		//ghost
		TimeCount -= Time.deltaTime;
		if (_isRendered) {
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		}
		if (TimeCount <= 0) {
			//画像をx軸のみに対して反転
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			if (ghost_m == 1) {
				speed = -2;
				ghost_m = 0;
			} else {
				speed = 2;
				ghost_m = 1;
			}
			TimeCount = 4;
		}
		//gameover
		if (gameObject.transform.position.y < Camera.main.transform.position.y - 8
			|| gameObject.transform.position.x < Camera.main.transform.position.x - 10) {
			Destroy (gameObject);
		}
		//attack
		attackCount -= Time.deltaTime;
		if (attackCount <= 0) {
			if (ghost_m == 1) {
				Instantiate (bullet, transform.position + new Vector3 (1.2f, 0f, 0f), transform.rotation);
			} else {
				Instantiate (bullet, transform.position + new Vector3 (-1.2f, 0f, 0f), transform.rotation);
			}
			attackCount = 1;
		}
	}

	//爆発処理2
	void OnTriggerEnter2D(Collider2D col)
	{
		if (_isRendered) {
			if (col.tag == "Bullet") {
				Destroy (gameObject);
				Instantiate (explosion, transform.position, transform.rotation);
				if (Random.Range (0, 2) == 0) {
					Instantiate (item, transform.position, transform.rotation);
				}
			}
		}
	}

	//HP
	void OnCollisionEnter2D (Collision2D col)
	{
		//unitychanとぶつかった時
		if (col.gameObject.tag == "UnityChan") {
			//LifeScriptのLifeDownメソッドを実行
			lifeScript.LifeDown(attackPoint);
		}
	}

	//待機
	void OnWillRenderObject() {
		//メインカメラに映った時だけ_isRenderedをtrue
		if (Camera.current.tag == MAIN_CAMERA_TAG_NAME) {
			_isRendered = true;
		}
	}
}
