using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private TMP_Text speakerText;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private Image portraitImage;

    [SerializeField]
    private string[] speaker;
    [SerializeField]
    [TextArea]
    private string[] dialogueWords;
    [SerializeField]
    private Sprite[] portrait;

    public float textSpeed;
    private int index = -1;
    private bool isActive = false;

    private bool ísTriggerActive = false;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }

    void Update()
    {
        if (ísTriggerActive) 
        {
            if(!isActive && Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
            else if (isActive && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)))
            {
                if (dialogueText.text == dialogueWords[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueWords[index];
                }
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ísTriggerActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ísTriggerActive = false;
        }
    }

    public void StartDialogue()
    {
        GameManager.Instance.ActivateDialogue();
        dialogueText.text = string.Empty;
        isActive = true;
        index = 0;
        dialogueCanvas.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        speakerText.text = speaker[index];
        portraitImage.sprite = portrait[index];

        foreach (char c in dialogueWords[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueWords.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueCanvas.SetActive(false);
            isActive = false;
            GameManager.Instance.DeactivateDialogue();
        }
    }
}
