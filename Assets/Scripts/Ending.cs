using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {

    // The character and shadow
    public GameObject character;
    public GameObject shadow;

    // The shadow sprite for the end of the scene
    public GameObject endShadow;

    // Surprised sprite and jump sprite
    public Sprite surprise;
    public Sprite jump;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Runs the ending scene
    IEnumerator PlayScene() {
        yield return new WaitForSeconds(1f);

        SetSprite(surprise);
        for (int i = 0; i < 25; i++) {
            CameraShake();
            yield return null;
            yield return null;
            yield return null;
        }

        transform.position = new Vector3(0, 0, -10);
        SetSprite(jump);
        shadow.SetActive(false);
        endShadow.SetActive(true);
        Jumping();
    }

    // Sets the sprites
    void SetSprite(Sprite sprite) {
        character.GetComponent<SpriteRenderer>().sprite = sprite;
        shadow.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Shakes the camera
    void CameraShake() {
        transform.position = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), -10);
    }

    // Makes the character jump
    void Jumping() {
        
    }
}
