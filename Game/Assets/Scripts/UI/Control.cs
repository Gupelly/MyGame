using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField]
    GameObject control;

    public void Change()
    {
        control.SetActive(!control.activeSelf);
    }
}
