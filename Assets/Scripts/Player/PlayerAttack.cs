using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	/*
     * Set these in the editor
     * Define frames the player will use for their animations
     * Each UNIQUE frame gets its own collider
    */
	public PolygonCollider2D light1, heavy1;

	// Holds all the colliders
    public PolygonCollider2D[] attackHitboxes;

    // Active collider for the game object
    private PolygonCollider2D currentHitbox;

    // Hitbox states for each UNIQUE hitbox
    public enum hitboxes {
    	light1Box,
    	heavy1Box,
    	clear
    }

	// Use this for initialization
	void Start () {
		// Initialize colliders
		attackHitboxes = new PolygonCollider2D[] {
			light1,
			heavy1
		};

		// Create a polygon collider
		currentHitbox = gameObject.AddComponent<PolygonCollider2D>();
		currentHitbox.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
		currentHitbox.pathCount = 0; // Clear auto-generated polygons
	}
	
	// Do something when it collides with another collider
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("Hit something!");
	}

	// Turns on hittbox, used in when adding an animation event
    public void setHitBox(hitboxes val) {
    	if (val != hitboxes.clear) {
    		currentHitbox.SetPath(0, attackHitboxes[(int)val].GetPath(0));
    		return;
    	}
    	currentHitbox.pathCount = 0;
    }
}
