using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {
    private GameObject[] players;
    public float xMin, yMin, xMax, yMax;
    // private float margin;
    // private float z0; // z coordinate of the fighters plane
    // private float zCam; // camera distance
    // private float wScene; // scene width
    // private float xL; // left screen x coordinate
    // private float xR; // right screen x coordinate

    // void CalcScreen(transform p1, transform p2) {
    // 	if (p1.position.x < p2.position.x) {
    // 		xL = p1.position.x - margin;
    // 		xR = p2.position.x + margin;
    // 	} else {
    // 		xL = p2.position.x - margin;
    // 		xR = p1.position.x + margin;
    // 	}
    // }

	// Use this for initialization
	void Start () {
		// margin = 1.5f;
        players = GameObject.FindGameObjectsWithTag("Player");
        // CalcScreen(players[0].transform, players[1].transform);
        // wScene = xR - xL;
        // zCam = transform.position.z - z0;
	}
	
	// LateUpdate is called after all Update functions have been called
	void LateUpdate () {
		// CalcScreen(players[0].transform, players[1].transform);
		// float width = xR - xL;
		// if (width > wScene) {
		// 	gameObject.transform.position.x = zCam * width / wScene + z0;
		// }
		// gameObject.transform.position.x = (xR + xL) / 2;
        float x0 = Mathf.Clamp(players[0].transform.position.x, xMin, xMax);
        float y0 = Mathf.Clamp(players[0].transform.position.y, yMin, yMax);
        float x1 = Mathf.Clamp(players[1].transform.position.x, xMin, xMax);
        float y1 = Mathf.Clamp(players[1].transform.position.y, xMin, xMax);
        gameObject.transform.position = new Vector3((x0 + x1) / 2, (y0 + y1) / 2, gameObject.transform.position.z);
    }
}
