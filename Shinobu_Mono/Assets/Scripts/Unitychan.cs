using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Unitychan : MonoBehaviour 
	{
	public float speed = 4f; //歩くスピード
	//ジャンプ処理1開始
	public float jumpPower = 700; // ジャンプ力
	public LayerMask groundLayer; //Linecastで判定するLayer
	//ジャンプ処理1終了
	//Bullet1
	public GameObject bullet;
	//Bullet1 fin
	//gameover
	public Life lifeScript;

	private Rigidbody2D rigidbody2D;
	private BoxCollider2D boxCollider2D;
	private CircleCollider2D circleCollider2D;
	private Animator anim;
	//ジャンプ処理2開始
	//private bool isGrounded; //着地判定
	private bool isJump = false;
	public const int MAX_JUMP_COUNT = 2;
	private int jumpCount = 0;
	// ジャンプ処理2終了
	//無敵
	private Renderer renderer;
	//gameclear
	private bool gameClear = false; //ゲームクリアしたら操作不能にする

	// gameover
	private bool gameOver = false; // ゲームオーバーになったらタイトルに戻る

	private bool goal = false; // 建物に入ったらステージを遷移させる
	public Text clearText; //ゲームクリアー時に表示するテキスト
	// Start is called before the first frame update

	public bool isChange;
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
		//gameclear
		if (!gameClear) {
			// スペースキーを押し
			if (Input.GetKeyDown ("space")) {
				if (jumpCount < MAX_JUMP_COUNT) {
					isJump = true;
				}
			}
			if (Input.GetKeyDown ("c")) {
				isChange = !isChange;
				anim.SetBool ("Change", isChange);
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
			if (gameOver) {
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

				//左も右も入力していなかったら
			} else {
				// 横移動の速度を0にしてピタッと止まるようにする
				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				//dash→wait
				anim.SetBool ("Dash", false);
			}
		} else {
			//クリアーテキストを表示
			clearText.enabled = true;
			
			if (goal) {
				// プレイヤーの色を透明にする
				Color color = renderer.material.color;
				color.a = 0f;
				renderer.material.color = color;

				// 横移動の速度を0にしてピタッと止まるようにする
				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				//dash→wait
				anim.SetBool ("Dash", false);

			} else {
				anim.SetBool ("Dash", true);
				rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
				//5秒後にタイトル画面に戻るCallTitleメソッドを呼び出す

				Invoke ("CallTitle", 5);
			}
		}

		//jump
		if (isJump) {
			rigidbody2D.velocity = Vector2.zero;
			anim.SetBool ("Dash", false);
			anim.SetTrigger ("Jump");
			rigidbody2D.AddForce (Vector2.up * jumpPower);
			jumpCount++;
			isJump = false;
		}
	}
	//無敵
	void OnCollisonEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			StartCoroutine ("Damage");
		}
	}
	void OnCollisionEnter2D(Collision2D other) {
		string layerName = LayerMask.LayerToName(other.gameObject.layer);
		if (layerName == "Ground") {
			jumpCount = 0;
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
		//タグがAbyssZoneであるTriggerにぶつかったら
		if (col.tag == "AbyssZone") {
			//ゲームオーバー
			gameOver = true;
		}

		//タグがClearZoneであるTriggerにぶつかったら
		if (col.tag == "ClearZone") {
			//ゲームクリアー
			gameClear = true;
		}
		if (col.tag == "Goal") {
			goal = true;
		}
	}

	void CallTitle()
	{
		//タイトル画面へ
		SceneManager.LoadScene("Title");
	}
}