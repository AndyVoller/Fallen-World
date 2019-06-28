using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Paused { get; private set; }

    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject playerStatsUI;
    [SerializeField] private GameObject InventoryUI;

    void Start()
    {
        Paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerStatsUI.SetActive(!playerStatsUI.activeSelf);
            playerUI.SetActive(!playerUI.activeSelf);
            SetPause();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
            playerUI.SetActive(!playerUI.activeSelf);
            SetPause();
        }
    }

    public void SetPause()
    {
        Paused = !Paused;

        if (Paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}
