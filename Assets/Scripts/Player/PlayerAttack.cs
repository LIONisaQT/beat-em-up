using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public PolygonCollider2D[] attackHitboxes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Animator anim = GetComponent<Animator>();
        
    }

    private void LaunchAttack(Collider col) {

    }
}
