using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporwaveMovement : PlayerMovement {
    private void Start() {
        anim = GetComponent<Animator>();
        playerSpeed = 7;
        playerJumpPower = 1250;
        totalJumps = 2;
        numJumps = totalJumps;
        canMove = true;

        // Use appropriate keys for correct side
        // This will be optimized later to check for which side the character are on
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        lightAtk = KeyCode.Z;
        heavyAtk = KeyCode.X;
    }

    // Update is called once per frame
    private void Update() {
        base.PlayerLogic(left, right, up, down, lightAtk, heavyAtk);
    }

    // FixedUpdate is always called on fixed intervals, good for physics
    private void FixedUpdate() {
        base.PlayerPhysics(left, right);
    }
}

