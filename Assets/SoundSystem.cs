using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    // The single instance of this object
    public static SoundSystem instance;

    // The audio source attached to the character
    private AudioSource source;

    // The sound effects
    public AudioClip jump;
    public AudioClip death;
    public AudioClip spikes;

    void Awake() {
        instance = this;
	}

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    // Plays the jump sound
    public void PlayJumpSound() {
        source.PlayOneShot(jump, 0.5f);
    }

    // Plays the death sounds
    public void PlayDeathSound() {
        source.PlayOneShot(death);
        source.PlayOneShot(spikes);
    }
}
