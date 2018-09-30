using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layers : MonoBehaviour {
	//public GameObject g1;
	public GameObject g2;
	private SpriteRenderer r1;
	private SpriteRenderer r2;
	// Use this for initialization
	void Start () {
		r1 = GetComponent<SpriteRenderer> ();
		r2 = g2.GetComponent<SpriteRenderer> ();
	}
	
	// Upd1ate is called once per frame
	void Update () {
		if (r1.enabled == true) {
			Invoke ("v1", 0.2f);
		}
		else {
			Invoke ("v2", 0.2f);
		}
	}
	public void v1()
	{
		r1.enabled = false;
		r2.enabled = true;
	}
	public void v2()
	{
		r2.enabled = false;
			r1.enabled = true;
	}
}
