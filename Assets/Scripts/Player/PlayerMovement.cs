using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    protected int numJumps;
    protected bool isGrounded, canMove;
    protected Animator anim;

    public float playerSpeed, playerJumpPower;
    public int totalJumps;
    public KeyCode left, right, up, down, lightAtk, heavyAtk;


    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        playerSpeed = 7;
        playerJumpPower = 1250;
        totalJumps = 2;
        numJumps = totalJumps;
        canMove = true;

        // Default character keys just in case
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        lightAtk = KeyCode.Z;
        heavyAtk = KeyCode.X;
    }

    // Update is called once per frame
    private void Update() {
        PlayerLogic(left, right, up, down, lightAtk, heavyAtk);
    }

    // FixedUpdate is always called on fixed intervals, good for physics
    private void FixedUpdate() {
        PlayerPhysics(left, right);
    }

    // Player movement logic
    protected void PlayerLogic(KeyCode left, KeyCode right, KeyCode up, KeyCode down, KeyCode light, KeyCode heavy) {
        // Animations on ground
        if (isGrounded) {
            // Movement
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.0f && canMove) {
                anim.SetBool("Walking", true);
            } else if (!CheckActiveAttackStates()) {
                anim.SetBool("Walking", false);
            }

            // Crouching
            if (Input.GetKey(down)) {
                anim.SetBool("Crouching", true);
                canMove = false;
            }
            if (Input.GetKeyUp(down)) {
                anim.SetBool("Crouching", false);
                ResetCanMove();
            }

            if (anim.GetBool("Crouching")) {
                if (Input.GetKeyDown(lightAtk)) {
                    canMove = false;
                    if (!CheckActiveAttackStates()) {
                        anim.SetTrigger("LightAttack");
                        print("crouching light");
                    }
                }
                if (Input.GetKeyDown(heavyAtk)) {
                    canMove = false;
                    if (!CheckActiveAttackStates()) {
                        anim.SetTrigger("HeavyAttack");
                        print("crouching heavy");
                    }
                }
            } else if (anim.GetBool("Walking")) {
                if (Input.GetKeyDown(lightAtk)) {
                    if (!CheckActiveAttackStates()) {
                        anim.SetTrigger("LightAttack");
                        print("moving light");
                    }
                }
                if (Input.GetKeyDown(heavyAtk)) {
                    canMove = false;
                    if (!CheckActiveAttackStates()) {
                        anim.SetTrigger("HeavyAttack");
                        print("moving heavy");
                    }
                }
            } else {
                if (Input.GetKeyDown(lightAtk)) {
                    canMove = false;
                    if (!CheckActiveAttackStates()) {
                        anim.SetTrigger("LightAttack");
                        print("light");
                    }
                } else if (Input.GetKeyDown(heavyAtk)) {
                    canMove = false;
                    if (!CheckActiveAttackStates()) {
                        anim.SetTrigger("HeavyAttack");
                        print("heavy");
                    }
                }
            }
        }

        // Animations in air
        else {
            // Maybe different jumping and falling animations?
            if (Input.GetKeyDown(lightAtk)) {
                if (!CheckActiveAttackStates()) {
                    //anim.SetTrigger("LightAttack");
                    print("light air");
                }
            }
            if (Input.GetKeyDown(heavyAtk)) {
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
    protected void PlayerPhysics(KeyCode left, KeyCode right) {
        if (!CheckActiveAttackStates() && canMove) {
            if (Input.GetKey(right)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
            } else if (Input.GetKey(left)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (Input.GetKeyDown(up) && numJumps > 0) { Jump(); }
        }
    }

    protected void Jump() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0); // Resets player y velocity so double jump works correctly
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        anim.SetBool("Walking", false);
        anim.SetBool("Jumping", true);
        isGrounded = false;
        numJumps--;
    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
            if (anim.GetBool("Jumping")) {
                anim.SetBool("Jumping", false);
                numJumps = totalJumps;
                // clear hurtboxes here, otherwise player's jumping hurthox will persist
                GetComponent<PlayerHurtboxManager>().setHurtBox(PlayerHurtboxManager.hurtboxes.clear);
            }
        }
    }

    /*
     * Helper function that checks what attacks are active
     * Ensures attack commands can't be buffered while an attack is active (prolly not a good idea for combos, but we'll see)
    */
    protected bool CheckActiveAttackStates() {
        return (anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"));
    }

    /*
     * Helper function that resets canMove
     * Ensures player attack commands override player movement commands, so they can attack while moving
     * Gets called in the player animation window for light and heavy attacks
     * Also gets called when uncrouching
    */
    protected void ResetCanMove() {
        canMove = true;
    }
}