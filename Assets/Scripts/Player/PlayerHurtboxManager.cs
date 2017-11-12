using UnityEngine;
using System.Collections;

public class PlayerHurtboxManager : MonoBehaviour {
    /*
     * Set these in the editor
     * Define frames the player will use for their animations
     * Each UNIQUE frame gets its own collider
    */
    public PolygonCollider2D
        walk1, walk2, walk3, walk4, walk5, walk6, walk7, walk8,
        jump1, jump2,
        light1, light2,
        heavy1, heavy2, heavy3,
        idle1, idle2,
        crouch1;

    // Holds all the colliders
    private PolygonCollider2D[] colliders;

    // Active collider for the game object
    private PolygonCollider2D localCollider;

    // Hurtbox states for each UNIQUE hurtbox
    public enum hurtBoxes {
        walk1box, walk2box, walk3box, walk4box, walk5box, walk6box, walk7box, walk8box,
        jump1Box, jump2Box,
        light1Box, light2Box,
        heavy1Box, heavy2Box, heavy3Box,
        idle1Box, idle2Box,
        crouch1Box,
        clear // special case to remove all boxes
    }

    void Start() {
    	// Initialize colliders
        colliders = new PolygonCollider2D[] {
            walk1, walk2, walk3, walk4, walk5, walk6, walk7, walk8,
            jump1, jump2,
            light1, light2,
            heavy1, heavy2, heavy3,
            idle1, idle2,
            crouch1
        };

        // Create a polygon collider
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = false; // Set as a trigger so it doesn't collide with our environment
        localCollider.pathCount = 0; // Clear auto-generated polygons
    }

    // Do something when it collides with another collider
    void OnTriggerEnter2D(Collider2D col) {
        // Debug.Log("Collider hit something!");
    }

    // Turns on hurtbox, used in when adding an animation event
    public void setHurtBox(hurtBoxes val) {
        if (val != hurtBoxes.clear) {
            localCollider.SetPath(0, colliders[(int)val].GetPath(0));
            return;
        }
        localCollider.pathCount = 0;
    }
}