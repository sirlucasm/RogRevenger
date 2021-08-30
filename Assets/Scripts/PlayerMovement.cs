using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float playerSpeed;
    public float runningSpeed = 5f;
    public float walkSpeed = 2.5f;
    public float strafeSpeed = 5f;

    float gravity;
    float jumpSpeed;
    public float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.35f;

    void Start() {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

    }

    void Update() {


        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            playerSpeed = walkSpeed;
            // strafeSpeed = walkSpeed;
        }
        else {
            playerSpeed = runningSpeed;
        }

        // force = input * speed * direction
        forward = forwardInput * playerSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if(controller.isGrounded) {
            vertical = Vector3.down;

            if (forward != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) {
                // walking
                animator.SetFloat("Speed", 0.8f, 0.1f, Time.deltaTime);
            } else if (forward != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) {
                // running
                animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
            } else if (forward == Vector3.zero) {
                // idle
                animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
            }

            if (strafeInput > 0f) {
                // strafing right
                animator.SetBool("StrafeRight", true);
            } else if (strafeInput < 0f) {
                // strafing left
                animator.SetBool("StrafeLeft", true);
            } else if (strafeInput == 0f) {
                // idle
                animator.SetBool("StrafeRight", false);
                animator.SetBool("StrafeLeft", false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0) {
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);


    }
}
