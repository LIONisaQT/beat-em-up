using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {
    private GameObject[] players;
    public float xMin, yMin, xMax, yMax;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// LateUpdate is called after all Update functions have been called
	void LateUpdate () {
        float x = Mathf.Clamp(players[0].transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(players[0].transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
