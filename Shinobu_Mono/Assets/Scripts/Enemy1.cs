using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public int speed = -3;
	//耐久力
	[SerializeField]
	public int endurance;
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

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<Life> ();
    }

    void Update()
    {
		if (_isRendered) { // カメラに写っている
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		}
		// 敵を消す範囲
		if (gameObject.transform.position.y < Camera.main.transform.position.y - 15
		    || gameObject.transform.position.x < Camera.main.transform.position.x - 30) {
			Destroy (gameObject);
		}
    }

//爆発処理2
    void OnTriggerEnter2D(Collider2D col)
    {
		if (_isRendered) {
			if (col.tag == "Bullet") {
				endurance--;
				if(endurance <= 0) {
					Destroy (gameObject);
					Instantiate (explosion, transform.position, transform.rotation);
					if (Random.Range (0, 2) == 0) {
						Instantiate (item, transform.position, transform.rotation);
					}
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
