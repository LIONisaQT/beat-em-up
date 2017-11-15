using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagLogic : MonoBehaviour {
    public int health;

	// Use this for initialization
	void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            Die();
        }
    }

    public void removeHealth(int dmg) {
        health -= dmg;
        print("new health: " + health);
    }

    void Die() {
        print("ah fuck i can't believe you've done this");
        health = 100;
    }
}
