using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBG : MonoBehaviour {
    //private GameObject[] SoundArray;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
      //  SoundArray = GameObject.FindGameObjectsWithTag("Sound");
       // if (SoundArray.Length > 1)
            //for (int i = 1; i < SoundArray.Length; i++)
             //   Destroy(SoundArray[i]);
    }

}
