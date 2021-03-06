﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    /*
     * Set these in the editor
     * Define frames the player will use for their animations
     * Each UNIQUE frame gets its own collider
    */
    public PolygonCollider2D light1, heavy1;

    // How much knockback each attack does
    public Vector2 lightAttackForce, heavyAttackForce;

    // sound of the hit
    public AudioClip hitClip;

    public AudioSource hitSource;

    // Holds all the colliders
    protected PolygonCollider2D[] attackHitboxes;

    // Active collider for the game object
    protected PolygonCollider2D currentHitbox;

    // Hitbox states for each UNIQUE hitbox
    public enum hitboxes {
        light1Box,
        heavy1Box, heavy2Box, heavy3Box,
        clear
    }

    // Gets current state of hitbox
    private hitboxes currentState;

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

    // Do something when it collides with another collider + play sound
    void OnTriggerEnter2D(Collider2D col) {
        hitSource.clip = hitClip;
        if (currentState == hitboxes.light1Box) {
            col.GetComponent<Rigidbody2D>().AddForce(lightAttackForce);
            hitSource.Play();
        } else if (currentState == hitboxes.heavy1Box) {
            col.GetComponent<Rigidbody2D>().AddForce(heavyAttackForce);
            hitSource.Play();
        }
    }

    void SetAttackForce() {
        lightAttackForce = new Vector2(200, 0);
        heavyAttackForce = new Vector2(300, 100);
    }

    // Turns on hittbox, used in when adding an animation event
    public void setHitBox(hitboxes val) {
        if (val != hitboxes.clear) {
            currentState = val;
            currentHitbox.SetPath(0, attackHitboxes[(int)val].GetPath(0));
            return;
        }
        currentHitbox.pathCount = 0;
    }
}