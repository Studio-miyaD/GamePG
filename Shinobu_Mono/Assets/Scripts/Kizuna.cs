using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kizuna : MonoBehaviour {

	RectTransform rt;
	//gameover
	public GameObject unityChan;

	void Start () {
		rt = GetComponent<RectTransform> ();
	}
	public void KizunaDown (float kp) {
		//RectTransformのサイズを取得し、マイナスする
		rt.sizeDelta -= new Vector2(0, kp);
	}

	//回復
	public void KizunaUp(int kp) {
		//RectTransformのサイズを取得し、プラス
		rt.sizeDelta += new Vector2(0, kp);
		//最大値を超えたら、最大値で上書きする
		if (rt.sizeDelta.y > 240f) {
			rt.sizeDelta = new Vector2 (51f, 240f);
		}
	}
}
