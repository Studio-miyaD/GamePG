using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//追加
public class GameStart : MonoBehaviour {
	public void OnStartButtonClicked() {
		SceneManager.LoadScene ("Main");
	}
}
