using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//追加
public class BackToTitle : MonoBehaviour {
	public void OnBackButtonClicked() {
		SceneManager.LoadScene("Title");
	}
}
