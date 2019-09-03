using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//追加
public class ManualController : MonoBehaviour {	
  public void OnPreviousButtonClicked() {
		SceneManager.LoadScene ("Manual");
	}

    public void OnNextButtonClicked() {
		SceneManager.LoadScene("Manual2");
    }
}


