using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExitController : MonoBehaviour {
  public void OnExitButtonClicked() {
    Quit();
  }
  void Quit() {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
    #endif
  }
  void Update () {
    if (Input.GetKey(KeyCode.Escape)) Quit();
  }
}