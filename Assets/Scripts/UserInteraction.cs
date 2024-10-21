using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject actionObject;
    public GameObject reactionObject;
    public Sprite[] actionObjectSpriteArray;
    public Sprite[] reactionObjectSpriteArray;
    public string[] storedObjects;

    public void Start()
    {
        GameManager.Instance.SetCorrectObjectSprites(actionObject, actionObjectSpriteArray, reactionObject, reactionObjectSpriteArray);
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
            if (actionObject.CompareTag("Character"))
            {
                GameManager.Instance.ActivateDialogue();
            }
            else 
            {
                GameManager.Instance.InteractWithObject(actionObject, actionObjectSpriteArray, reactionObject, reactionObjectSpriteArray, storedObjects);
            }
        }
    }
}
