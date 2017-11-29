using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class QueueAudioClip: MonoBehaviour {
	public AudioSource audioSourceIntro;
	public AudioSource audioSourceLoop;
	private bool startedLoop;

	IEnumerator Wait () {
		yield return new WaitForSeconds (0.01f);
		if (!audioSourceIntro.isPlaying && !startedLoop) {
			audioSourceLoop.Play();
			//Debug.Log("Done playing");
			startedLoop = true;
		}
	}

	void FixedUpdate() {
		StartCoroutine ("Wait");
	}
}