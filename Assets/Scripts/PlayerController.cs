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

    public Vector2 startingPosition = new Vector2(7.4f, -5f);//-10.5f, 0.5f);

    private void Start()
    {
        transform.position = startingPosition;
    }

    private void Update()
    {
        if(!GameManager.Instance.IsDialogueActive())
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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "SecurityRoomToStart":
                SceneManager.LoadScene("Scenes/Start");

                startingPosition = new Vector2(-7.5f, -2);
                ChangePosition();
                break;
            case "ExitStartToHallway":
                SceneManager.LoadScene("Scenes/Hallway");

                startingPosition = new Vector2(-8.5f, -0.25f);
                ChangePosition();
                break;
            case "ExitHallwayToStart":
                SceneManager.LoadScene("Scenes/Start");

                startingPosition = new Vector2(8.5f, -0.25f);
                ChangePosition();
                break;
            case "ExitHallwayToSewerStart":
                SceneManager.LoadScene("Scenes/SewerStart");

                startingPosition = new Vector2(-8.5f, -0.25f);
                ChangePosition();
                break;
            case "ExitSewerStartToHallway":
                SceneManager.LoadScene("Scenes/Hallway");

                startingPosition = new Vector2(8.5f, -0.25f);
                ChangePosition();
                break;
            case "ExitSewerStartToSewerStart2":
                SceneManager.LoadScene("Scenes/SewerStart2");

                startingPosition = new Vector2(-8.5f, -1f);
                ChangePosition();
                break;
            case "ExitSewerStartToEndStart":
                SceneManager.LoadScene("Scenes/EndStart");

                startingPosition = new Vector2(6.5f, -3f);
                ChangePosition();
                break;
            case "ExitEndStartToSewerStart":
                SceneManager.LoadScene("Scenes/SewerStart");

                startingPosition = new Vector2(-6.5f, -3f);
                ChangePosition();
                break;
            case "ExitEndStartToEnd":
                SceneManager.LoadScene("Scenes/End");

                startingPosition = new Vector2(5f, 0f);
                ChangePosition();
                break;
            case "ExitEndToEndStart":
                SceneManager.LoadScene("Scenes/EndStart");

                startingPosition = new Vector2(-5.5f, 1f);
                ChangePosition();
                break;
            case "ExitSewerStart2ToSewerStart":
                SceneManager.LoadScene("Scenes/SewerStart");

                startingPosition = new Vector2(8.5f, -1.5f);
                ChangePosition();
                break;
            case "ExitSewerStart2ToSewerFlood":
                SceneManager.LoadScene("Scenes/SewerFlood");

                startingPosition = new Vector2(4.5f, 4f);
                ChangePosition();
                break;
            case "ExitSewerFloodToSewerStart2":
                SceneManager.LoadScene("Scenes/SewerStart2");

                startingPosition = new Vector2(6.5f, -4f);
                ChangePosition();
                break;
            case "ExitSecurityRoomToStart":
                SceneManager.LoadScene("Scenes/Start");

                startingPosition = new Vector2(-8.5f, -5.25f);
                ChangePosition();
                break;
        }
    }
    void ChangePosition()
    {
        transform.position = startingPosition;
    }

}