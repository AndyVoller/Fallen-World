using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRotator))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFader fader;
    CameraRotator cameraRotator;

    // 0 - New Game, 1 - Load Game, 2 - Options, 3 - Exit
    int menuOption = 0;

    void Awake()
    {
        cameraRotator = GetComponent<CameraRotator>();
    }

    void Update()
    {
        if (cameraRotator.isRotating)
            return;

        if(Input.GetKeyDown(KeyCode.Return))
        {
            ChooseOption();
            return;
        }

        float rotateDirection = -Input.GetAxis("Horizontal");
        if (rotateDirection != 0f)
        {
            if (rotateDirection > 0f)           // Anticlockwise rotation
            {
                rotateDirection = 1f;
                menuOption += 3;
                menuOption %= 4;
            }
            else                                // Clockwise rotation
            {
                rotateDirection = -1f;
                menuOption++;
                menuOption %= 4;
            }

            cameraRotator.isRotating = true;
            IEnumerator coroutine = cameraRotator.Rotate(rotateDirection);
            StartCoroutine(coroutine);
        }
    }

    void ChooseOption()
    {
        switch(menuOption)
        {
            case 0: fader.FadeTo("WorldScene");         
                break;
            case 1: // Load Game
                break;
            case 2: fader.FadeTo("Options");
                break;
            case 3: Application.Quit();
                break;
        }
    }

}
