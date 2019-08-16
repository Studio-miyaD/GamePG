using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unitychan : MonoBehaviour 
	{

	public float speed = 4f; //歩くスピード
	//ジャンプ処理1開始
	public float jumpPower = 700; // ジャンプ力
	public LayerMask groundLayer; //Linecastで判定するLayer
	//ジャンプ処理1終了
	//カメラ処理1
	//public GameObject mainCamera;
	// カメラ処理1終了
	//Bullet1
	public GameObject bullet;
	//Bullet1 fin
	//gameover
	public Life lifeScript;

	private Rigidbody2D rigidbody2D;
	private Animator anim;
	//ジャンプ処理2開始
	private bool isGrounded; //着地判定
	// ジャンプ処理2終了
	//無敵
	private Renderer renderer;
	//gameclear
	private bool gameClear = false; //ゲームクリアしたら操作不能にする
	public Text clearText; //ゲームクリアー時に表示するテキスト
	// Start is called before the first frame update
	void Start()
	{
	    anim = GetComponent<Animator>();
	    rigidbody2D = GetComponent<Rigidbody2D>();
		//無敵
		renderer = GetComponent<Renderer> ();
	        
	}
	//ジャンプ処理3開始
	void Update ()
	{
	    //Listcastでユニティちゃんの足元に地面があるか判定
	    //isGrounded = Physics2D.Linecast(transform.position + transform.up * 1, transform.position -
	    //transform.up * 0.05f, groundLayer);
		isGrounded = true;
		//gameclear
		if (!gameClear) {
			// スペースキーを押し
			if (Input.GetKeyDown ("space")) {
				//着地していた時
				if (isGrounded) {
					//Dashアニメーションを止めて、Jumpアニメーションを実行
					anim.SetBool ("Dash", false);
					anim.SetTrigger ("Jump");
					// 着地判定をfalse
					isGrounded = false;
					//AddForceにて上方向へ力を加える
					rigidbody2D.AddForce (Vector2.up * jumpPower);
				}
			}
		}
	    //上下への移動速度を取得
	    float velY = rigidbody2D.velocity.y;
	    // 移動速度がより0.1大きければ上昇
	    bool isJumping = velY > 0.1f ? true:false;
	    //移動速度がより0.1小さければ降下
	    bool isFalling = velY < -0.1f ? true:false;
	    // 結果をアニメータービューの変数は反映する
	    anim.SetBool("isJumping", isJumping);
	    anim.SetBool("isFalling", isFalling);

		//gameclear
		if (!gameClear) {
			//Bullet2 begin
			if (Input.GetKeyDown ("left ctrl")) {
				anim.SetTrigger ("Shot");
				Instantiate (bullet, transform.position + new Vector3 (0f, 1.2f, 0f), transform.rotation);
			}
			//gameover
			if (gameObject.transform.position.y < Camera.main.transform.position.y - 8) {
				//LifeScriptのGameOverメソッドを実行
				lifeScript.GameOver ();
			}
		}
	}
	//ジャンプ処理3終了

	void FixedUpdate()
	{
		//gameclear
		if (!gameClear) {
			// 右キー: 1、左キー: -1
			float x = Input.GetAxisRaw ("Horizontal");
			//左か右を入力したら
			if (x != 0) {
				// 入力方向へ移動
				rigidbody2D.velocity = new Vector2 (x * speed, rigidbody2D.velocity.y);
				//localScale.xを-1すると画像が反転する
				Vector2 temp = transform.localScale;
				temp.x = x;
				transform.localScale = temp;
				//wait→dash
				anim.SetBool ("Dash", true);

				//カメラ処理2
				//画面中央から左に4移動した位置をユニティちゃんが超えたら
				/*
				if (transform.position.x > mainCamera.transform.position.x - 4) {
					// カメラの位置を取得
					Vector3 cameraPos = mainCamera.transform.position;
					//ユニティちゃんの位置から右に4移動した位置を画面中央にする
					cameraPos.x = transform.position.x + 4;
					mainCamera.transform.position = cameraPos;
				}

				// カメラ表示領域の左下をワールド座標に変換
				Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
				//カメラ表示領域の右上をワールド座標に変換
				Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
				//ユニティちゃんのポジションを取得
				Vector2 pos = transform.position;
				// ユニティちゃんのx座標の移動範囲をClampメソッドで制限
				pos.x = Mathf.Clamp (pos.x, min.x + 0.5f, max.x);
				transform.position = pos;
				//カメラ処理2終了
				*/

				//左も右も入力していなかったら
			} else {
				// 横移動の速度をにしてピタッと止まるようにする
				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				//dash→wait
				anim.SetBool ("Dash", false);
			}
		} else {
			//クリアーテキストを表示
			clearText.enabled = true;
			anim.SetBool ("Dash", true);
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
			//5秒後にタイトル画面に戻るCallTitleメソッドを呼び出す

			Invoke ("CallTitle", 5);
		}
	}
	//無敵
	void OncollisonEnter2D(Collision2D col)
	{
		 if (col.gameObject.tag == "Enemy") {
			 StartCoroutine ("Damage");
		 }
	}

	IEnumerator Damage()
	{
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		int count = 10;
		while (count > 0) {
			//透明にする
			renderer.material.color = new Color(1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			renderer.material.color = new Color(1,1,1,1);
			yield return new WaitForSeconds (0.05f);
			count--;
		}
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Player");
	}

	//gameclear
	void OnTriggerEnter2D(Collider2D col) 
	{
		//タグがClearZoneであるTriggerにぶつかったら
		if (col.tag == "ClearZone") {
			//ゲームクリアー
			gameClear = true;
		}
	}

	void CallTitle()
	{
		//タイトル画面へ
		Application.LoadLevel("Title");
	}
}
