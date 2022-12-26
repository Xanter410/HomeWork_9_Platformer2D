using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField, Range(-15, 15)] float _cameraSpeed = 0f;

    void Update()
    {
        var temp = new Vector3(transform.position.x + _cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = temp;
    }
}
