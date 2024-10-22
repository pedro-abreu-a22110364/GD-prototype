using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    [SerializeField] private bool dialogueTriggerActive = false;

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
            if (actionObject.CompareTag("Dialogue"))
            {
                dialogueTriggerActive = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerActive = false;
            if (actionObject.CompareTag("Dialogue"))
            {
                dialogueTriggerActive = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueTriggerActive)
            {
                GameManager.Instance.ManageDialogue(actionObject, reactionObject);
            }
            if (triggerActive)
            {
                GameManager.Instance.InteractWithObject(actionObject, actionObjectSpriteArray, reactionObject, reactionObjectSpriteArray, storedObjects);
            }
        }
        
    }
}
