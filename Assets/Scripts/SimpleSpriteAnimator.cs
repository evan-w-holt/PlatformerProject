using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteAnimator : MonoBehaviour {

    // Array of all of the animation frames
    public Sprite[] animationFrames;

    // The number of frames between each iteration of sprite effects
    public int framesBetweenEffect;

    // The speed of the effect animation (number of frames that should pass between each animation frame)
    public int animationFramerate;

    // The status of the animation
    private bool isIdling = true;

    // The current frame number (an index of animationFrames)
    private int frameNum = 0;

    private SpriteRenderer sprite;
    private int framesPassed = 0;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Animate();

        framesPassed++;
	}

    // Animates the sprite
    void Animate() {
        if (isIdling && framesPassed >= framesBetweenEffect) {
            isIdling = false;
            frameNum++;
            framesPassed = 0;

            sprite.sprite = animationFrames[frameNum];
        } else if (!isIdling && framesPassed >= animationFramerate) {
            frameNum++;
            framesPassed = 0;

            if (frameNum > animationFrames.Length - 1) {
                frameNum = 0;
                isIdling = true;
            }

            sprite.sprite = animationFrames[frameNum];
        }
    }
}
