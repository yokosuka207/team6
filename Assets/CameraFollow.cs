using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraPreset = 8;
    public float cameraPosY = 6.25f;
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.z = -8; // カメラのZ位置を設定
            newPosition.x += cameraPreset;
            newPosition.y = cameraPosY;
            transform.position = newPosition;
        }
    }
}

