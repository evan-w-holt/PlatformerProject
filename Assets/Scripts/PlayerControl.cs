using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour {
    
    //Some Parameters
    
    public float speed;
    public float jumpheight;

    private Rigidbody2D rigidBody;

    private bool isGrounded = false;
    private int groundingMask = int.MaxValue - (1 << 8);

	// Use this for initialization
	void Start() {
        rigidBody = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        UpdateGrounding();
        InterpretInput();
	}

    // Handles player input
    void InterpretInput() {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 vel = rigidBody.velocity;

        vel.x = horizontal;

        if (Input.GetAxis("Jump") > 0 && isGrounded) {
            vel.y = jumpheight;
        }

        rigidBody.velocity = vel;
    }

    // Determines if the player can jump
    void UpdateGrounding() {
        Vector3 groundCheck = transform.position - new Vector3(0, 0.4f, 0);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck, groundingMask);
        if (hit.collider != null) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }
}
