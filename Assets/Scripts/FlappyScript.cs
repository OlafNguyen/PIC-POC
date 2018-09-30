using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlappyScript : MonoBehaviour
{
	public static FlappyScript instance;
	public float bounceForce;
	private Rigidbody2D  myBody;
	public Rigidbody2D body2;  //bird2
	private bool isAlive;
	private bool didFlap;
	private bool didFlap2;
	public GameObject Score;
	public GameObject ScoreDie;
	public GameObject BestDie;


   // public AudioClip  DeathAudioClip, ScoredAudioClip;
    public Sprite GetReadySprite;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    public  GameObject IntroGUI, DeathGUI;
    // public Collider2D restartButtonGameCollider;
    //public Collider2D RateButtonGameCollider;

    // public float VelocityPerJump = 3;
    // public float XSpeed = 1;
    public static int score = 0;
    public static int highscore = 0;
    public static int ad = 0;
    public static int gamestate = 1;
    int previousScore = -1;
    public Sprite[] numberSprites;
    public SpriteRenderer Units, Tens, Hundreds;
    

    void Awake () {
		isAlive = true;
		highscore=PlayerPrefs.GetInt("highscore");
        ad= PlayerPrefs.GetInt("ad");
        //ScoreManagerScript.ad=PlayerPrefs.GetInt("ad");
        myBody = GetComponent<Rigidbody2D> ();
	//	anim = GetComponent<Animator> ();
		_MakeInstance ();
	}
  
	void _MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

    // Use this for initialization
    void Start()
    {
        //Input.multiTouchEnabled = true;
            Input.multiTouchEnabled=true;
            (Tens.gameObject as GameObject).SetActive(false);
            (Hundreds.gameObject as GameObject).SetActive(false);
        
    }

    FlappyYAxisTravelState flappyYAxisTravelState;
	FlappyYAxisTravelState2 flappyYAxisTravelState2;


    enum FlappyYAxisTravelState
    {
        GoingUp, GoingDown
    }
	enum FlappyYAxisTravelState2
	{
		GoingUp2, GoingDown2
	}


    Vector3 birdRotation = Vector3.zero;
	Vector3 birdRotation2 = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        //handle back key in Windows Phone
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetMouseButtonDown(0))
        {
           

            if (gamestate == 1)
            {

                //MoveBirdOnXAxis();

                //BoostOnYAxis();

                didFlap = true;
                didFlap2 = true;
                gamestate = 2;
                IntroGUI.SetActive(false);
                score = 0;

            }
           /* else
            {
                
                    score = 0;
                    gamestate = 1;
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                
            }*/
        }
		/*
        else if (GameStateManager.GameState == GameState.Dead)
        {
            //Vector2 contactPoint = Vector2.zero;

            if (Input.touchCount > 0)
                contactPoint = Input.touches[0].position;
            if (Input.GetMouseButtonDown(0))
                contactPoint = Input.mousePosition;

            //check if user wants to restart the game
            if (restartButtonGameCollider == Physics2D.OverlapPoint
                (Camera.main.ScreenToWorldPoint(contactPoint)))
            {
				ScoreManagerScript.Score = 0;
                GameStateManager.GameState = GameState.Intro;
				UnityEngine.SceneManagement.SceneManager.LoadScene (0);
				//Application.LoadLevel(Application.loadedLevelName);
            }
			if (RateButtonGameCollider == Physics2D.OverlapPoint
				(Camera.main.ScreenToWorldPoint(contactPoint)))
			{
				RateApp ();
			}
        }*/

    }
    void FixScore()
        {

        if (previousScore != score) //save perf from non needed calculations
        {
            if (score < 10)
            {
                //just draw units
                Units.sprite = numberSprites[score];
            }
            else if (score >= 10 && score < 100)
            {
                (Tens.gameObject as GameObject).SetActive(true);
                Tens.sprite = numberSprites[score / 10];
                Units.sprite = numberSprites[score % 10];
            }
            else if (score >= 100)
            {
                (Hundreds.gameObject as GameObject).SetActive(true);
                Hundreds.sprite = numberSprites[score / 100];
                int rest = score % 100;
                Tens.sprite = numberSprites[rest / 10];
                Units.sprite = numberSprites[rest % 10];
            }
        }
    }

    void FixedUpdate()
    {
        //just jump up and down on intro screen
      
      //  else if (GameStateManager.GameState == GameState.Playing || GameStateManager.GameState == GameState.Dead)
		if (gamestate==2 )
		{
            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {//MoveBirdOnXAxis();
             // if (    (Input.GetMouseButtonDown(0) && (Input.mousePosition.x < Screen.width / 2)) ||
             // (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && Input.GetTouch(0).position.x < Screen.width / 2))

                if (Input.mousePosition.x <= Screen.width / 2)
                {
                    didFlap2 = true;
                    //BoostOnYAxis();
                }
                else
                {
                    didFlap = true;
                    //BoostOnYAxis();
                }

            }
            #elif (UNITY_ANDROID || UNITY_IOS)

            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                if (touch.position.x < Screen.width / 2)
                {
                    didFlap2 = true;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    didFlap = true;
                }
            }
            #endif
            //MoveBirdOnXAxis ();
            _BirdMoveMent();
			_BirdMoveMent2 ();
        }
        else if (gamestate == 1)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < -1) //when the speed drops, give a boost
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, GetComponent<Rigidbody2D>().mass * 5500 * Time.deltaTime)); //lots of play and stop 
                                                                                                                                //and play and stop etc to find this value, feel free to modify
            if (body2.velocity.y < -1) //when the speed drops, give a boost
                body2.AddForce(new Vector2(0, body2.mass * 5500 * Time.deltaTime)); //lots of play and stop 
                                                                                    //and play and stop etc to find this value, feel free to modify
        }
        else
		{
			FixFlappyRotation();
			FixFlappyRotation2();
		}
    }

	//bool WasTouchedOrClickedLeft()
    //{
		//if ((Input.GetMouseButtonDown (0)&&(Input.mousePosition.x<Screen.width/2) )||
		//	(Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Ended && Input.GetTouch(0).position.x<Screen.width/2))
          //  return true;
       // else
           // return false;
    //}
	//bool WasTouchedOrClickedRight()
