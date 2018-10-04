using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public float degreesPerSecond;

    int currentScene = 0;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

	// Update is called once per frame
	void Update () {
        Rotate(Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D col) {
        SceneManager.LoadScene(currentScene + 1);
    }

    // Rotates the door sprite
    void Rotate(float deltaTime) {
        transform.rotation *= Quaternion.AngleAxis(degreesPerSecond * deltaTime, Vector3.forward);
    }
}
