using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField]
    //private float speed = 2f;
    [SerializeField]
    private Transform target;
    private CinemachineBrain smart;

    private void Awake()
    {
        smart = GetComponent<CinemachineBrain>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log(true);
            smart.enabled = false;
            var position = target.position;
            position.y = target.position.y - 3f;
            position.z = -10f;

        }
    }
}
