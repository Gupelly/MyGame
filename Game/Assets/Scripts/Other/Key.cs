using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    public GameObject Door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Character>();
        if (unit.unlockDash) unit.unlockDoubleJump = true;
        gameObject.SetActive(false);
        panel.SetActive(true);
        Destroy(Door);
        Invoke(nameof(HidePanel), 4f);
    }

    private void HidePanel()
    {
        panel.SetActive(false);
    }
}
