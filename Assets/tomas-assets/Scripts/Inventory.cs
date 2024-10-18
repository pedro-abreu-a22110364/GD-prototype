using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Inventory
{
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Key, amount = 1 });

        Debug.Log("Inventory Amount:");
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
