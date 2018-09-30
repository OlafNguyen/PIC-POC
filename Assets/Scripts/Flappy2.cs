using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flappy2 : MonoBehaviour {
	public GameObject Score;
	public GameObject ScoreDie;
	public GameObject BestDie;
	public GameObject  DeathGUI;
    //public AudioClip DeathAudioClip, ScoredAudioClip;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (FlappyScript.gamestate==2)
		{
			if (col.gameObject.tag == "Pipeblank") //pipeblank is an empty gameobject with a collider between the two pipes
			{
				//GetComponent<AudioSource>().PlayOneShot(ScoredAudioClip);
                FlappyScript.score++;
				if (FlappyScript.score > FlappyScript.highscore)
				{
                    FlappyScript.highscore = FlappyScript.score;
				}
			}
			else if (col.gameObject.tag == "Pipe")
			{
                //col.gameObject.SetActive (false);
                FlappyScript.gamestate = 3;
				//GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
				Invoke("FlappyDies",1f);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (FlappyScript.gamestate==2)
		{
			if (col.gameObject.tag == "Floor")
			{
                FlappyScript.gamestate = 3;
              //  GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
				Invoke("FlappyDies",1f);
			}
		}
	}

	void FlappyDies()
	{
        PlayerPrefs.SetInt("highscore", FlappyScript.highscore);
        PlayerPrefs.Save();
        FlappyScript.ad++;
        PlayerPrefs.SetInt("ad", FlappyScript.ad);
        PlayerPrefs.Save();
        Score.SetActive(false);
        ScoreDie.GetComponent<Text>().text = "Score " + FlappyScript.score;
        BestDie.GetComponent<Text>().text = "Best " + FlappyScript.highscore;
        DeathGUI.SetActive(true);
        if (FlappyScript.ad % 5 == 0)
        {
            AdManager.Instance.ShowInterstitial();
        }
    }
}
