using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporwaveHurtbox : PlayerHurtboxManager {
    // Use this for initialization
    void Start () {
        // Initialize colliders
        colliders = new PolygonCollider2D[] {
            walk1, walk2, walk3, walk4, walk5, walk6, walk7, walk8,
            jump1, jump2,
            light1, light2,
            heavy1, heavy2, heavy3,
            idle1, idle2, idle3, idle4, idle5, 
            crouch1
        };

        // Create a polygon collider
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = false; // False == collides with ground
        localCollider.pathCount = 0; // Clear auto-generated polygons
    }
}
