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
        idle1, idle2, idle3, idle4, idle5, idle6, idle7,
        crouch1;

    // Holds all the colliders
    protected PolygonCollider2D[] colliders;

    // Active collider for the game object
    protected PolygonCollider2D localCollider;

    // Hurtbox states for each UNIQUE hurtbox
    public enum hurtboxes {
        walk1box, walk2box, walk3box, walk4box, walk5box, walk6box, walk7box, walk8box,
        jump1Box, jump2Box,
        light1Box, light2Box,
        heavy1Box, heavy2Box, heavy3Box,
        idle1Box, idle2Box, idle3Box, idle4Box, idle5Box, idle6Box, idle7Box,
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
            idle1, idle2, idle3, idle4, idle5, idle6, idle7,
            crouch1
        };

        // Create a polygon collider
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = false; // False == collides with ground
        localCollider.pathCount = 0; // Clear auto-generated polygons
    }

    // Do something when it collides with another collider
    void OnTriggerEnter2D(Collider2D col) {
    }

    // Turns on hurtbox, used in when adding an animation event
    public void setHurtBox(hurtboxes val) {
        if (val != hurtboxes.clear) {
            localCollider.SetPath(0, colliders[(int)val].GetPath(0));
            return;
        }
        localCollider.pathCount = 0;
    }
}