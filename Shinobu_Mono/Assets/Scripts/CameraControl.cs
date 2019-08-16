using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private GameObject player = null;
	private Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("UnityChan");
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
  {
		if (player == null) { return; } 
    Vector3 newPosition = transform.position;
    newPosition.x = player.transform.position.x + offset.x;
    newPosition.y = player.transform.position.y + offset.y;
    newPosition.z = player.transform.position.z + offset.z;
		transform.position = newPosition;
  }
}
