using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimator : MonoBehaviour {

    // Array of all of the animation frames
    public Sprite[] animationFrames;

    // The time between each iteration of sprite effects
    public float timeBetweenEffect;

    // The speed of the effect animation (time that should pass between each animation frame)
    public float animationTime;

    // The status of the animation
    private bool isIdling = true;

    // The current frame number (an index of animationFrames)
    private int frameNum = 0;

    private SpriteRenderer sprite;
    private float time = 0;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Animate();

        time += Time.deltaTime;
	}

    // Animates the sprite
    void Animate() {
        if (isIdling && time >= timeBetweenEffect) {
            isIdling = false;
            frameNum++;
            time = 0;

            sprite.sprite = animationFrames[frameNum];
        } else if (!isIdling && time >= animationTime) {
            frameNum++;
            time = 0;

            if (frameNum > animationFrames.Length - 1) {
                frameNum = 0;
                isIdling = true;
            }

            sprite.sprite = animationFrames[frameNum];
        }
    }
}
