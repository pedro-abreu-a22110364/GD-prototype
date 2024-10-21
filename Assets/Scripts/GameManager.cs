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
    public bool isLeverPulled { get; set; }
    public bool isLeverBroken { get; set; }
    public bool hasHammer { get; set; }
    public bool isHomeDoorOpen { get; set; }
    public bool isHomeDoorUnlocked { get; set; }
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

    public void InteractWithObject(GameObject actionObject, Sprite[] actionObjectSpriteArray, GameObject reactionObject, Sprite[] reactionObjectSpriteArray, string[] storedObjects)
    {
        inventory.UnionWith(storedObjects); // Add stored objects to inventory

        switch (actionObject.name)
        {
            case "Chest":
                isChestOpen = true;
                break;
            case "Lever":
                if (isLeverPulled && inventory.Contains("hammer"))
                {
                    isLeverBroken = true;
                    inventory.Remove("hammer");
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
                else if(inventory.Contains("homeDoorKey"))
                {
                    isHomeDoorUnlocked = true;
                    inventory.Remove("homeDoorKey");
                }
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
        }
    }

    public void OpenChest(GameObject chest, Sprite[] chestSpriteArray)
    {
        chest.GetComponent<SpriteRenderer>().sprite = chestSpriteArray[0];
        DeleteObject("ChestUIArea");
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
        DeleteObject("LeverUIArea");
    }

    public void DrainRoom()
    {
        DeleteObject("Water");
    }

    public void OpenDoor(GameObject door, Sprite[] doorSpriteArray)
    {
        GameObject doorParts = GameObject.Find(door.name + "Parts");
        for (int i = 0; i < doorParts.transform.childCount; i++)
        {
            doorParts.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[i];
        }
        DeleteObject("HomeDoorBoundary");
        DeleteObject("HomeDoorUIArea");
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
}
