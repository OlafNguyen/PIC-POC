using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScripts : MonoBehaviour {

    	//public string LinkAndroid = "https://play.google.com/store/apps/details?id=com.nguyenvanquy.POCPOC";
     //   public string LinkIOS = "https://play.google.com/store/apps/details?id=com.nguyenvanquy.POCPOC";
        
    public GameObject Admob;
        GameObject g2;
    void Start () {
        g2 = GameObject.FindGameObjectWithTag("Admob");
        if (g2 == null)
            Admob.SetActive(true);
    }
  /*
        public void Share()
        {
            #if UNITY_ANDROID
            Application.OpenURL ("http://www.facebook.com/sharer.php?&p[url]="+LinkAndroid);
            #elif UNITY_IOS
            Application.OpenURL ("http://www.facebook.com/sharer.php?&p[url]="+LinkIOS);
            #else
            Application.OpenURL ("http://www.facebook.com/sharer.php?&p[url]="+LinkAndroid);
            #endif
        }

        public void RateApp()
        {
            #if UNITY_ANDROID
            Application.OpenURL (LinkAndroid);
            #elif UNITY_IOS
            Application.OpenURL (LinkIOS);
            #else
            Application.OpenURL (LinkAndroid);
            #endif
        }*/
    public void Replay()
    {
        FlappyScript.score = 0;
        FlappyScript.gamestate = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
