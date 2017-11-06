using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private int numJumps;
    private float moveX; // Returns float between -1 and 1 depending on which way player is moving
    private bool isGrounded, canMove;
    private Animator anim;

    public float playerSpeed, playerJumpPower;
    public int totalJumps;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
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
        } else {
            Debug.Log("you shouldn't be moving wtf");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        // Animations
        /*
         * Player cannot launch attack while moving; they must wait until their x velocity is 0 to be able to trigger attack
         * Player can attack, then input the walk command to move while the attack animation is playing
        */
        if (!isGrounded) {
        } else {
            if (Mathf.Abs(moveX) < 0.0f) {
                anim.SetBool("Walking", true);
            } else if (Input.GetKeyDown(KeyCode.Z)) {
                anim.SetTrigger("LightAttack");
                canMove = false;
            } else if (Input.GetKeyDown(KeyCode.X)) {
                anim.SetTrigger("HeavyAttack");
                canMove = false;
            } else {
                PlayerHurtboxManager hurtScript = this.GetComponent<PlayerHurtboxManager>();
                hurtScript.setHurtBox(PlayerHurtboxManager.hurtBoxes.clear);
                anim.SetBool("Walking", false);
                canMove = true;
            }
            PlayerAttack atkScript = this.GetComponent<PlayerAttack>();
            atkScript.setHitBox(PlayerAttack.hitBoxes.clear);
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
        anim.SetBool("Jumping", true);
        isGrounded = false;
        numJumps--;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            anim.SetBool("Jumping", false);
            isGrounded = true;
            numJumps = totalJumps;
        }
    }
}
