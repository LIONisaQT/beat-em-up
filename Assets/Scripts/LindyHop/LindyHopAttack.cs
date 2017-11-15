using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LindyHopAttack : PlayerAttack {

    // Use this for initialization
    void Start() {
        // Initialize colliders
        attackHitboxes = new PolygonCollider2D[] {
            light1,
            heavy1
        };

        // Create a polygon collider
        currentHitbox = gameObject.AddComponent<PolygonCollider2D>();
        currentHitbox.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
        currentHitbox.pathCount = 0; // Clear auto-generated polygons
        SetAttackForce();
    }

    void SetAttackForce() {
        lightAttackForce = new Vector2(-300, 0);
        heavyAttackForce = new Vector2(-500, 100);
    }
}
