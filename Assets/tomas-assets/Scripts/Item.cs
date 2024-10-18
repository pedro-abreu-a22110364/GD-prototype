using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Key,
        Card,
        Tool,
    }

    public ItemType itemType;
    public int amount;

    public Item()
    {
        Debug.Log("Item");
    }
}
