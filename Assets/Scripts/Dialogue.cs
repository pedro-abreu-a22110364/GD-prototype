using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private static Dialogue _instance;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index = -1;
    private bool isActive = false;
    public static Dialogue Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Dialogue is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Writing");
        if (isActive && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        Debug.Log("Activating Box");
        textComponent.text = string.Empty;
        isActive = true;
        index = 0;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        Debug.Log("Typing");
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            isActive = false;
            GameManager.Instance.DeactivateDialogue();
        }
    }
}
