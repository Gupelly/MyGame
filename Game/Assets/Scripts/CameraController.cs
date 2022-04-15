using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private Transform target;

    void Update()
    {
        var position = target.position;
        position.z = -10f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
