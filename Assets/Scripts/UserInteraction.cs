using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject interactableObject;

    public Sprite[] spriteArray;

    public void Start()
    {
        GameManager.Instance.SetCorrectObjectSprites(interactableObject, spriteArray);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.InteractWithObject(interactableObject, spriteArray);
        }
    }
}
