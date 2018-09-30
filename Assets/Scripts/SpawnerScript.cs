using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
	public GameObject SpawnObject;
    // Use this for initialization
    void Start()
    {
       // SpawnObject = SpawnObjects[Random.Range(0, SpawnObjects.Length)];
        Spawn();
    }

    void Spawn()
    {
        if (FlappyScript.gamestate==2)
        {
            //random y position
            float y = Random.Range(0.75f, 2.5f);
			Vector3 pos = this.transform.position;
			pos.y = y;
			Instantiate(SpawnObject,pos, Quaternion.identity);
        }
        Invoke("Spawn", Random.Range(timeMin, timeMax));
    }
    public float timeMin = 0.7f;
    public float timeMax = 2f;
}
