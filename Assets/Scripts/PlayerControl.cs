using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour {
    
    public float speed;
    public float jumpHeight;

    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;

    private bool isGrounded = false;

    // The layer mask for grounding raycasts
    private int groundingMask = int.MaxValue - (1 << 8);

    // When cancelDirection is not zero, we cancel horizontal movement in the direction of cancelDirection
    private static float cancelDirection;

	// Use this for initialization
	void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        if (CharacterAnimator.instance.characterStatus != CharacterAnimator.AnimStatus.Dead) {
            UpdateGrounding();
            InterpretInput();
        }
	}

    // Handles player input
    void InterpretInput() {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector2 vel = rigidBody.velocity;
        vel.x = horizontal;

        if (horizontal < 0) {
            sprite.flipX = true;
        } else if (horizontal > 0) {
            sprite.flipX = false;
        }

        if (horizontal != 0 && isGrounded) {
            CharacterAnimator.instance.Running(sprite);
        } else if (isGrounded) {
            CharacterAnimator.instance.Idle(sprite);
        } else {
            CharacterAnimator.instance.Jumping(sprite);
        }

        if (Input.GetAxis("Jump") > 0 && isGrounded) {
            vel.y = jumpHeight;
            SoundSystem.instance.PlayJumpSound();
        }

        rigidBody.velocity = vel;
    }

    // Determines if the player can jump
    void UpdateGrounding() {
        // groundCheck is below the character if jumpHeight is positive, otherwise it is above the character
        Vector3 groundCheck = transform.position - new Vector3(0, 0.55f * (jumpHeight < 0 ? -1 : 1), 0);

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
            Kill();
        }
    }

    // Kills the player
    void Kill() {
        SoundSystem.instance.PlayDeathSound();
        StartCoroutine(CharacterAnimator.instance.Death());
    }
}
