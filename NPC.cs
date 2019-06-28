using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private string npcName;
    [TextArea] [SerializeField] private string[] dialogue;

    public override void Interact()
    {
        base.Interact();

        FindObjectOfType<DialogueManager>().AddNewDialogue(dialogue, npcName);
    }
}
