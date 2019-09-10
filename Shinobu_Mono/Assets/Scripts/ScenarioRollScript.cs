using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class ScenarioRollScript : MonoBehaviour
{
    //　テキストのスクロールスピード
    [SerializeField]
    private float textScrollSpeed;
    //　テキストの制限位置
    [SerializeField]
    private float limitPosition = 730f;
    //　エンドロールが終了したかどうか
    private bool isStopEndRoll;
    //　シーン移動用コルーチン
    private Coroutine endRollCoroutine;
    public BlinkTextController btcScript;
    // Update is called once per frame
    void Update()
    {
        //　エンドロールが終了した時
        if (isStopEndRoll) {
            endRollCoroutine = StartCoroutine(GoToNextScene());
        } else {
            //　エンドロール用テキストがリミットを越えるまで動かす
            if (transform.position.y <= limitPosition) {
                transform.position = new Vector2(transform.position.x, transform.position.y + textScrollSpeed * Time.deltaTime);
            } else {
                Debug.Log("OK");
                isStopEndRoll = true;
                btcScript.isTextRender();
            }
        }
    }

    IEnumerator GoToNextScene() {
        if(Input.GetKeyDown("space")) {
            StopCoroutine(endRollCoroutine);
            SceneManager.LoadScene("MainStage");
        }
        yield return null;
    }
}