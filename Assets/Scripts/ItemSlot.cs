using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite = null;
    public bool isFull = false;

    [SerializeField]
    public Image itemImage;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.isFull = true;
        //Debug.Log(
        //    "ItemSlotVersion --> itemName = "
        //        + itemName
        //        + " quantity = "
        //        + quantity
        //        + " itemSprite"
        //        + itemSprite
        //);

        itemImage.sprite = itemSprite;
    }
}
