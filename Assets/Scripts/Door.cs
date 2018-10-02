using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {


    int CurrentScene = 0;

	// Use this for initialization
	void Start () {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(CurrentScene+1);
    }


	// Update is called once per frame
	void Update () {
		
	}
}
