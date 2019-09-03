using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    private GameObject player;
    private Kizuna kizunaScript;
    private int speed = 10;

    void Start()
    {
        //ユニティちゃんオブジェクトを取得
      player = GameObject.FindWithTag("UnityChan");
      kizunaScript = GameObject.FindWithTag ("KP").GetComponent<Kizuna> ();
        //rigidbody2Dコンポーネントを取得
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        //ユニティちゃんの向いている向きに弾を飛ばす
        rigidbody2D.velocity = new Vector2(speed * player.transform.localScale.x, rigidbody2D.velocity.y);
        //画面の向きをユニティちゃんに合わせる
        Vector2 temp = transform.localScale;
        temp.x = player.transform.localScale.x;
        transform.localScale = temp;
        //5 秒後に消滅
        Destroy(gameObject, 5);
        
    }

    //追加
    void OnTriggerEnter2D(Collider2D col)
    {
      if (col.gameObject.tag == "Enemy") {
        kizunaScript.KizunaDown(15f);
          Destroy(gameObject);
      }

      string layerName = LayerMask.LayerToName(col.gameObject.layer);
      if (layerName == "Ground") {
        Destroy(gameObject, 0.01f);
      }
    }
}