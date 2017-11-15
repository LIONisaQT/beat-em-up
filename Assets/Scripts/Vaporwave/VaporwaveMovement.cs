using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporwaveMovement : PlayerMovement {
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
                anim.SetBool("Walking", true);
            } else if (!CheckActiveAttackStates()) {
                anim.SetBool("Walking", false);
                // Clear hurtbox, otherwise whatever last walking hurtbox will persist
                //PlayerHurtboxManager hurtScript = this.GetComponent<PlayerHurtboxManager>();
                //hurtScript.setHurtBox(PlayerHurtboxManager.hurtboxes.clear);
            }

            // Crouching
            if (Input.GetKey(KeyCode.S)) {
                anim.SetBool("Crouching", true);
                canMove = false;
            }
            if (Input.GetKeyUp(KeyCode.S)) {
                anim.SetBool("Crouching", false);
                ResetCanMove();
            }

            // Attacks
            if (Input.GetKeyDown(KeyCode.Z)) {
                canMove = false;
                if (!CheckActiveAttackStates()) {
                    anim.SetTrigger("LightAttack");
                }
            } else if (Input.GetKeyDown(KeyCode.X)) {
                canMove = false;
                if (!CheckActiveAttackStates()) {
                    anim.SetTrigger("HeavyAttack");
                }
            }
        }

        // Animations in air
        else {
            // Maybe different jumping and falling animations?
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
            if (Input.GetKey(KeyCode.D)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
            } else if (Input.GetKey(KeyCode.A)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (Input.GetKeyDown(KeyCode.W) && numJumps > 0) { Jump(); }
        }
    }
}

