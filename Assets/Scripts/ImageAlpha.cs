using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlpha : MonoBehaviour {
	public float duration=0.2f;
	private float timeAlpha;
	// Use this for initialization
	void Start () {
		timeAlpha = 0f;
	}

	// Update is called once per frame
	void Update () {
		timeAlpha += Time.deltaTime;
		if (timeAlpha > duration) {
			GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
			timeAlpha = 0;
		}
	}
}
