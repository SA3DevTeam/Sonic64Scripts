using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {
	public GameObject player;
	public bool homingBridge;
	public float respawnTime;

	IEnumerator Respawn () {
		yield return new WaitForSeconds (respawnTime);
		GetComponent<Collider> ().enabled = true;
		GetComponent<MeshRenderer> ().enabled = true;
		Target script = gameObject.AddComponent<Target> ();
	}

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter (Collider col) {
		if (player.GetComponent<SonicController> ().jump == true) {
			if (homingBridge == false) {
				Destroy (gameObject);
			} else {
				GetComponent<Collider> ().enabled = false;
				GetComponent<MeshRenderer> ().enabled = false;
				Destroy (gameObject.GetComponent<Target> ());
				StartCoroutine ("Respawn");
			}
		}
	}

}
