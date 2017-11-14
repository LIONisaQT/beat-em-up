using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start () {
        rb.velocity = new Vector2(speed, 0);
	}
	
	void Update () {
		
	}
}
