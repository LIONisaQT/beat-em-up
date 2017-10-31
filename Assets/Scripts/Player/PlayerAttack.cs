using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public PolygonCollider2D light1, heavy1;
    public PolygonCollider2D[] attackHitboxes;
    private PolygonCollider2D currentHitbox;
    public enum hitboxes {
    	light1Box,
    	heavy1Box,
    	clear
    }

	// Use this for initialization
	void Start () {
		attackHitboxes = new PolygonCollider2D[] {
			light1,
			heavy1
		};

		currentHitbox = gameObject.AddComponent<PolygonCollider2D>();
		currentHitbox.isTrigger = true;
		currentHitbox.pathCount = 0;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("Hit something!");
	}

    public void setHitBox(hitboxes val) {
    	if (val != hitboxes.clear) {
    		currentHitbox.SetPath(0, attackHitboxes[(int)val].GetPath(0));
    		return;
    	}
    	currentHitbox.pathCount = 0;
    }
}