//	//{
		//if ((Input.GetMouseButtonDown (0)&&(Input.mousePosition.x>Screen.width/2) )||
		//	(Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Ended && Input.GetTouch(0).position.x>Screen.width/2))
		//	return true;
		//else
			//return false;
	//}

    /*void MoveBirdOnXAxis()
    {
        transform.position += new Vector3(Time.deltaTime * XSpeed, 0, 0);
		body2.gameObject.transform.position+= new Vector3(Time.deltaTime * XSpeed, 0, 0);
    }
    */
   // void BoostOnYAxis()
    //{
      //  GetComponent<Rigidbody2D>().velocity = new Vector2(0, VelocityPerJump);
      //  GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
    //}

	void _BirdMoveMent(){

		if (isAlive) {
			if (didFlap) {
				didFlap = false;
				myBody.velocity = new Vector2 (myBody.velocity.x, bounceForce);
				//GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
			}
		}
	/*	if (myBody.velocity.y > 0) {
			float angel = 0;
			angel = Mathf.Lerp (0, 90, myBody.velocity.y / 7);
			transform.rotation = Quaternion.Euler (0, 0, angel);
		}else if (myBody.velocity.y == 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}else if (myBody.velocity.y < 0) {
			float angel = 0;
			angel = Mathf.Lerp (0, -90, -myBody.velocity.y / 7);
			transform.rotation = Quaternion.Euler (0, 0, angel);
		}*/
	}
	void _BirdMoveMent2(){

		if (isAlive) {
			if (didFlap2) {
				didFlap2 = false;
				body2.velocity = new Vector2 (body2.velocity.x, bounceForce);
			//	GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
			}
		}
		/*if (body2.velocity.y > 0) {
			float angel = 0;
			angel = Mathf.Lerp (0, 90, body2.velocity.y / 7);
			body2.transform.rotation = Quaternion.Euler (0, 0, angel);
		}else if (body2.velocity.y == 0) {
			body2.transform.rotation = Quaternion.Euler (0, 0, 0);
		}else if (body2.velocity.y < 0) {
			float angel = 0;
			angel = Mathf.Lerp (0, -90, -body2.velocity.y / 7);
			body2.transform.rotation = Quaternion.Euler (0, 0, angel);
		}*/
	}


    /// <summary>
    /// when the flappy goes up, it'll rotate up to 45 degrees. when it falls, rotation will be -90 degrees min
    /// </summary>
    private void FixFlappyRotation()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > 0) 
			flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;

        float degreesToAdd = 0;

        switch (flappyYAxisTravelState)
        {
            case FlappyYAxisTravelState.GoingUp:
                degreesToAdd = 6 * RotateUpSpeed;
                break;
            case FlappyYAxisTravelState.GoingDown:
                degreesToAdd = -3 * RotateDownSpeed;
                break;
            default:
                break;
        }
        //solution with negative eulerAngles found here: http://answers.unity3d.com/questions/445191/negative-eular-angles.html

        //clamp the values so that -90<rotation<45 *always*
        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -90, 45));
        transform.eulerAngles = birdRotation;
    }
	private void FixFlappyRotation2()
	{
		if (body2.velocity.y > 0) 
			flappyYAxisTravelState2 = FlappyYAxisTravelState2.GoingUp2;
		else flappyYAxisTravelState2 = FlappyYAxisTravelState2.GoingDown2;

		float degreesToAdd = 0;

		switch (flappyYAxisTravelState2)
		{
		case FlappyYAxisTravelState2.GoingUp2:
			degreesToAdd = 6 * RotateUpSpeed;
			break;
		case FlappyYAxisTravelState2.GoingDown2:
			degreesToAdd = -3 * RotateDownSpeed;
			break;
		default:
			break;
		}
		//solution with negative eulerAngles found here: http://answers.unity3d.com/questions/445191/negative-eular-angles.html

		//clamp the values so that -90<rotation<45 *always*
		birdRotation2 = new Vector3(0, 0, Mathf.Clamp(birdRotation2.z + degreesToAdd, -90, 45));
		body2.transform.eulerAngles = birdRotation2;
	}

    /// <summary>
    /// check for collision with pipes
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gamestate==2)
        {
            if (col.gameObject.tag == "Pipeblank") //pipeblank is an empty gameobject with a collider between the two pipes
            {
               // GetComponent<AudioSource>().PlayOneShot(ScoredAudioClip);
                score++;
                FixScore();

                if (score>highscore)
				{
				highscore = score;
				}
            }
            else if (col.gameObject.tag == "Pipe")
            {
                //col.gameObject.SetActive (false);
                gamestate = 3;
				//GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
				Invoke("FlappyDies",1f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gamestate==2)
        {
            if (col.gameObject.tag == "Floor")
            {
                gamestate = 3;
			//	GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
				Invoke("FlappyDies",1f);
            }
        }
    }

    public void FlappyDies()
    {
		PlayerPrefs.SetInt ("highscore", highscore);
		PlayerPrefs.Save ();
		ad++;
		PlayerPrefs.SetInt ("ad", ad);
		PlayerPrefs.Save ();
        Score.SetActive(false);
        ScoreDie.GetComponent<Text>().text = "Score " + score;
        BestDie.GetComponent<Text>().text = "Best " + highscore;
        DeathGUI.SetActive(true);
		if (ad % 5 == 0) {
			AdManager.Instance.ShowInterstitial ();
		}
    }
    public void SetOb()
    {
       
    }

}
