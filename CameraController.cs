using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothing = 10f;

    void FixedUpdate()
    {
        
        transform.rotation = player.rotation;
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        targetPosition += transform.up * 0.5f;
        targetPosition = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.fixedDeltaTime);

        transform.position = targetPosition;
    }

}
