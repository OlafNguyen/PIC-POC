using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveColumn : MonoBehaviour {
    public float XSpeed = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(Time.deltaTime * XSpeed, 0, 0);
    }
}
