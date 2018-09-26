using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour {
    
    //Some Parameters
    
    public float speed;
    public float jumpHeight;

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
            vel.y = jumpHeight;
        }

        rigidBody.velocity = vel;
    }

    // Determines if the player can jump
    void UpdateGrounding() {
        // groundCheck is below the character if jumpHeight is positive, otherwise it is above the character
        Vector3 groundCheck = transform.position - new Vector3(0, 0.4f * (jumpHeight < 0 ? -1 : 1), 0);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck, groundingMask);
        if (hit.collider != null) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    // Called when the player runs into something
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Death")) {
            StartCoroutine(Kill());
        }
    }

    // Kills the player
    IEnumerator Kill() {
        yield return new WaitForSeconds(1);

        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
