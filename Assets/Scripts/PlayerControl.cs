using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour {
    
    //Some Parameters

    //speed
    public static float speed;

    //jumpheight
    public static float jumpheight;

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
