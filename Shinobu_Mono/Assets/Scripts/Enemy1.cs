using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
  Rigidbody2D rigidbody2D;
  public int speed = -3;
	[Header("耐久力")] public int endurance = 8;
  public bool isDamageCut = false;
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
	//効果音
	public AudioClip enemyDestroy;

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
    }

//爆発処理2
    void OnTriggerEnter2D(Collider2D col)
    {
		if (_isRendered) {
			if (col.tag == "Bullet" || col.tag == "Shuriken" || col.tag == "Kunai") {
				if (col.tag == "Bullet") {
          endurance -= 4;
        } else if (col.tag == "Shuriken") {
          endurance --;
        } else {
          endurance -= 2;
        }
        if(endurance <= 0) {
					AudioSource.PlayClipAtPoint (enemyDestroy, transform.position);
					Destroy (gameObject);
					Instantiate (explosion, transform.position, transform.rotation);
					if (Random.Range (0, 2) == 0) {
						Instantiate (item, transform.position, transform.rotation);
					}
				}
			}
			if (col.tag == "AbyssZone") {
				Destroy (gameObject);
			}
    }
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    //unitychanとぶつかった時
    if (col.gameObject.tag == "UnityChan")
    {
      //LifeScriptのLifeDownメソッドを実行
      lifeScript.LifeDown(attackPoint);
    }
  }

  void OnWillRenderObject()
  {
    //メインカメラに映った時だけ_isRenderedをtrue
    if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
    {
      _isRendered = true;
    }
  }
}