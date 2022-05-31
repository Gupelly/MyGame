using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diam1 : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Character>();
        if (unit.unlockDash) unit.unlockDoubleJump = true;
        unit.unlockDash = true;
        panel.SetActive(true);
        gameObject.SetActive(false);
        Invoke(nameof(HidePanel), 4f);
    }

    private void HidePanel()
    {
        panel.SetActive(false);
    }
}
