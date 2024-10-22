using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool isChestOpen { get; set; }
    public bool isChestSewerOpen { get; set; }
    public bool isLeverPulled { get; set; }
    public bool isLeverBroken { get; set; }
    public bool hasHammer { get; set; }
    public bool isHomeDoorOpen { get; set; }
    public bool isHomeDoorUnlocked { get; set; }
    public bool isSewerDoorOpen { get; set; }
    public bool isSewerDoorUnlocked { get; set; }
    public bool isDialogueActive { get; set; }
    public bool isButton1Pressed { get; set; }
    public bool isButton2Pressed { get; set; }
    public bool isButton3Pressed { get; set; }
    public bool isButton4Pressed { get; set; }
    public HashSet<string> inventory { get; set; } = new HashSet<string>();

    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("GameManager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion
    private void FixedUpdate()
    {
        if (isButton1Pressed && isButton2Pressed && isButton3Pressed && isButton4Pressed)
        {
            Debug.Log("You won");
        }
    }

    public void InteractWithObject(GameObject actionObject, Sprite[] actionObjectSpriteArray, GameObject reactionObject, Sprite[] reactionObjectSpriteArray, string[] storedObjects)
    {

        inventory.UnionWith(storedObjects); // Add stored objects to inventory

        switch (actionObject.name)
        {
            case "Chest":
                isChestOpen = true;
                break;
            case "ChestSewer":
                isChestSewerOpen = true;
                break;
            case "Lever":
                if (isLeverPulled)
                {
                    isLeverBroken = RemoveFromInventory("hammer");
                }
                else
                {
                    isLeverPulled = true;
                }
                break;
            case "HomeDoor":
                if (isHomeDoorUnlocked)
                {
                    isHomeDoorOpen = true;
                    actionObject.transform.position += new Vector3(-0.3f, 0);
                }
                else
                {
                    isHomeDoorUnlocked = RemoveFromInventory("homeDoorKey");
                }
                break;
            case "SewerDoor":
                if (isSewerDoorUnlocked)
                {
                    isSewerDoorOpen = true;
                    actionObject.transform.position += new Vector3(-0.3f, 0);
                }
                else
                {
                    isSewerDoorUnlocked = RemoveFromInventory("lever");
                }
                break;
            case "Button1":
                isButton1Pressed = true;
                break;
            case "Button2":
                isButton2Pressed = true;
                break;
            case "Button3":
                isButton3Pressed = true;
                break;
            case "Button4":
                isButton4Pressed = true;
                break;
        }
        SetCorrectObjectSprites(actionObject, actionObjectSpriteArray, reactionObject, reactionObjectSpriteArray);
    }

    public void SetCorrectObjectSprites(GameObject actionObject, Sprite[] actionObjectSpriteArray, GameObject reactionObject, Sprite[] reactionObjectSpriteArray)
    {
        switch (actionObject.name)
        {
            case "Chest":
                if (isChestOpen)
                {
                    OpenChest(actionObject, actionObjectSpriteArray);
                }
                break;
            case "ChestSewer":
                if (isChestSewerOpen)
                {
                    OpenChest(actionObject, actionObjectSpriteArray);
                }
                break;
            case "Lever":
                if (isLeverPulled)
                {
                    if (isLeverBroken)
                    {
                        BreakLever(actionObject, actionObjectSpriteArray);
                    }
                    else 
                    { 
                        PullLever(actionObject, actionObjectSpriteArray, reactionObject, reactionObjectSpriteArray); 
                    }
                }
                break;
            case "HomeDoor":
                if (isHomeDoorUnlocked)
                {
                    OpenLock(reactionObject);
                    if (isHomeDoorOpen)
                    {
                        OpenDoor(actionObject, actionObjectSpriteArray);
                    }
                }
                break;
            case "SewerDoor":
                if (isSewerDoorUnlocked)
                {
                    OpenLock(reactionObject);
                    if (isSewerDoorOpen)
                    {
                        OpenDoor(actionObject, actionObjectSpriteArray);
                    }
                }
                break;
            case "Button1":
                isButton1Pressed = true;
                break;
            case "Button2":
                isButton2Pressed = true;
                break;
            case "Button3":
                isButton3Pressed = true;
                break;
            case "Button4":
                isButton4Pressed = true;
                break;
        }
    }

    public void PushButton(GameObject button, Sprite[] buttonSpriteArray)
    {
        button.GetComponent<SpriteRenderer>().sprite = buttonSpriteArray[0];
        DeleteObject(button.name + "Collider");     
    }
    public void OpenChest(GameObject chest, Sprite[] chestSpriteArray)
    {
        chest.GetComponent<SpriteRenderer>().sprite = chestSpriteArray[0];
    }

    public void PullLever(GameObject lever, Sprite[] leverSpriteArray, GameObject door, Sprite[] doorSpriteArray)
    {
        lever.transform.gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[0];

        CloseDoor(door, doorSpriteArray);
        DrainRoom();
    }

    public void BreakLever(GameObject lever, Sprite[] leverSpriteArray)
    {
        lever.transform.gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[1];
    }

    public void DrainRoom()
    {
        DeleteObject("Water");
    }

    public void OpenDoor(GameObject door, Sprite[] doorSpriteArray)
    {
        GameObject doorParts;
        doorParts = GameObject.Find(door.name + "Parts");
        for (int i = 0; i < doorParts.transform.childCount; i++)
        {
            doorParts.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[i];
        }
        DeleteObject(door.name + "Boundary");
        DeleteObject(door.name + "UIArea");
    }

    public void CloseDoor(GameObject door, Sprite[] doorSpriteArray)
    {
        GameObject doorParts = GameObject.Find(door.name + "Parts");
        for (int i = 0; i < doorParts.transform.childCount; i++)
        {
            doorParts.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[i];
        }
    }
    public void OpenLock(GameObject padlock)
    {
        Destroy(padlock);
    }

    public void DeleteObject(string objName)
    {
        GameObject destroy = GameObject.Find(objName);

        if (destroy != null)
        {
            Destroy(destroy);
        }
    }

    public void ActivateDialogue()
    {
        if (!isDialogueActive)
        {
            isDialogueActive = true;
        }
    }

    public void DeactivateDialogue()
    {
        isDialogueActive = false;
    }
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    public void ManageDialogue(GameObject dialogue, GameObject reactionObject)
    {
        switch (dialogue.name)
        {
            case "ShardsoulUIArea":
                if (inventory.Contains("lever"))
                {
                    dialogue.SetActive(false);
                    reactionObject.SetActive(true);
                    //inventory.Remove("lever");
                }
                break;
        }
    }

    public bool RemoveFromInventory(string objName)
    {
        return inventory.Remove(objName);
    }
}
