using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Vector3 CameraDirection;

    private void Update()
    {
        CameraDirection = Camera.main.transform.forward;
        CameraDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(CameraDirection);
    }
}
