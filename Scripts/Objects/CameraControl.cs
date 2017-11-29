using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public GameObject player;
	public GameObject pivot;
	public GameObject cam;
	private float yrot;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		transform.position = player.transform.position;
		pivot.transform.Rotate (Vector3.up * Input.GetAxis ("Triggers") * 30f);

		transform.up = Vector3.Slerp (transform.up, player.transform.up, Time.deltaTime * 6f);

		Vector3 pos = player.GetComponent<SonicController>().rayPos + player.transform.forward * 1f;
		if (Physics.Raycast (pos, -player.transform.up, 0.3f)) {
			cam.transform.localEulerAngles = Vector3.Slerp (cam.transform.localEulerAngles, new Vector3 (6f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z), Time.deltaTime * 3f);
		} else {
			if (player.GetComponent<SonicController> ().grounded == true) {
				cam.transform.localEulerAngles = Vector3.Slerp (cam.transform.localEulerAngles, new Vector3 (75f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z), Time.deltaTime * 1f);
			} else {
				if (Vector3.Dot (-player.transform.up, player.GetComponent<Rigidbody> ().velocity) > 1f) {
					if (player.GetComponent<SonicController> ().homTarg == false) {
						cam.transform.localEulerAngles = Vector3.Slerp (cam.transform.localEulerAngles, new Vector3 (75f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z), Time.deltaTime * 1f);
					} else {
						cam.transform.localEulerAngles = Vector3.Slerp (cam.transform.localEulerAngles, new Vector3 (6f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z), Time.deltaTime * 3f);
					}
				}
			}
		}
	}
}

