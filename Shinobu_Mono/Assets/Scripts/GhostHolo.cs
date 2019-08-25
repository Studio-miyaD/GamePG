using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHolo : MonoBehaviour
{
	Rigidbody2D rigidbody2D;
	public int speed = 2;
	[Header("耐久力")] public int endurance = 15;
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
	[Header("敵の行動のインターバル")]public int ghost_m = 1;
	float TimeCount = 4;

	float attackCount = 1;
	public GameObject bullet;
	//効果音
	public AudioClip enemyDestroy;

	public GameObject player;

	void Start()
	{
		player = GameObject.FindWithTag("UnityChan");
		rigidbody2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
	}

	void Update()
	{
		//ghost
		if (_isRendered) {
			TimeCount -= Time.deltaTime;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, speed);
			if(player.transform.position.x < gameObject.transform.position.x) {
				ghost_m = -1;
				//画像をx軸のみに対して反転
				transform.localScale = new Vector3(6, transform.localScale.y, transform.localScale.z);
			} else {
				ghost_m = 1;
				//画像をx軸のみに対して反転
				transform.localScale = new Vector3(-6, transform.localScale.y, transform.localScale.z);
			}
			
			// 数秒間に一度の間隔で変化
			
			if (TimeCount <= 0) {
				if (speed == 2) {
					speed = -2;
					//ghost_m = -1;
				} else {
					speed = 2;
					//ghost_m = 1;
				}
				TimeCount = 4;
			}
			//attack
			attackCount -= Time.deltaTime;
			Debug.Log(ghost_m);
			if (attackCount <= 0) {
				if (ghost_m == 0) {
					Instantiate (bullet, transform.position + new Vector3 (0.6f * ghost_m, 0f, 0f), transform.rotation);
				} else {
					Instantiate (bullet, transform.position + new Vector3 (0.6f * ghost_m, 0f, 0f), transform.rotation);
				}
				attackCount = 1;
			}
		}
	}

	//爆発処理2
	void OnTriggerEnter2D(Collider2D col)
	{
		if (_isRendered) {
			if (col.tag == "Bullet") {
				endurance--;
				if(endurance <= 0) {
				// AudioSource.PlayClipAtPoint (enemyDestroy, transform.position);
					Destroy (gameObject);
					Instantiate (explosion, transform.position, transform.rotation);
					if (Random.Range (0, 2) == 0) {
						Instantiate (item, transform.position, transform.rotation);
					}
				}
			}
		}
		if (col.tag == "AbyssZone") {
				Destroy (gameObject);
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
