using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour {
	public float speed;

	void Update () {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed); //To set the speed, just multiply the Time.time with whatever amount you want.
		
	}
}
