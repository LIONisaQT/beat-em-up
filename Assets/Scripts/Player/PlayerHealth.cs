using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public int health;

	// Use this for initialization
	void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        // Quick die button
        if (Input.GetKeyDown(KeyCode.E)) {health = 0;}

        // Go to next round if not final round I guess
        // Else go to back to character select or something?
        // Currently just restarts the fight scene
        if (health <= 0) {
            StartCoroutine("Die");
        }
	}

    // Like a method, but you can make it wait before executing
    // Probably useful somewhere else but it's cool soooo c:
    IEnumerator Die() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Fight");
    }
}
