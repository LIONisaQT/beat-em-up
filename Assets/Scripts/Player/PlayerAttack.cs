using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Currently watching (both have different implementaions):
 * Fast n' dirty: https://www.youtube.com/watch?v=mvVM1RB4HXk
 * More in-depth: https://www.youtube.com/watch?v=x_b6A-DSdkk&index=4&list=PL1bPKmY0c-wmOi4Ki-6ryydPeHEcQpul0
*/

public class PlayerAttack : MonoBehaviour {
    public Collider[] attackHitboxes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z)) {
            LaunchAttack(attackHitboxes[0]);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            LaunchAttack(attackHitboxes[1]);
        }
	}

    private void LaunchAttack(Collider col) {

    }
}
