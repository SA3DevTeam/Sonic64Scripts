﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpriteAnimation : MonoBehaviour {
	public Texture[] textures;
	public float changeInterval = 0.33F;
	public Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		
		if (textures.Length == 0)
			return;

		int index = Mathf.FloorToInt(Time.time / changeInterval);
		index = index % textures.Length;
		rend.material.SetTexture ("_MainTex", textures[index]);
	}

}