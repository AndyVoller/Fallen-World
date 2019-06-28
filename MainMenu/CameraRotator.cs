using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private new Transform camera;

    public bool isRotating { get; set; }

    /// <summary>
    /// Moves camera between menu options
    /// </summary>

    public IEnumerator Rotate(float direction)
    {
        for (int i = 1; i <=45; i++)
        {
            camera.Rotate(new Vector3(0f, 0f, 2 * direction));
            yield return new WaitForSeconds(0.01f);
        }

        isRotating = false;
    }

}
