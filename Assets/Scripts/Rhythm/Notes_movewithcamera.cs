using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes_movewithcamera : MonoBehaviour {

    private GameObject[] players;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(players[0].transform.position.x+11, gameObject.transform.position.y);
    }
}
