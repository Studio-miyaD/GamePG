using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//追加
public class TitleController : MonoBehaviour {	
  public void OnStartButtonClicked() {
		SceneManager.LoadScene ("Outline");
	}

    public void OnManualButtonClicked() {
		SceneManager.LoadScene("Manual");
    }
}
