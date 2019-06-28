using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected static Transform playerTransform;
    protected static WorldInteraction worldInteraction;

    [SerializeField] protected float interactDistance = 1f;

    protected GameObject interactText;                  // Shows message to start interaction

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        worldInteraction = playerTransform.GetComponent<WorldInteraction>();
        interactText = transform.Find("InteractText").gameObject;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < interactDistance)
        {
            worldInteraction.SetInteraction(this);
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interact with " + this.name);
    }
}
