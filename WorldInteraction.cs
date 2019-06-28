using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : MonoBehaviour
{
    private Interactable activeInteraction;

    public void SetInteraction(Interactable interaction)
    {
        activeInteraction = interaction;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activeInteraction != null)
        {
            Interact();
        }

        SetInteraction(null);
    }

    public void Interact()
    {
        activeInteraction.Interact();
    }
}
