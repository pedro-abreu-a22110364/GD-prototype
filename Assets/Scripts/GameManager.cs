using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool isChestOpen { get; set; }
    public bool isLeverPulled { get; set; }
    public bool isLeverBroken { get; set; }
    public bool hasHammer { get; set; }

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

    public void InteractWithObject(GameObject actionObject, Sprite[] actionObjectSpriteArray, GameObject reactionObject, Sprite[] reactionObjectSpriteArray)
    {
        switch (actionObject.name)
        {
            case "Chest":
                isChestOpen = true;
                break;
            case "Lever":
                if (isLeverPulled && hasHammer)
                {
                    isLeverBroken = true;
                }
                else
                {
                    isLeverPulled = true;
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
        }
    }

    public void OpenChest(GameObject chest, Sprite[] chestSpriteArray)
    {
        chest.GetComponent<SpriteRenderer>().sprite = chestSpriteArray[0];
    }

    public void PullLever(GameObject lever, Sprite[] leverSpriteArray, GameObject door, Sprite[] doorSpriteArray)
    {
        lever.transform.gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[0];
        hasHammer = true;

        for (int i = 0; i < door.transform.childCount; i++)
        {
            door.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[i];
        }

        DrainRoom();
    }

    public void BreakLever(GameObject lever, Sprite[] leverSpriteArray)
    {
        lever.transform.gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[1];
    }

    public void DrainRoom()
    {
        Destroy(GameObject.Find("Water"));
        //GameObject.Find("Water").GetComponent<Tilemap>().ClearAllTiles();
    }
}
