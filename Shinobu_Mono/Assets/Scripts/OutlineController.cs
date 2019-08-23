using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//追加
public class OutlineController : MonoBehaviour {
  public void OnSkipButtonClicked() {
		SceneManager.LoadScene("Main");
  }
}
