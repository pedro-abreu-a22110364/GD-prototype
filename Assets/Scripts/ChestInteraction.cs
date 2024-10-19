using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    public GameObject chest;

    public Sprite[] spriteArray;

    public static bool isChestOpen = false;     // not very good solution but it will do for now

    public void Start()
    {
        if (isChestOpen)
        {
            OpenChest();
        }
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
        if (!isChestOpen && triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            isChestOpen = true;
            OpenChest();
        }
    }

    public void OpenChest()
    {
        chest.GetComponent<SpriteRenderer>().sprite = spriteArray[1];
    }
}
