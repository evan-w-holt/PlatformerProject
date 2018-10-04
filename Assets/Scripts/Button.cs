using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject platform;
    public Sprite buttonPress;
    public Sprite buttonUp;

    private bool isButtonPressed = false;
    private bool isEnabled;

	// Use this for initialization
	void Start () {
        platform.SetActive(false);
        isEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (isEnabled) {

            StartCoroutine(Reenable());

            if (isButtonPressed) {
                isButtonPressed = false;
                platform.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().sprite = buttonUp;
            } else {
                isButtonPressed = true;
                platform.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().sprite = buttonPress;
            }
        }
        
    }

    // Reenables the button 1 second after being pressed
    IEnumerator Reenable() {
        isEnabled = false;
        yield return new WaitForSeconds(1f);
        isEnabled = true;
    }
}
