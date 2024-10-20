using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector2 input;

    public Vector2 lastMove = Vector2.zero;

    public Animator animator;

    public Vector2 startingPosition = new Vector2(-7.5f, -2);

    private void Start()
    {
        transform.position = startingPosition;
    }

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "ExitStartToHallway":
                SceneManager.LoadScene("Scenes/Hallway");

                startingPosition = new Vector2(-8.5f, -0.25f);
                transform.position = startingPosition;
                //StartCoroutine(ChangePosition(2f));
                break;
            case "ExitHallwayToStart":
                SceneManager.LoadScene("Scenes/Start");

                startingPosition = new Vector2(8.5f, -0.25f);
                transform.position = startingPosition;
                //StartCoroutine(ChangePosition(2f));
                break;
        }
    }
    IEnumerator ChangePosition(float delay)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(delay);
        
        transform.position = startingPosition;
    }

}