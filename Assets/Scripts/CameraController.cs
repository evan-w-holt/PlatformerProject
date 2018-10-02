using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // The two player objects
    public GameObject player1;
    public GameObject player2;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (player1 && player2) {
            if (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) <= 4.25) {
                MoveCamera();
            }
        }
	}

    // Moves the camera to track the players
    void MoveCamera() {
        Vector3 pos = transform.position;

        // Get the average x position of the two players
        float avgX = (player1.transform.position.x + player2.transform.position.x) / 2f;
        pos.x = avgX;

        // Set the camera position
        transform.position = pos;
    }
}
