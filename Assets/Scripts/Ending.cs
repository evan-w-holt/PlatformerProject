using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {

    // The character and shadow
    public GameObject character;
    public GameObject shadow;

    // The shadow sprite for the end of the scene
    public GameObject endShadow;

    // The screen overlay in the canvas
    public Image overlay;

    // Surprised sprite and jump sprite
    public Sprite surprise;
    public Sprite jump;

    public float jumpHeight;

    private bool isGrounded;

    // The layer mask for grounding raycasts
    private int groundingMask = int.MaxValue - (1 << 8);

    // Use this for initialization
    void Start () {
        GameObject music = GameObject.Find("Music");
        if (music) { Destroy(music); }

        StartCoroutine(PlayScene());
	}
	
	// Update is called once per frame
	void Update () {
        UpdateGrounding();
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
        StartCoroutine(Jumping());

        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
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
    IEnumerator Jumping() {
        while (true) {
            yield return new WaitUntil(delegate { return isGrounded; });

            character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
            SoundSystem.instance.PlayJumpSound();
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Determines if the player can jump
    void UpdateGrounding() {
        // groundCheck is below the character if jumpHeight is positive, otherwise it is above the character
        Vector3 groundCheck = character.transform.position - new Vector3(0, 0.55f * (jumpHeight < 0 ? -1 : 1), 0);

        RaycastHit2D hit = Physics2D.Linecast(character.transform.position, groundCheck, groundingMask);
        if (hit.collider != null) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    // Fades the screen to black
    IEnumerator FadeOut() {
        for (int i = 0; i < 256; i++) {
            overlay.color = new Color(0, 0, 0, i/255f);
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
