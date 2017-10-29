using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private int numJumps;
    private float moveX; // Returns float between -1 and 1 depending on which way player is moving
    private bool isGrounded, canMove;

    public float playerSpeed, playerJumpPower;
    public int totalJumps;

    // Use this for initialization
    void Start() {
        playerSpeed = 10;
        playerJumpPower = 1250;
        totalJumps = 2;
        numJumps = totalJumps;
        canMove = true;
	}
	
	// Update is called once per frame
	void Update() {
        PlayerMove();
    }

    // FixedUpdate is always called on fixed intervals, good for physics
    private void FixedUpdate() {
        PlayerPhysics();
    }

    void PlayerMove() {
        // Controls
        if (canMove) {
            moveX = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump") && numJumps > 0) {
                Jump();
            }
        }

        // Animations
        Animator anim = GetComponent<Animator>();
        if (!isGrounded) {
            anim.SetBool("Walking", false);
            anim.SetBool("Jumping", true);
        } else {
            anim.SetBool("Jumping", false);
            if (Mathf.Abs(moveX) > 0.0f) {
                anim.SetBool("Walking", true);
            } else {
                anim.SetBool("Walking", false);
            }
        }

        // Player direction
        // Commented block makes player turn on left/right input; barring personal moveset reasons, players should always face their opponent
        //if (moveX < 0.0f) {
        //    GetComponent<SpriteRenderer>().flipX = true;
        //} else if (moveX > 0.0f) {
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
    }

    void PlayerPhysics() {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump() {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0); // Resets player y velocity so double jump works correctly
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
        numJumps--;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
            numJumps = totalJumps;
        }
    }
}
