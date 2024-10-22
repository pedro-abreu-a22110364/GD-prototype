using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector2 input;

    public Vector2 lastMove = Vector2.zero;

    public Animator animator;

    public Vector2 startingPosition = new Vector2(7.4f, -5f);

    public CinemachineConfiner2D cinemachineConfiner;

    private void Start()
    {
        transform.position = startingPosition;

        if (cinemachineConfiner == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                foreach (Transform child in playerObject.transform)
                {
                    if (child.CompareTag("VirtualCamera"))
                    {
                        cinemachineConfiner = child.GetComponent<CinemachineConfiner2D>();

                        if (cinemachineConfiner == null)
                        {
                            Debug.LogError("CinemachineConfiner not found on the virtual camera.");
                        }
                        break;
                    }
                }

                if (cinemachineConfiner == null)
                {
                    Debug.LogError("No child with tag 'VirtualCamera' found under the player.");
                }
            }
            else
            {
                Debug.LogError("GameObject with tag 'Player' not found.");
            }
        }

        UpdateConfiner();
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
                UpdateConfiner();
                break;
            case "ExitStartToHallway":
                SceneManager.LoadScene("Scenes/Hallway");

                startingPosition = new Vector2(-8.5f, -0.25f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitHallwayToStart":
                SceneManager.LoadScene("Scenes/Start");

                startingPosition = new Vector2(8.5f, -0.25f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitHallwayToSewerStart":
                SceneManager.LoadScene("Scenes/SewerStart");

                startingPosition = new Vector2(-8.5f, -0.25f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSewerStartToHallway":
                SceneManager.LoadScene("Scenes/Hallway");

                startingPosition = new Vector2(8.5f, -0.25f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSewerStartToSewerStart2":
                SceneManager.LoadScene("Scenes/SewerStart2");

                startingPosition = new Vector2(-8.5f, -1f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSewerStartToEndStart":
                SceneManager.LoadScene("Scenes/EndStart");

                startingPosition = new Vector2(6.5f, -3f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitEndStartToSewerStart":
                SceneManager.LoadScene("Scenes/SewerStart");

                startingPosition = new Vector2(-6.5f, -3f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitEndStartToEnd":
                SceneManager.LoadScene("Scenes/End");

                startingPosition = new Vector2(7.5f, -5f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitEndToEndStart":
                SceneManager.LoadScene("Scenes/EndStart");

                startingPosition = new Vector2(-5.5f, 1f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSewerStart2ToSewerStart":
                SceneManager.LoadScene("Scenes/SewerStart");

                startingPosition = new Vector2(8.5f, -1.5f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSewerStart2ToSewerFlood":
                SceneManager.LoadScene("Scenes/SewerFlood");

                startingPosition = new Vector2(4.5f, 4f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSewerFloodToSewerStart2":
                SceneManager.LoadScene("Scenes/SewerStart2");

                startingPosition = new Vector2(6.5f, -4f);
                ChangePosition();
                UpdateConfiner();
                break;
            case "ExitSecurityRoomToStart":
                SceneManager.LoadScene("Scenes/Start");

                startingPosition = new Vector2(-8.5f, -5.25f);
                ChangePosition();
                UpdateConfiner();
                break;
        }
    }
    void ChangePosition()
    {
        transform.position = startingPosition;
    }

    void UpdateConfiner()
    {
        GameObject boundsObject = GameObject.FindGameObjectWithTag("CameraBounds");

        if (boundsObject != null)
        {
            PolygonCollider2D newConfinerShape = boundsObject.GetComponent<PolygonCollider2D>();

            if (newConfinerShape != null && cinemachineConfiner != null)
            {
                cinemachineConfiner.m_BoundingShape2D = newConfinerShape;

                cinemachineConfiner.InvalidateCache();
            }
            else
            {
                Debug.LogError("PolygonCollider2D not found on the object with tag 'CameraBounds'.");
            }
        }
        else
        {
            Debug.LogError("GameObject with tag 'CameraBounds' not found.");
        }
    }

}