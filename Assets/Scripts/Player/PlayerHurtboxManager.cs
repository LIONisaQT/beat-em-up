using UnityEngine;
using System.Collections;

public class PlayerHurtboxManager : MonoBehaviour {

    // Set these in the editor
    public PolygonCollider2D
        walk1, walk2, walk3, walk4, walk5, walk6, walk7, walk8,
        jump1, jump2,
        light1, light2,
        heavy1, heavy2, heavy3;

    // Used for organization
    private PolygonCollider2D[] colliders;

    // Collider on this game object
    private PolygonCollider2D localCollider;

    // We say box, but we're still using polygons.
    public enum hurtBoxes {
        walk1box, walk2box, walk3box, walk4box, walk5box, walk6box, walk7box, walk8box,
        jump1Box, jump2Box,
        light1Box, light2Box,
        heavy1Box, heavy2Box, heavy3Box,
        clear // special case to remove all boxes
    }

    void Start() {
        // Set up an array so our script can more easily set up the hit boxes
        colliders = new PolygonCollider2D[] {
            walk1, walk2, walk3, walk4, walk5, walk6, walk7, walk8,
            jump1, jump2,
            light1, light2,
            heavy1, heavy2, heavy3
        };

        // Create a polygon collider
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
        localCollider.pathCount = 0; // Clear auto-generated polygons
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Collider hit something!");
    }

    public void setHurtBox(hurtBoxes val) {
        if (val != hurtBoxes.clear) {
            localCollider.SetPath(0, colliders[(int)val].GetPath(0));
            return;
        }
        localCollider.pathCount = 0;
    }
}