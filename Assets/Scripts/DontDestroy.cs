using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Persist gameObjects between scenes whilst avoiding duplicates
public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if((Object.FindObjectsOfType<DontDestroy>()[i] != this) && // dont delete current gameObject
                (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name))   // delete all gameObjects with same name as current
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
