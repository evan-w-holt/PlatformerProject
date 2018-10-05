using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAnimator : MonoBehaviour {

    // The character and shadow sprite renderers
    public SpriteRenderer character;
    public SpriteRenderer shadow;

    // The single instance of this object
    public static CharacterAnimator instance;

    // Variables for the idle animation frames
    public Sprite[] idleAnimationFrames;
    public float idleTimeBetweenEffect;
    public float idleAnimationTime;
    private bool isIdling = true;
    private int idleFrameNum = 0;

    // Variables for the running animation frames
    public Sprite[] runningAnimationFrames;
    public float runningAnimationTime;
    private int runningFrameNum = 0;

    // The jumping sprite
    public Sprite jumpingSprite;

    // The death sprite
    public Sprite deathSprite;

    // Whether or not the player just died
    public static bool justDied = false;

    // The status of the character and shadow (the current animation being run) and the corresponding enum
    [HideInInspector]
    public enum AnimStatus {
        Idle,
        Running,
        Jumping,
        Dead
    }
    [HideInInspector] public AnimStatus characterStatus = AnimStatus.Idle;
    [HideInInspector] public AnimStatus shadowStatus = AnimStatus.Idle;

    // Keeping track of time separately for the character and the sprite
    private float characterTime = 0;
    private float shadowTime = 0;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        character = GameObject.FindGameObjectWithTag("Character").GetComponent<SpriteRenderer>();
        shadow = GameObject.FindGameObjectWithTag("Shadow").GetComponent<SpriteRenderer>();

        if (justDied) {
            StartCoroutine(Blink());
        }
    }

    // Update is called once per frame
    void Update () {
        if (characterStatus != AnimStatus.Dead && shadowStatus != AnimStatus.Dead) {
            Animate(character, characterStatus, ref characterTime);
            Animate(shadow, shadowStatus, ref shadowTime);
        }

        characterTime += Time.deltaTime;
        shadowTime += Time.deltaTime;
	}

    // Animates the given sprite sprite
    void Animate(SpriteRenderer sprite, AnimStatus status, ref float time) {
        if (status == AnimStatus.Running) {
            idleFrameNum = 0;

            if (time >= runningAnimationTime) {
                runningFrameNum++;
                time = 0;

                if (runningFrameNum > runningAnimationFrames.Length - 1) {
                    runningFrameNum = 0;
                }
            }

            sprite.sprite = runningAnimationFrames[runningFrameNum];
        } else if (status == AnimStatus.Jumping) {
            runningFrameNum = 0;
            idleFrameNum = 0;

            sprite.sprite = jumpingSprite;
        } else {
            // Idle
            runningFrameNum = 0;

            if (isIdling && time >= idleTimeBetweenEffect) {
                isIdling = false;
                idleFrameNum++;
                time = 0;
            } else if (!isIdling && time >= idleAnimationTime) {
                idleFrameNum++;
                time = 0;

                if (idleFrameNum > idleAnimationFrames.Length - 1) {
                    idleFrameNum = 0;
                    isIdling = true;
                }
            }

            sprite.sprite = idleAnimationFrames[idleFrameNum];
        }
    }

    // Runs the idle animation on the given sprite
    public void Idle(SpriteRenderer sprite) {
        if (sprite == character) {
            characterStatus = AnimStatus.Idle;
        } else if (sprite == shadow) {
            shadowStatus = AnimStatus.Idle;
        }
    }

    // Flashes the character and shadow if you just died
    IEnumerator Blink() {
        justDied = false;

        for (int i = 0; i < 15; i++) {
            character.color = new Color32(255, 255, 255, 0);
            shadow.color = new Color32(0, 0, 0, 0);
            yield return null;
            yield return null;
            character.color = new Color32(255, 255, 255, 255);
            shadow.color = new Color32(0, 0, 0, 255);
            yield return null;
            yield return null;
        }
        
    }

    // Runs the running animation on the given sprite
    public void Running(SpriteRenderer sprite) {
        if (sprite == character) {
            characterStatus = AnimStatus.Running;
        } else if (sprite == shadow) {
            shadowStatus = AnimStatus.Running;
        }
    }

    // Runs the jumping animation on the given sprite
    public void Jumping(SpriteRenderer sprite) {
        if (sprite == character) {
            characterStatus = AnimStatus.Jumping;
        } else if (sprite == shadow) {
            shadowStatus = AnimStatus.Jumping;
        }
    }

    // Runs the death animation on both sprites
    public IEnumerator Death() {
        justDied = true;

        characterStatus = AnimStatus.Dead;
        shadowStatus = AnimStatus.Dead;

        character.sprite = deathSprite;
        shadow.sprite = deathSprite;

        // Break the colliders
        Collider2D[] charColliders = character.GetComponents<Collider2D>();
        Collider2D[] shadColliders = shadow.GetComponents<Collider2D>();
        foreach (Collider2D col in charColliders) { Destroy(col); }
        foreach (Collider2D col in shadColliders) { Destroy(col); }

        // Launch the players upward a bit
        float horizontalPush = Random.Range(-4f, 4f);

        Rigidbody2D charRB = character.GetComponent<Rigidbody2D>();
        Rigidbody2D shadRB = shadow.GetComponent<Rigidbody2D>();
        charRB.velocity += new Vector2(horizontalPush, 6);
        shadRB.velocity += new Vector2(horizontalPush, -6);

        // Spin the players
        float rotationalPush = Random.Range(-360f, 360f);

        charRB.freezeRotation = false;
        shadRB.freezeRotation = false;
        charRB.angularVelocity = rotationalPush;
        shadRB.angularVelocity = rotationalPush;

        yield return new WaitForSeconds(0.75f);

        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
