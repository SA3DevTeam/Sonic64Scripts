using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
	public GameObject camera;
	public Vector3 downstairsPos;
	public Vector3 upstairsPos;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			camera.transform.position = upstairsPos;
		}
	}
	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Player") {
			camera.transform.position = downstairsPos;
		}
	}
}
