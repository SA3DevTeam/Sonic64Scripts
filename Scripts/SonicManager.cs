using UnityEngine;
using System.Collections;
using TMPro;

public class SonicManager : MonoBehaviour {

	public float rings;
	public TextMeshPro timer;
	private float startTime;
	public bool inLevel;

	void Start () {
		if (inLevel == true)
			startTime = Time.time;
	}

	void Update () {
		if (inLevel == true) {
			float t = Time.time - startTime;
			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f2");

			timer.text = minutes + ":" + seconds;
		}
	}
	
}

