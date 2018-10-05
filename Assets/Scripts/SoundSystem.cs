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

    // The empty game object containing the music
    private static GameObject musicObject = null;

    void Awake() {
        instance = this;
	}

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();

        GetMusic();
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

    // Gets the music object and make sure there are not multiples
    void GetMusic() {
        GameObject music = GameObject.Find("Music");

        if (music) {
            if (!musicObject) {
                musicObject = music;

                if (!musicObject.GetComponent<AudioSource>().isPlaying) {
                    musicObject.GetComponent<AudioSource>().Play();
                }

                DontDestroyOnLoad(music);
            }
        }
    }
}
