using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private int numJumps;
	//checks
    private bool isGrounded, isMoving, isFlying, isCrouching, canMove;
    private Animator anim;

    public float playerSpeed, playerJumpPower;
    public int totalJumps;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        playerSpeed = 7;
        playerJumpPower = 1250;
        totalJumps = 2;
        numJumps = totalJumps;
        canMove = true;
	}
	
	// Update is called once per frame
	void Update() {
        PlayerLogic();
    }

    // FixedUpdate is always called on fixed intervals, good for physics
    private void FixedUpdate() {
        PlayerPhysics();
    }

    // Player movement logic
    void PlayerLogic() {
        // Animations on ground
        if (isGrounded) {
            // Movement
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.0f && canMove) {
				//isMoving = true
                anim.SetBool("Walking", true);
				isMoving = true;
            } else {
				//isMoving = false
                anim.SetBool("Walking", false);
				isMoving = false;
            }

            // Crouching
            if (Input.GetKey("down")) {
                anim.SetBool("Crouching", true);
                canMove = false;
				isCrouching = true;
            }
            if (Input.GetKeyUp("down")) {
                anim.SetBool("Crouching", false);
                ResetCanMove();
				isCrouching = false;
            }
				

			if (isCrouching) {

				if (Input.GetKeyDown (KeyCode.Z)) {
					canMove = false;
					if (!CheckActiveAttackStates ()) {
						anim.SetTrigger ("LightAttack");
						print ("crouching light");
					}
					//basic heavy attack
				} else if (Input.GetKeyDown (KeyCode.X)) {
					canMove = false;
					if (!CheckActiveAttackStates ()) {
						anim.SetTrigger ("HeavyAttack");
						print ("crouching heavy");
					}
				}

			} else if (isMoving) {

				if (Input.GetKeyDown (KeyCode.Z)) {
					//canMove = false;
					if (!CheckActiveAttackStates ()) {
						anim.SetTrigger ("LightAttack");
						print ("moving light");
					}
				} else if (Input.GetKeyDown (KeyCode.X)) {
					canMove = false;
					if (!CheckActiveAttackStates ()) {
						anim.SetTrigger ("HeavyAttack");
						print ("moving heavy");
					}
				}

			} else {

				if (Input.GetKeyDown (KeyCode.Z)) {
					canMove = false;
					if (!CheckActiveAttackStates ()) {
						anim.SetTrigger ("LightAttack");
						print ("light");
					}
				} else if (Input.GetKeyDown (KeyCode.X)) {
					canMove = false;
					if (!CheckActiveAttackStates ()) {
						anim.SetTrigger ("HeavyAttack");
						print ("heavy");
					}
				}
					
			}


        }
        
        // Animations in air
        else {
			//basic light air attack
			if (Input.GetKeyDown (KeyCode.Z)) {
				if (!CheckActiveAttackStates()) {
					//anim.SetTrigger("LightAttack");
					print("light air");
				}
			}
            // Maybe different jumping and falling animations?
			if (Input.GetKeyDown (KeyCode.X)) {
				if (!CheckActiveAttackStates()) {
					//anim.SetTrigger("HeavyAttack");
					print("heavy air");
				}
			}

        }

        // Player direction
        // Commented block makes player turn on left/right input; barring personal moveset reasons, players should always face their opponent
        //if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 0.0f) {
        //    GetComponent<SpriteRenderer>().flipX = true;
        //} else if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.0f) {
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
    }

    // Player physics logic
    void PlayerPhysics() {
        if (!CheckActiveAttackStates() && canMove) {
            if (Input.GetKey("right")) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
            } else if (Input.GetKey("left")) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (Input.GetButtonDown("Jump") && numJumps > 0) { Jump(); }
        }
    }

    void Jump() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0); // Resets player y velocity so double jump works correctly
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        anim.SetBool("Walking", false);
        anim.SetBool("Jumping", true);
        isGrounded = false;
        numJumps--;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            anim.SetBool("Jumping", false);
            PlayerHurtboxManager hurtScript = this.GetComponent<PlayerHurtboxManager>();
            hurtScript.setHurtBox(PlayerHurtboxManager.hurtBoxes.clear);
            isGrounded = true;
            numJumps = totalJumps;
        }
    }

    /*
     * Helper function that checks what attacks are active
     * Ensures attack commands can't be buffered while an attack is active (prolly not a good idea for combos, but we'll see)
    */
    private bool CheckActiveAttackStates() {
        return (anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"));
    }

    /*
     * Helper function that resets canMove
     * Ensures player attack commands override player movement commands, so they can attack while moving
     * Gets called in the player animation window for light and heavy attacks
     * Also gets called when uncrouching
    */
    private void ResetCanMove() {
        canMove = true;
    }
}
