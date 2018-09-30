using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaSprite : MonoBehaviour {

	public float duration=3f;
	private float timeAlpha;
	// Use this for initialization
	void Start () {
		timeAlpha = 0f;
	}

	// Update is called once per frame
	void Update () {
		timeAlpha += Time.deltaTime;
		if (timeAlpha > duration) {
			GetComponent<SpriteRenderer> ().enabled = !GetComponent<SpriteRenderer> ().enabled;
			timeAlpha = 0;
		}
	}
}
