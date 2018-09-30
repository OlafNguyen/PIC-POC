using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		Vector3 temScale = transform.localScale;

		float height = sr.bounds.size.y;
		float width = sr.bounds.size.x;

		float worldHeight = Camera.main.orthographicSize * 2f;
		float worldWidth = worldHeight * Screen.width / Screen.height;

		temScale.y = worldHeight / height;
		temScale.x = worldWidth / width;

		transform.localScale = temScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
