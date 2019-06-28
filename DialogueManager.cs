using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private List<string> dialogueLines;
    private string npcName;

    [SerializeField] private GameObject dialoguePanel;

    Text npcNameText;
    Text dialogueText;
    Button continueButton;
    int dialogueIndex;

    void Awake()
    {
        npcNameText = dialoguePanel.transform.Find("NPCNameText").GetComponent<Text>();
        dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<Text>();
        continueButton = dialoguePanel.transform.Find("ContinueButton").GetComponent<Button>();

        continueButton.onClick.AddListener(ContinueDialogue);
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;

        MakeDialogue();
    }

    void MakeDialogue()
    {
        npcNameText.text = npcName;
        dialogueText.text = dialogueLines[dialogueIndex];
        dialoguePanel.SetActive(true);
    }

    void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

}
