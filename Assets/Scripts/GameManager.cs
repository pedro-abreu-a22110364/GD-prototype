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
        lever.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[0];
        lever.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[1];
        //hasHammer = true;

        door.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[0];
        door.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[1];
        door.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[2];
        door.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[3];
        door.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[4];
        door.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = doorSpriteArray[5];

        DrainRoom();
    }

    public void BreakLever(GameObject lever, Sprite[] leverSpriteArray)
    {
        lever.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[2];
        lever.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = leverSpriteArray[3];
    }

    public void DrainRoom()
    {
        Destroy(GameObject.Find("Water"));
        //GameObject.Find("Water").GetComponent<Tilemap>().ClearAllTiles();
    }
}
