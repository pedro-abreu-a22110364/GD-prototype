using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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

    public void InteractWithObject(GameObject interactableObject, Sprite[] spriteArray)
    {
        switch (interactableObject.name)
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
        SetCorrectObjectSprites(interactableObject, spriteArray);
    }

    public void SetCorrectObjectSprites(GameObject interactableObject, Sprite[] spriteArray)
    {
        switch (interactableObject.name)
        {
            case "Chest":
                if (isChestOpen)
                {
                    OpenChest(interactableObject, spriteArray);
                }
                break;
            case "Lever":
                if (isLeverPulled)
                {
                    if (isLeverBroken)
                    {
                        BreakLever(interactableObject, spriteArray);
                    }
                    else 
                    { 
                        PullLever(interactableObject, spriteArray); 
                    }
                }
                break;
        }
    }

    public void OpenChest(GameObject chest, Sprite[] spriteArray)
    {
        chest.GetComponent<SpriteRenderer>().sprite = spriteArray[0];
    }

    public void PullLever(GameObject lever, Sprite[] spriteArray)
    {
        lever.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = spriteArray[0];
        lever.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = spriteArray[1];
        //hasHammer = true;
    }

    public void BreakLever(GameObject lever, Sprite[] spriteArray)
    {
        lever.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = spriteArray[2];
        lever.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = spriteArray[3];
    }
}
