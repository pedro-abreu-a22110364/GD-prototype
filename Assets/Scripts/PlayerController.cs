using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector2 input;

    private Vector2 lastMove;

    public Animator animator;

    private void Update()
    {

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero)
        {
            lastMove = input;

            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
        else
        {
            animator.SetFloat("moveX", lastMove.x);
            animator.SetFloat("moveY", lastMove.y);
        }

        animator.SetFloat("speed", input.sqrMagnitude);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime);
    }

}